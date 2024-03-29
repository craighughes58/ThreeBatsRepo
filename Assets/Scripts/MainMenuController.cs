/*****************************************************************************
// File Name :         MenuController.cs
// Author :            Craig D. Hughes
// Creation Date :     September 21, 2023
//
// Brief Description : This script controls scene transitions and button
//                     controls like quitting the game
//                  
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    #region Private Variables
    //the gameobject in the canvas that is currently active
    private GameObject _currentSection = null;
    #region Serialized Variables
    [Tooltip("Different gameobjects in the canvas that hold different information")]
    [SerializeField] private List<GameObject> _canvasPieces;
    [Tooltip("The time between hitting the start button and loading the scene")]
    [SerializeField] private float _delayTime;
    #endregion

    #endregion

    private void Start()
    {
        if (_canvasPieces.Count >= 1)
        {
            _currentSection = _canvasPieces[0];
        }

    }
    /// <summary>
    /// change the scene to the scene given by name to this method
    /// </summary>
    /// <param name="name">the name of the scene that must be loaded</param>
    public void MoveScene(string name)
    {
        //the noise that plays when changing scene
        //AudioSource.PlayClipAtPoint(ButtonNoise, Camera.main.transform.position);
        //change the scene
        foreach (GameObject piece in _canvasPieces)
        {
            piece.SetActive(false);
        }
        StartCoroutine(LoadNextScene(name));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerator LoadNextScene(string name)
    {
        yield return new WaitForSeconds(_delayTime);
        UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    /// <summary>
    /// exit the game
    /// called from a button
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="section"></param>
    public void ChangeCanvas(int section)
    {
        _currentSection.SetActive(false);
        _currentSection = _canvasPieces[section];
        _currentSection.SetActive(true);
    }
}
