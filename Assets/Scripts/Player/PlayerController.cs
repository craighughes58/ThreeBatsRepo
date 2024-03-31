/*****************************************************************************
// File Name :         PlayerController.cs
// Author :            Andrea Swihart-DeCoster
// Creation Date :     Date 03/30/24
//
// Brief Description : Controls the player
*****************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Sprite _default;
    [SerializeField] private Sprite _ducking;

    public PlayerActions Actions { get; private set; }


    private void Awake()
    {
        Actions = new PlayerActions();
    }

    private void OnEnable()
    {
        Actions.Enable();
    }

    public void SetDefaltSprite()
    {
        GetComponent<SpriteRenderer>().sprite = _default;
    }

    public void SetDuckingSprite()
    {
        GetComponent<SpriteRenderer>().sprite = _ducking;
    }
}
