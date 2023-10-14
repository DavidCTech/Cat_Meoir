using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody rb;

    public float moveSpeed = 5;
    public float visionMoveSpeed = 1; // Speed during Vision state
    private bool sprinting = false; // Flag to track if the player is sprinting
    public float rotationSpeed = 15;
    public bool isFrozen = false;
    private PlayerVisionStates visionStates;

    public float gravity = 9.81f; // Default gravity value

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        visionStates = GetComponent<PlayerVisionStates>();
    }

    void FixedUpdate()
    {
        // Apply custom gravity in the negative y direction
        Vector3 customGravity = new Vector3(0, -gravity * rb.mass, 0);
        rb.AddForce(customGravity, ForceMode.Force);
    }

    public void ManageAllMovement()
    {
        ManageMovement();
        ManageRotation();
    }

    public void OnSprint()
    {
        sprinting = !sprinting;

        if (sprinting)
        {
            // If sprinting, increase the player's speed
            moveSpeed = moveSpeed + 3;
        }
        else
        {
            moveSpeed = moveSpeed - 3;
        }
    }

    private void ManageMovement()
    {
        if (!isFrozen)
        {
            float currentMoveSpeed = visionStates.currentState == PlayerState.Vision ? visionMoveSpeed : moveSpeed;

            moveDirection = cameraObject.forward * inputManager.vInput;
            moveDirection = moveDirection + cameraObject.right * inputManager.hInput;
            moveDirection.Normalize();
            moveDirection.y = 0;
            moveDirection = moveDirection * currentMoveSpeed;

            Vector3 movementVelocity = moveDirection;
            rb.velocity = movementVelocity;
        }
    }

    private void ManageRotation()
    {
        if (!isFrozen)
        {
            Vector3 targetDirection = Vector3.zero;

            targetDirection = cameraObject.forward * inputManager.vInput;
            targetDirection = moveDirection + cameraObject.right * inputManager.hInput;
            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
                targetDirection = transform.forward;

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = playerRotation;
        }
    }
}
