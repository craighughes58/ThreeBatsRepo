/*****************************************************************************
// File Name :         PlayerAttack.cs
// Author :            Andrea Swihart-DeCoster
// Creation Date :     Date 03/30/24
//
// Brief Description : Controls player attacks
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttack : MonoBehaviour
{
    #region Serialized Variables

    [SerializeField] private GameObject _throwable;
    [SerializeField] private float _throwForce;

    #endregion

    #region Private Variables

    private PlayerActions _playerActions;
    private PlayerMovement _playerMovement;

    #endregion

    #region Public & Accessor Variables

    public bool HasBroom { get; private set; }

    #endregion

    #region Unity Functions

    private void Awake()
    {
        _playerActions = new PlayerActions();
    }

    private void OnEnable()
    {
        _playerActions.Enable();

        _playerActions.DefaultMap.Attack.performed += OnAttack;
    }
    private void OnDisable()
    {
        _playerActions.Disable();
    }




    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        HasBroom = true;

        BoxCollider2D collider = _throwable.GetComponent<BoxCollider2D>();
    }

    #endregion

    #region Throwing Functions

    /// <summary>
    /// Called when player issues attack input
    /// </summary>
    private void OnAttack(InputAction.CallbackContext context)
    {
        if (CanAttack())
        {
            RotateThrowable();
            ThrowObject();
        }
    }

    /// <summary>
    /// Rotates the throwable to the mouse
    /// </summary>
    private void RotateThrowable()
    {
        Vector3 playerWorldPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 throwDir = Input.mousePosition - playerWorldPos;
        float throwAngle = Mathf.Atan2(throwDir.y, throwDir.x) * Mathf.Rad2Deg;
        _throwable.transform.rotation = Quaternion.AngleAxis(throwAngle - 90, Vector3.forward);
    }

    /// <summary>
    /// Applies force to the object
    /// </summary>
    private void ThrowObject()
    {
        if (_throwable.TryGetComponent<Broom>(out Broom broom))
        {
            broom.Throw(_throwForce);
            HasBroom = false;
        }
    }

    /// <summary>
    /// Picks up the broom
    /// </summary>
    public void CollectBroom()
    {
        HasBroom = true;
    }

    /// <summary>
    /// If player can attack
    /// </summary>
    /// <returns></returns>
    private bool CanAttack()
    {
        return !_playerMovement.IsDucking && HasBroom && !PlayerController.Instance.IsDead;
    }

    #endregion
}
