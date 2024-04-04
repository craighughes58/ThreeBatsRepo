using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class GameController : MonoBehaviour
{
    #region Public Variables

    public static GameController Instance;

    #endregion

    #region Private Variables

    private int _numBatsAlive = 0;

    #endregion

    #region Unity Functions
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Instance = this;
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
    }

    #endregion

    #region Game Over Management
    /// <summary>
    /// Called when the game ends
    /// </summary>
    private void GameOver()
    {
        // Any end game functionality in this scene
        LoadEndScene();
    }

    /// <summary>
    /// Loads end game scene
    /// </summary>
    private void LoadEndScene()
    {
        // Load end game scene here
    }

    #endregion
}
