using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraPrint : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCamera;
    private float xAxisMaxSpeed; 


    void Start()
    {
        xAxisMaxSpeed = thirdPersonCamera.m_XAxis.m_MaxSpeed;
        Debug.Log("X Axis Max Speed: " + xAxisMaxSpeed);
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentXAxisPosition = thirdPersonCamera.m_XAxis.Value;
        Debug.Log("X Axis Max Speed: " + xAxisMaxSpeed + "Current speed: " + currentXAxisPosition);
    }
}
