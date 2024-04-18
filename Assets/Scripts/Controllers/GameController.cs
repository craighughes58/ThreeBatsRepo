using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    #region Public Variables

    public static GameController Instance;

    #endregion

    #region Serialized Variables

    [SerializeField] private int _startingBatCount = 3;
    [SerializeField] private GameObject _racoonPrefab;
    [SerializeField] private Transform _doorLocation;

    #endregion

    #region Private Variables

    private int _numBatsAlive = 3;
    private bool _diedToBat = false;

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
            PlayerController.DiedToBat += SetBatEnding;
            PlayerController.DiedToRaccoon += SetRaccEnding;

            RaccoonTrigger.RaccoonTriggered += SpawnRaccoon;

            BatBehaviour.BatDied += BatDied;

            FadeToBlack.DoneFading += LoadEndScene;
        }
        else
        {
            PlayerController.DiedToBat -= SetBatEnding;
            PlayerController.DiedToRaccoon -= SetRaccEnding;

            RaccoonTrigger.RaccoonTriggered -= SpawnRaccoon;

            BatBehaviour.BatDied -= BatDied;

            FadeToBlack.DoneFading -= LoadEndScene;
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
        Instantiate(_racoonPrefab, _doorLocation.position, Quaternion.identity);
        SpawnedRaccoon.Invoke(SFXController.SFX.RACCOONNOISES);
    }

    #endregion

    #region Game Over Management

    /// <summary>
    /// Sets bat ending bool if player died to bat
    /// </summary>
    private void SetBatEnding()
    {
        _diedToBat = true;
    }

    /// <summary>
    /// Sets bat ending bool if player died to raccoon
    /// </summary>
    private void SetRaccEnding()
    {
        _diedToBat = false;
    }

    /// <summary>
    /// Loads end game scene
    /// </summary>
    private void LoadEndScene()
    {
        if(_diedToBat)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            SceneManager.LoadScene(5);
        }
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
