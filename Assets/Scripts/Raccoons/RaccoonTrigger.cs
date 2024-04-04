using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
public class RaccoonTrigger : MonoBehaviour
{
    #region Private Variables

    private bool _canTrigger, _hasTriggered = false;

    #endregion

    #region Actions

    public static Action RaccoonTriggered;

    #endregion

    #region Unity Functions

    private void OnEnable()
    {
        ActionSubscription(true);
        print(_canTrigger);
    }

    private void OnDisable()
    {
        ActionSubscription(false);
    }

    #endregion

    #region Action Handling

    /// <summary>
    /// Subscribes and ubsubs from actions
    /// </summary>
    /// <param name="isSubscribing">if player is subscribing or ubsub</param>
    private void ActionSubscription(bool isSubscribing)
    {
        if (isSubscribing)
        {
            GameController.CaughtAllBats += AllowTrigger;
        }
        else
        {
            GameController.CaughtAllBats -= AllowTrigger;
        }
    }

    #endregion

    /// <summary>
    /// Allows the raccoon trigger to activate
    /// </summary>
    private void AllowTrigger()
    {
        _canTrigger = true;
    }

    #region Collision Handling

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Triggers when the player walks over to spawn the raccoon
        if(collision.TryGetComponent<PlayerController>(out PlayerController pc))
        {
            if (_canTrigger && !_hasTriggered)
            {
                _hasTriggered = true;
                RaccoonTriggered?.Invoke();
            }
        }
    }

    #endregion
}
