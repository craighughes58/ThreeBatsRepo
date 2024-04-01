using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private int _numBatsAlive = 0;

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

    /// <summary>
    /// Spawns raccoon at the end of the game
    /// </summary>
    private void SpawnRaccoon()
    {
        // Spawn Raccoon here
    }

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
}
