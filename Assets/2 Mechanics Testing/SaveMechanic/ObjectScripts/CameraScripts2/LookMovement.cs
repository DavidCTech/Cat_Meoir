using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookMovement : MonoBehaviour
{
    public float clampAngle;
    public float clampAngle2; 
    public float  mouseSpeedY;
    private float rotationX;
    private bool isController;
    public float controllerSpeedY;
    public InputManager inputManager;
 
    void Update()
    {

        //this part is for no controller - or basically just mouse based on comp-3 interactiv first person controller tutorial
        
      
        
        if (!isController)
        {
            rotationX -= Input.GetAxis("Mouse Y") * mouseSpeedY;
            rotationX = Mathf.Clamp(rotationX, -clampAngle2, clampAngle);
            this.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }
        else
        {
            //do the controller part 
            rotationX -= inputManager.GetMouseDelta().y * controllerSpeedY;
            rotationX = Mathf.Clamp(rotationX, -clampAngle2, clampAngle);
            this.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        }
        
        
      
        
       

    }
    private void OnEnable()
    {
        // Subscribe to events when the script is enabled
        ControllerManager.OnControllerConnected += ControllerConnect;
        ControllerManager.OnControllerDisconnected += ControllerDisconnect;
    }

    private void OnDisable()
    {
        // Unsubscribe from events when the script is disabled or destroyed
        ControllerManager.OnControllerConnected -= ControllerConnect;
        ControllerManager.OnControllerDisconnected -= ControllerDisconnect;
    }
    private void ControllerConnect()
    {
        isController = true; 
        // set default variable? 

    }
    private void ControllerDisconnect()
    {
        isController = false; 
        //set default variable? 
    }
}
