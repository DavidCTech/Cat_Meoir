using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class example : MonoBehaviour
{
    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        myLight.intensity = Mathf.PingPong(Time.time, 8);
    }
}