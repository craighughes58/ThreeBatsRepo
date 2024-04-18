using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    #region Serialized Variables

    [SerializeField] FMODUnity.StudioEventEmitter _broomThrow;
    [SerializeField] FMODUnity.StudioEventEmitter _pickUpBroom;
    [SerializeField] FMODUnity.StudioEventEmitter _batAttack;
    [SerializeField] FMODUnity.StudioEventEmitter _racoonNoises;
    [SerializeField] FMODUnity.StudioEventEmitter _enemyCaptured;
    [SerializeField] FMODUnity.StudioEventEmitter _duck;
    [SerializeField] FMODUnity.StudioEventEmitter _doorOpens;
    [SerializeField] FMODUnity.StudioEventEmitter _deathSound;

    #endregion

    #region Private Variables

    private Dictionary<SFX, FMODUnity.StudioEventEmitter> _sfxDictionary;

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
            Broom.BroomThrown += PlaySFX;
            Broom.PickUpBroom += PlaySFX;

            BatBehaviour.BatAttack += PlaySFX;
            BatBehaviour.BatHit += PlaySFX;

            GameController.SpawnedRaccoon += PlaySFX;
            GameController.DoorOpens += PlaySFX;

            PlayerMovement.Duck += PlaySFX;

            PlayerController.DeathSFX += PlaySFX;
        }
        else
        {
            Broom.BroomThrown -= PlaySFX;
            Broom.PickUpBroom -= PlaySFX;

            BatBehaviour.BatAttack -= PlaySFX;
            BatBehaviour.BatHit -= PlaySFX;

            GameController.SpawnedRaccoon -= PlaySFX;
            GameController.DoorOpens -= PlaySFX;

            PlayerMovement.Duck -= PlaySFX;

            PlayerController.DeathSFX -= PlaySFX;
        }
    }

    #endregion

    #region Public Variables

    public enum SFX
    {
        BROOMTHROW,
        PICKUPBROOM,
        BATATTACK,
        RACCOONNOISES,
        ENEMYCAPTURED,
        DUCK,
        DOOROPENS,
        DEATH
    }

    #endregion

    #region Unity Functions

    // Start is called before the first frame update
    void Start()
    {
        CreateDictionary();
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

    private void CreateDictionary()
    {
        _sfxDictionary = new Dictionary<SFX, FMODUnity.StudioEventEmitter>();
        _sfxDictionary.Add(SFX.BROOMTHROW, _broomThrow);
        _sfxDictionary.Add(SFX.PICKUPBROOM, _pickUpBroom);
        _sfxDictionary.Add(SFX.BATATTACK, _batAttack);
        _sfxDictionary.Add(SFX.RACCOONNOISES, _racoonNoises);
        _sfxDictionary.Add(SFX.ENEMYCAPTURED, _enemyCaptured);
        _sfxDictionary.Add(SFX.DUCK, _duck);
        _sfxDictionary.Add(SFX.DOOROPENS, _doorOpens);
        _sfxDictionary.Add(SFX.DEATH, _deathSound);
    }

    private void PlaySFX(SFX sfx)
    {
        _sfxDictionary[sfx].Play();
    }
}
