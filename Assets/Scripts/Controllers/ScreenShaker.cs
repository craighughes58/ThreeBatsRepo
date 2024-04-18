using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    public static float shakeDuration = 0;
    public float shakeAMT = 0.35f;
    private Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        shakeDuration = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.position = transform.position + (Random.insideUnitSphere * shakeAMT);
            shakeDuration -= Time.deltaTime;

        }

    }
}
