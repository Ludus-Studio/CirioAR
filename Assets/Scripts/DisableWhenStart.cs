using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWhenStart : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }
}
