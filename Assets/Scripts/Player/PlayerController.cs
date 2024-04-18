/*****************************************************************************
// File Name :         PlayerController.cs
// Author :            Andrea Swihart-DeCoster
// Creation Date :     Date 03/30/24
//
// Brief Description : Controls the player
*****************************************************************************/

using System;
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

    #region Private Variables

    public bool IsDead { get; set; }

    #endregion

    #region Actions

    public static Action DiedToBat;
    public static Action DiedToRaccoon;
    public static Action<SFXController.SFX> DeathSFX;

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

    #region State Management

    /// <summary>
    /// What happens when the player dies
    /// </summary>
    private void KilledByBat()
    {
        if (!IsDead)
        {
            IsDead = true;
            DeathSFX?.Invoke(SFXController.SFX.DEATH);
            DiedToBat?.Invoke();
        }
    }

    /// <summary>
    /// What happens when the player dies
    /// </summary>
    private void KilledByRaccoon()
    {
        if (!IsDead)
        {
            IsDead = true;
            DeathSFX?.Invoke(SFXController.SFX.DEATH);
            DiedToRaccoon?.Invoke();
        }
    }

    #endregion

    #region Collision Handling

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detects broom to pickup
        if (collision.transform.TryGetComponent<Broom>(out Broom broom))
        {
            GetComponent<PlayerAttack>().CollectBroom();
            broom.AttachToCraig();
        }

        // Detects bat to lose
        if(collision.transform.TryGetComponent<BatBehaviour>(out BatBehaviour batBehaviour))
        {
            
            if(!GetComponent<PlayerMovement>().IsDucking)
            {
                KilledByBat();
            }
        }

        // Detects raccoon to lose
        if (collision.transform.TryGetComponent<RaccoonBehavior>(out RaccoonBehavior racBehaviour))
        {
            KilledByRaccoon();
        }
    }

    #endregion
}
