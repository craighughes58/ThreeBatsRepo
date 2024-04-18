using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Serialized Variables
    [Tooltip("How fast the camera moves towards the next position")]
    [SerializeField] private float _desiredDuration;

    [Tooltip("The curve for the movement")]
    [SerializeField] AnimationCurve _curve;

    [SerializeField] private float _castYoffset;
    #endregion

    #region Private Variables
    //the position the camera is moving to
    private Vector3 _targetPosition;
    //the position where the camera was
    private Vector3 _lastPositon;
    //
    private float _elapsedTimeLerp = 0;
    #endregion

    public static CameraController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        /*        _targetPosition = transform.position;
                _lastPositon = transform.position;*/
    }

    private void FixedUpdate()
    {
        if (_targetPosition != transform.position)
        {
            _elapsedTimeLerp += Time.fixedDeltaTime;
            float _percentageComplete = _elapsedTimeLerp / _desiredDuration;
            transform.position = Vector3.Lerp(_lastPositon, _targetPosition, _curve.Evaluate(_percentageComplete));
        }
    }

    public void UpdatePosition(Vector3 _nextPos, bool isCast)
    {
        _elapsedTimeLerp = 0;
        _lastPositon = transform.position;
        if (isCast)
        {
            _targetPosition = new Vector3(_nextPos.x, _nextPos.y + _castYoffset, -10f);
        }
        else
        {
            _targetPosition = new Vector3(_nextPos.x, _nextPos.y, -10f);

        }
    }

    public float GetDesiredDuration()
    {
        return _desiredDuration;
    }
}
