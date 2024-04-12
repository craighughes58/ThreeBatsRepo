using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehaviour : MonoBehaviour
{
    #region Serialized Variables
    [Tooltip("The areas on the wall that the bat flies to")]
    [SerializeField] private List<Transform> _wallNodes;

    [Tooltip("How fast the bat moves")]
    [SerializeField] private float _speed;

    [Tooltip("The minimum and maximum amount of time that the bat can stay on the wall")]
    [SerializeField] private Vector2 _wallDelayBounds;


    [Tooltip("The jumpscare System")]
    [SerializeField] private JumpscareController _jumpScarer;

    [Tooltip("The Captured text")]
    [SerializeField] private GameObject _capturedScreen;
    #endregion

    #region Private Variables

    //The transform of the player
    private Transform _playerPosition;

    //has the bat collided with the player
    private bool _reachedPlayer = false;

    //has the bat collided with the wall node
    private bool _reachedNode = false;

    //The wall node that the bat needs to move towards
    private Transform _nextNode = null;
    #endregion

    #region Actions

    public static Action BatDied;
    public static Action<SFXController.SFX> BatAttack;
    public static Action<SFXController.SFX> BatHit;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _playerPosition = PlayerController.Instance.transform;
        StartCoroutine(MovementPattern(true));
    }


    #region Movement and Node Management
    private IEnumerator MovementPattern(bool firstRun)
    {
        BatAttack?.Invoke(SFXController.SFX.BATATTACK);

        SetNextNode();
        //wait on the wall
        yield return new WaitForSeconds(UnityEngine.Random.Range(_wallDelayBounds.x,_wallDelayBounds.y));

        //50/50 chance they fly at the player as long as its not the first movement
        if(UnityEngine.Random.Range(0,2) == 0 && !firstRun)
        {
            _jumpScarer.ActivateJumpscare();
            while(!_reachedPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position, _playerPosition.position, _speed);
                yield return new WaitForEndOfFrame();
            }
        }
        //fly to the next wall 
        while (!_reachedNode)
        {
            transform.position = Vector2.MoveTowards(transform.position, _nextNode.position, _speed);
            yield return new WaitForEndOfFrame();

        }


        //reset variables for next coroutine 
        _reachedPlayer = false;
        _reachedNode = false;
        StartCoroutine(MovementPattern(false));
    }



    private void SetNextNode()
    {
        Transform prevNode = _nextNode;
        while(_nextNode == prevNode)
        {
            _nextNode = _wallNodes[UnityEngine.Random.Range(0, _wallNodes.Count)];
        }
        
    }
    #endregion
    #region Collisions and Triggers


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            StartCoroutine(DelayedFoundPlayer());
        }
        else if (collision.gameObject.tag.Equals("Node"))
        {
            _reachedNode = true;
        }
        else if(collision.gameObject.tag.Equals("Broom"))
        {
            if(collision.TryGetComponent<Broom>(out Broom broom))
            {
                if(broom.IsThrown)
                {
                    BatHit?.Invoke(SFXController.SFX.ENEMYCAPTURED);
                    BatDied?.Invoke();
                      
                    Destroy(gameObject);
                    Destroy(Instantiate(_capturedScreen,transform.position,Quaternion.identity),1f);
                }
            }   
        }
    }

    private IEnumerator DelayedFoundPlayer()
    {
        yield return new WaitForSeconds(.3f);
        _reachedPlayer = true;
    }
    #endregion

}
