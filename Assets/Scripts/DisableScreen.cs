using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableScreen : MonoBehaviour
{
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }


}
