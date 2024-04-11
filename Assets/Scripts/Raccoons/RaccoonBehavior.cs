using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonBehavior : MonoBehaviour
{

    #region Serialized Variables

    [Tooltip("How fast the bat moves")]
    [SerializeField] private float _speed;

    [Tooltip("The Captured text")]
    [SerializeField] private GameObject _capturedScreen;

    [Tooltip("How much the raccoon curves to the side when running at the player")]
    [SerializeField] private float _zigZagOffset;

    [Tooltip("How much time in between zig zag calculations")]
    [SerializeField] private float _zigZagDelay;


    #endregion

    #region Private Variables

    //The transform of the player
    private Transform _playerPosition;

    //has the bat collided with the wall node
    private bool _reachedNode = false;

    private Vector2 _targetPosition;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _playerPosition = PlayerController.Instance.transform;
       StartCoroutine(CalculateZigZag());
    }


    private void FixedUpdate()
    {
        transform.right = _targetPosition - new Vector2(transform.position.x,transform.position.y);
        if(Vector2.Distance(transform.position,_playerPosition.position) > _zigZagOffset)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerPosition.position, _speed * Time.deltaTime);
        }
        //transform.Translate(Vector3.forward * Time.deltaTime * _speed);



    }

    private IEnumerator CalculateZigZag()
    {
        Vector3 nextOffset = Vector3.zero;
        //calculate

        if (_playerPosition.position.x > transform.position.x)
        {
            nextOffset.x += _zigZagOffset;
        }
        else
        {
            nextOffset.x -= _zigZagOffset;
        }

        if (_playerPosition.position.y > transform.position.y)
        {
            nextOffset.y += _zigZagOffset;
        }
        else
        {
            nextOffset.y -= _zigZagOffset;
        }


        _targetPosition = _playerPosition.position + nextOffset;
        //wait
        yield return new WaitForSeconds(_zigZagDelay);
        //restart
        StartCoroutine(CalculateZigZag());
    }
    


}
