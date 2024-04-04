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
    #region Serialized Variables

    [SerializeField] private Sprite _default;
    [SerializeField] private Sprite _ducking;

    #endregion

    #region Public & Accessor Variables

    public static PlayerController Instance;
    public PlayerActions Actions { get; private set; }

    #endregion

    #region Unity Functions

    private void Awake()
    {
        Actions = new PlayerActions();
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        Actions.Enable();
    }

    #endregion

    #region Sprite Management
    /// <summary>
    /// Sets default craig
    /// </summary>
    public void SetDefaltSprite()
    {
        GetComponent<SpriteRenderer>().sprite = _default;
    }

    /// <summary>
    /// Sets crouching craig
    /// </summary>
    public void SetDuckingSprite()
    {
        GetComponent<SpriteRenderer>().sprite = _ducking;
    }

    #endregion

    #region Collision Handling

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<Broom>(out Broom broom))
        {
            GetComponent<PlayerAttack>().CollectBroom();
            broom.AttachToCraig();
        }
    }

    #endregion
}
