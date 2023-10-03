using UnityEngine;
using Cinemachine;


//Script developed by chatgpt for cinemachine documentation
public class CameraSwitch : MonoBehaviour
{
    public CinemachineFreeLook thirdPersonCamera;
    public CinemachineVirtualCamera firstPersonCamera;

    void Start()
    {
        EnableThirdPersonCamera();
    }

    void Update()
    {
       //need to switch this to the input system
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCamera();
        }
    }

    void ToggleCamera()
    {
        if (thirdPersonCamera.gameObject.activeSelf)
        {
            EnableFirstPersonCamera();
        }
        else
        {
            EnableThirdPersonCamera();
        }
    }

    void EnableThirdPersonCamera()
    { 
        //enable player rotation
        thirdPersonCamera.gameObject.SetActive(true);
        firstPersonCamera.gameObject.SetActive(false);
    }

    void EnableFirstPersonCamera()
    {
        //lock player rotation
        firstPersonCamera.gameObject.SetActive(true);
        thirdPersonCamera.gameObject.SetActive(false);
    }
}