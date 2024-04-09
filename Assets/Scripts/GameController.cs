using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    #region Public Variables

    public static GameController Instance;

    #endregion

    #region Private Variables

    private int _numBatsAlive = 3;

    [Tooltip("The light that the player will go to")]
    [SerializeField] private GameObject _doorLight;

    [Tooltip("This text will notify the player that a door opened")]
    [SerializeField] private TextMeshProUGUI _doorOpensText;
    #endregion

    #region Actions

    /// <summary>
    /// Called when all bats have been caught
    /// </summary>
    public static Action CaughtAllBats;

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
            StartCoroutine(NotifyOfDoor());
            _doorLight.SetActive(true);
            CaughtAllBats?.Invoke();
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

    private void OpenDoor()
    {

    }

    #endregion

    #region UI

    private IEnumerator NotifyOfDoor()
    {
        _doorOpensText.enabled = true;
        yield return new WaitForSeconds(3f);
        _doorOpensText.enabled = false;
    }

    #endregion
}
