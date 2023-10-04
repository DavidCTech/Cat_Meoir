using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInputs : MonoBehaviour
{
    CameraController cameraControls;


    private void OnEnable()
    {
        if (cameraControls == null)
        {
            cameraControls = new CameraController();
        }

        cameraControls.Enable();
    }

    private void OnDisable()
    {
        cameraControls.Disable();
    }


}