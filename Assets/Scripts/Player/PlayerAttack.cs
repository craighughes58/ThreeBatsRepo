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
    [SerializeField] private GameObject _throwable;
    [SerializeField] private float _throwForce;
    private bool _hasBroom;

    private PlayerActions _playerActions;
    private PlayerMovement _playerMovement;

    private void OnEnable()
    {
        _playerActions.Enable();

        _playerActions.DefaultMap.Attack.performed += OnAttack;
    }

    private void Awake()
    {
        _playerActions = new PlayerActions();
    }

    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _hasBroom = true;

        BoxCollider2D collider = _throwable.GetComponent<BoxCollider2D>();
        
    }
    
    /// <summary>
    /// Called when player issues attack input
    /// </summary>
    private void OnAttack(InputAction.CallbackContext context)
    {
        if(CanAttack())
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
            _hasBroom = false;
        }
    }

    /// <summary>
    /// Picks up the broom
    /// </summary>
    public void CollectBroom()
    {
        _hasBroom = true;
    }

    /// <summary>
    /// If player can attack
    /// </summary>
    /// <returns></returns>
    private bool CanAttack()
    {
        return !_playerMovement.IsDucking() && _hasBroom;
    }

    /// <summary>
    /// If player has broom
    /// </summary>
    public bool HasBroom()
    {
        return _hasBroom;
    }

    private void OnDisable()
    {
        _playerActions.Disable();
    }
}
