using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWave : MonoBehaviour
{

    public Vector3 startPosition;
    public float frequence = 5;
    public float magnitude = 5;
    public float offset = 0;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + transform.forward * Mathf.Sin(Time.time * frequence + offset) * magnitude;
    }
}
