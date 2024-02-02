using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ObjectGrabber : MonoBehaviour
{
    private bool isGrabbing = false;
    private GrabbableObject grabbedObject;
    private Vector3 grabOffset;
    private bool previousKinematicState;
    private InputAction boxPushAction;

    private void OnEnable()
    {
        // Enable the input action when the script is enabled
        boxPushAction.Enable();
    }

    private void OnDisable()
    {
        // Disable the input action when the script is disabled
        boxPushAction.Disable();
    }

    private void Awake()
    {
        // Assign the input action in the Awake method or through the Unity Editor
        boxPushAction = new InputAction("BoxPush", binding: "<Mouse>/leftButton");
        boxPushAction.performed += context => UpdateGrab(context);
    }
    public void UpdateGrab(InputAction.CallbackContext context)
    {
        if (context.started && !isGrabbing)
        {
            Debug.Log("Button Hold Started");
        }
        else if (context.canceled && isGrabbing)
        {
            Debug.Log("Button Hold Released");
            ReleaseObject();
        }

        if (isGrabbing)
        {
            Debug.Log("Dragging Object");
            DragObject();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an object with the GrabbableObject script
        GrabbableObject grabbableObject = collision.gameObject.GetComponent<GrabbableObject>();
        if (grabbableObject != null)
        {
            TryGrabObject(grabbableObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the collision is with the previously grabbed object
        GrabbableObject grabbableObject = collision.gameObject.GetComponent<GrabbableObject>();
        if (grabbableObject != null && grabbableObject == grabbedObject)
        {
            ReleaseObject();
        }
    }

    private void TryGrabObject(GrabbableObject grabbableObject)
    {
        // Check if the object has a Rigidbody and the button is pressed
        Rigidbody objectRb = grabbableObject.GetComponent<Rigidbody>();
        if (objectRb != null && Mouse.current.leftButton.isPressed)
        {
            Debug.Log("Trying to grab the box.");
            isGrabbing = true;
            grabbedObject = grabbableObject;
            previousKinematicState = objectRb.isKinematic;

            // Temporarily set isKinematic to false
            grabbedObject.SetKinematic(false);

            grabOffset = grabbedObject.transform.position - transform.position;

            // Disable player movement and rotation
            GetComponent<PlayerMovement>().enabled = false;
        }
    }

    private void ReleaseObject()
    {
        isGrabbing = false;

        // Revert isKinematic to the previous state
        if (grabbedObject != null)
        {
            grabbedObject.SetKinematic(previousKinematicState);
        }

        // Enable player movement and rotation
        GetComponent<PlayerMovement>().enabled = true;

        grabbedObject = null;
    }

    private void DragObject()
    {
        if (grabbedObject != null)
        {
            Vector3 targetPosition = transform.position + grabOffset;

            // Keep only vertical movement
            targetPosition.y = grabbedObject.transform.position.y;

            grabbedObject.transform.position = targetPosition;
        }
    }
}
