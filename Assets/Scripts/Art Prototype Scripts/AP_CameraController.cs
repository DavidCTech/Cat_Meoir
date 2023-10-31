using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_CameraController : MonoBehaviour
{
    public Transform target;

    void Awake()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    void LateUpdate()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
