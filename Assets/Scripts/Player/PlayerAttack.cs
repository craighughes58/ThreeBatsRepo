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

public class PlayerAttack : MonoBehaviour
{
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
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if(CanAttack())
        {
            Debug.Log("Attacked");
        }
    }

    private bool CanAttack()
    {
        return !_playerMovement.IsDucking();
    }

    private void OnDisable()
    {
        _playerActions.Disable();
    }
}
