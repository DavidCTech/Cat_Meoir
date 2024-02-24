using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoxMover : MonoBehaviour
{
    private GameObject currentBox;
    private Vector3 offset;
    private Quaternion initialRotation;
    public bool isGrabbing = false;

    void OnEnable()
    {
        InputAction boxPushAction = GetComponent<PlayerInput>().actions["BoxPush"];
        boxPushAction.started += ctx => OnBoxPush();
        boxPushAction.canceled += ctx => ReleaseBox();
    }

    void OnDisable()
    {
        InputAction boxPushAction = GetComponent<PlayerInput>().actions["BoxPush"];
        boxPushAction.started -= ctx => OnBoxPush();
        boxPushAction.canceled -= ctx => ReleaseBox();
    }

    void FixedUpdate()
    {
        //player is grabbing the box, move the box with the player
        if (isGrabbing && currentBox != null)
        {
            Vector3 targetPos = transform.position + offset;

            // Freeze the y position
            targetPos.y = currentBox.transform.position.y;

            currentBox.GetComponent<Rigidbody>().MovePosition(targetPos);

            // Lock the rotation of the box to the initial rotation
            currentBox.transform.rotation = initialRotation;
        }
    }

    void OnBoxPush()
    {
        if (!isGrabbing && currentBox != null)
        {
            isGrabbing = true;
            offset = currentBox.transform.position - transform.position;
            initialRotation = currentBox.transform.rotation; // Store the initial rotation of the box
        }
    }

    void ReleaseBox()
    {
        isGrabbing = false;
    }

    bool isCollidingWithBox()
    {
        // Check if the player is colliding with a box
        return currentBox != null && currentBox.GetComponent<Collider>().bounds.Intersects(GetComponent<Collider>().bounds);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player collided with a box
        if (collision.gameObject.CompareTag("Box") && !isGrabbing)
        {
            currentBox = collision.gameObject;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Reset current box reference when the player leaves the box collider
        if (collision.gameObject == currentBox && !isGrabbing)
        {
            currentBox = null;
        }
    }
}