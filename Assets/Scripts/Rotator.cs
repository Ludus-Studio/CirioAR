using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed;
    public Vector3 rotationVector;

    public bool isCartRotation;
    public Transform[] cartsTransform;
    public Transform cartRotation;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (isCartRotation)
            for (int i = 0; i < cartsTransform.Length; i++)
                cartsTransform[i].transform.rotation = cartRotation.rotation;

        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);

    }
}
