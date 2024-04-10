/*****************************************************************************
// File Name :         Broom.cs
// Author :            Andrea Swihart-DeCoster
// Creation Date :     Date 03/31/24
//
// Brief Description : A broom!
*****************************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Broom : MonoBehaviour
{
    #region Serialized Variables

    [SerializeField] private Transform _craigsHand;
    [SerializeField] private Transform _craigsHandDucking;

    [SerializeField] private Collider2D _collider;
    [SerializeField] private Collider2D _trigger;

    [Header("SFX")]
    [SerializeField] private GameObject _throwSound;

    #endregion

    #region Private Variables

    private Rigidbody2D _rb;
    private bool _attachedToCraig = true;

    #endregion

    #region Public & Accessor Variables

    public bool IsThrown {  get; private set; }

    #endregion

    #region Actions

    public static Action<SFXController.SFX> BroomThrown;
    public static Action<SFXController.SFX> PickUpBroom;

    #endregion

    #region Unity Functions
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(GetComponentInParent<PlayerController>().GetComponent<Collider2D>(), _collider);
        IsThrown = false;
    }

    #endregion

    #region Throwing & Position Updates

    /// <summary>
    /// Broom yeeted
    /// </summary>
    /// <param name="force"></param>
    public void Throw(float force)
    {
        BroomThrown?.Invoke(SFXController.SFX.BROOMTHROW);

        _attachedToCraig = false;
        IsThrown = true;
        transform.parent = null;
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.AddForce(transform.up * force);
        _rb.freezeRotation = true;
    }

    /// <summary>
    /// Attaches to the Craig character
    /// </summary>
    public void AttachToCraig()
    {
        if (!_attachedToCraig)
        {
            PickUpBroom?.Invoke(SFXController.SFX.PICKUPBROOM);

            _attachedToCraig = true;
            IsThrown = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            transform.parent = _craigsHand.transform.parent;
            ChangePosition(false);
        }
    }

    /// <summary>
    /// Sets position based on crouched or not
    /// </summary>
    /// <param name="_isDucking"> is craig ducking? </param>
    public void ChangePosition(bool _isDucking)
    {
        transform.position = _isDucking ? _craigsHandDucking.position : _craigsHand.position;
        transform.rotation = _isDucking ? _craigsHandDucking.rotation : _craigsHand.rotation;
    }

    #endregion

    #region Collision Handling
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    #endregion
}
