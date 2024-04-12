using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class FadeToBlack : MonoBehaviour
{
    #region Actions

    public static Action DoneFading;

    #endregion

    #region Action Handling

    /// <summary>
    /// Subs and unsubs to action
    /// </summary>
    /// <param name="isSubscribing"></param>
    private void ActionSubscription(bool isSubscribing)
    {
        if (isSubscribing)
        {
            PlayerController.DiedToBat += FadeOut;
            PlayerController.DiedToRaccoon += FadeOut;
        }
        else
        {
            PlayerController.DiedToBat -= FadeOut;
            PlayerController.DiedToRaccoon -= FadeOut;
        }
    }


    #endregion

    #region Unity Functions

    private void OnEnable()
    {
        ActionSubscription(true);
    }
    private void OnDisable()
    {
        ActionSubscription(false);
    }

    #endregion

    #region UI

    /// <summary>
    /// Enables the fade obj to fade out
    /// </summary>
    private void FadeOut()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(FadeDuration());
    }

    /// <summary>
    /// Calls teh DoneFading action after fading out
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeDuration()
    {
        yield return new WaitForSecondsRealtime(3.25f);
        DoneFading?.Invoke();
    }

    #endregion
}
