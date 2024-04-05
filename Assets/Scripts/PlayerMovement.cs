/*****************************************************************************
// File Name :         PlayerMovement.cs
// Author :            Andrea Swihart-DeCoster
// Creation Date :     Date 03/30/24
//
// Brief Description : Player Movement Controller
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    #region Serialized Variables

    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _duckingSpeed;

    #endregion

    #region Private Variables

    private float _currentSpeed;

    private PlayerActions _playerActions;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    public bool IsDucking { get; private set; }

    #endregion

    #region Unity Functions

    private void Awake()
    {
        _playerActions = new PlayerActions();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _currentSpeed = _defaultSpeed;
    }

    private void OnEnable()
    {
        _playerActions.Enable();

        _playerActions.DefaultMap.Move.performed += OnMove;
        _playerActions.DefaultMap.Move.canceled += OnMove;
        _playerActions.DefaultMap.Duck.performed += OnDuck;
        _playerActions.DefaultMap.Duck.canceled += OnDuck;
    }

    private void OnDisable()
    {
        _playerActions.Disable();
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movement * _currentSpeed;
        RotatePlayer();
    }
    #endregion

    #region Movement Management
    /// <summary>
    /// Reads player movement values
    /// </summary>
    private void OnMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Sets current ducking state + sprite
    /// </summary>
    private void OnDuck(InputAction.CallbackContext context)
    {
        IsDucking = !IsDucking;
        PlayerController _pc = GetComponent<PlayerController>();

        if(IsDucking)
        {
            _pc.SetDuckingSprite();
            _currentSpeed = _duckingSpeed; 
        }
        else
        {
           _pc.SetDefaltSprite();
            _currentSpeed = _defaultSpeed;
        }

        if (GetComponent<PlayerAttack>().HasBroom)
        {
            GetComponentInChildren<Broom>().ChangePosition(IsDucking);
        }
    }

    /// <summary>
    /// Rotates player to the mouse
    /// </summary>
    private void RotatePlayer()
    {
        if(!IsDucking)
        {
            Vector3 playerWorldPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 lookDir = Input.mousePosition - playerWorldPos;
            float lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(lookAngle - 90, Vector3.forward);
        }
    }

    #endregion
}
