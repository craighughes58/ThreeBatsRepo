using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Broom : MonoBehaviour
{
    [SerializeField] private Transform _craigsHand;
    [SerializeField] private Transform _craigsHandDucking;

    private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Collider2D _trigger;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(GetComponentInParent<PlayerController>().GetComponent<Collider2D>(), _collider);
    }

    /// <summary>
    /// Broom yeeted
    /// </summary>
    /// <param name="force"></param>
    public void Throw(float force)
    {
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
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        transform.parent = _craigsHand.transform.parent;
        ChangePosition(false);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
