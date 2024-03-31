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
    [SerializeField] private float _defaultSpeed;
    [SerializeField] private float _duckingSpeed;
    private float _currentSpeed;

    private PlayerActions _playerActions;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    private bool _isDucking;

    // Start is called before the first frame update
    private void Awake()
    {
        _playerActions = new PlayerActions();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _playerActions.Enable();

        _playerActions.DefaultMap.Move.performed += OnMove;
        _playerActions.DefaultMap.Move.canceled += OnMove;
        _playerActions.DefaultMap.Duck.performed += OnDuck;
        _playerActions.DefaultMap.Duck.canceled += OnDuck;
    }

    private void Start()
    {
        _currentSpeed = _defaultSpeed;
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movement * _currentSpeed;
    }

    /// <summary>
    /// Reads player movement values
    /// </summary>
    private void OnMove(InputAction.CallbackContext context)
    {
        _movement = context.ReadValue<Vector2>();
    }

    /// <summary>
    /// Sets current ducking state
    /// </summary>
    private void OnDuck(InputAction.CallbackContext context)
    {
        _isDucking = !_isDucking;
        
        PlayerController _pc = GetComponent<PlayerController>();
        if(_isDucking)
        {
            _pc.SetDuckingSprite();
            _currentSpeed = _duckingSpeed;
        }
        else
        {
           _pc.SetDefaltSprite();
            _currentSpeed = _defaultSpeed;
        }
    }

    /// <summary>
    /// Checks if player is currently ducking
    /// </summary>
    /// <returns> _isDucking </returns>
    public bool IsDucking()
    {
        return _isDucking;
    }

    private void OnDisable()
    {
        _playerActions.Disable();
    }
}
