using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    #region Public Variables

    public static GameController Instance;

    #endregion

    #region Serialized Variables

    [SerializeField] private int _startingBatCount = 3;

    #endregion

    #region Private Variables

    private int _numBatsAlive = 0;

    #endregion

    #region Actions

    /// <summary>
    /// Called when all bats have been caught
    /// </summary>
    public static Action CaughtAllBats;

    public static Action<SFXController.SFX> SpawnedRaccoon;
    public static Action<SFXController.SFX> DoorOpens;

    #endregion

    #region Unity Functions
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _numBatsAlive = _startingBatCount;
    }

    private void OnEnable()
    {
        ActionSubscription(true);
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
            PlayerController.DiedToBat += BatEnding;
            PlayerController.DiedToRaccoon += RaccoonEnding;

            RaccoonTrigger.RaccoonTriggered += SpawnRaccoon;

            BatBehaviour.BatDied += BatDied;
        }
        else
        {
            PlayerController.DiedToBat -= BatEnding;
            PlayerController.DiedToRaccoon -= RaccoonEnding;

            RaccoonTrigger.RaccoonTriggered -= SpawnRaccoon;

            BatBehaviour.BatDied -= BatDied;
        }
    }

    #endregion

    #region Bats

    /// <summary>
    /// Removes a bat from the bat counter
    /// </summary>
    private void BatDied()
    {
        _numBatsAlive--;

        if(_numBatsAlive <= 0)
        {
            CaughtAllBats?.Invoke();
            DoorOpens?.Invoke(SFXController.SFX.DOOROPENS);
        }
    }

    #endregion

    #region Raccoon!

    /// <summary>
    /// Spawns raccoon at the end of the game
    /// </summary>
    private void SpawnRaccoon()
    {
        // Spawn Raccoon here
        print("Spawned Raccoon");
        SpawnedRaccoon.Invoke(SFXController.SFX.RACCOONNOISES);
    }

    #endregion

    #region Game Over Management

    /// <summary>
    /// Calls the Bat Ending
    /// </summary>
    private void BatEnding()
    {
        LoadEndScene(3);
    }
    
    /// <summary>
    /// Calls the Raccoon ending
    /// </summary>
    private void RaccoonEnding()
    {
        LoadEndScene(5);
    }

    /// <summary>
    /// Loads end game scene
    /// </summary>
    private void LoadEndScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
        // Load end game scene here
    }

    #endregion
}
