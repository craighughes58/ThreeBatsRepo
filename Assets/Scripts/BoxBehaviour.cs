using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{

    [SerializeField] private GameObject _trash;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name.Contains("Broom"))
        {
            Instantiate(_trash,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
