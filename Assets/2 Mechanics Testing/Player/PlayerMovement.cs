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
    public float visionMoveSpeed = 1;
    private bool sprinting = false;
    public float rotationSpeed = 15;
    public bool isFrozen = false;
    private float currentMoveSpeed;

    public float gravity = 9.81f; // Default gravity value
    public float jumpForce = 10.0f; // Force applied when jumping
    public float groundDistance = 0.2f; // The distance to check for ground
    public LayerMask groundLayer; // The layer for the ground objects

    private bool isGrounded = true; // Indicates if the player is grounded

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        currentMoveSpeed = moveSpeed;
    }

    void FixedUpdate()
    {
        // Apply custom gravity in the negative y direction
        Vector3 customGravity = new Vector3(0, -gravity * rb.mass, 0);
        rb.AddForce(customGravity, ForceMode.Force);
        currentMoveSpeed = moveSpeed;

        // Perform the ground check
        CheckGrounded();
    }

    private void CheckGrounded()
    {
        // Create a ray from the player's position slightly above them to check for ground
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.2f, groundLayer))
        {
            // The player is grounded
            Debug.Log("Grounded");
        }
        else
        {
            // The player is not grounded
            Debug.Log("Not Grounded");
        }
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
            moveSpeed = moveSpeed + 3;
        }
        else
        {
            moveSpeed = moveSpeed - 3;
        }
    }

    public void ToggleVisionSpeed()
    {
        currentMoveSpeed = visionMoveSpeed;
    }

    public void ToggleNormalSpeed()
    {
        currentMoveSpeed = moveSpeed;
    }

    public void OnJump()
    {
        if (!isFrozen && isGrounded) // Check if the player is not frozen and is grounded
        {
            Debug.Log("Jumping");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void ManageMovement()
    {
        if (!isFrozen)
        {
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