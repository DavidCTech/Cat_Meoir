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
    public GameObject camTransform; //game object of cam transform  

    public float moveSpeed = 5;
    public float defaultSpeed = 5; 
    public float sprintSpeedIncrease = 4;
    public float sneakSpeed = 3; 
    public float sneakSpeedIncrease = 1; 
    public float visionMoveSpeed = 1;
    public bool sprinting = false;
    public float rotationSpeed = 15;
    public bool isFrozen = false;
    public float acceleration = 0.08f; 
    public float currentMoveSpeed = 5;
    private float maxMoveSpeed; 

    public float gravity = 9.81f; // Default gravity value
    public float jumpForce = 1.0f; // Force applied when jumping
    public float jumpTime = 0.5f; // The time it takes for the jump force to be applied
    private bool isJumping = false;
    public float groundDistance = 0.2f; // The distance to check for ground
    public LayerMask groundLayer; // The layer for the ground objects

    public bool isGrounded = true; // Indicates if the player is grounded
    public bool isFirst = false;
    public float mouseSpeedX;
    public float controllerSpeedX;

    public PlayerStealth playerStealth; 


    private bool isController = false; 
    private PlayerVision playerVision;
    private BoxMover boxMover;


   

    //anim
    public Animator anim;

    private float velocitySpeed;
    private float velocityUp;

    ObjectGrabber objectGrabber;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        // currentMoveSpeed = moveSpeed;
        maxMoveSpeed = moveSpeed; 

        // Set the reference to the PlayerVision script
        playerVision = GetComponent<PlayerVision>();

        // Create an instance of ObjectGrabber and set up input action callbacks
        objectGrabber = gameObject.AddComponent<ObjectGrabber>();

        // Assuming you have a PlayerInput component attached to the player GameObject
        PlayerInput playerInput = GetComponent<PlayerInput>();

        boxMover = GetComponent<BoxMover>();

        if (playerInput != null)
        {
            InputAction boxPushAction = playerInput.actions.FindAction("BoxPush");
            if (boxPushAction != null)
            {
                boxPushAction.performed += objectGrabber.UpdateGrab;
                boxPushAction.canceled += objectGrabber.UpdateGrab;
            }
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
    }
    private void ControllerDisconnect()
    {
        isController = false; 
    }
    private float ClampValue(float inputValue, float minValue, float maxValue)
    {

        // Clamp inputValue between minValue and maxValue
        float clampedValue = Mathf.Clamp(inputValue, minValue, maxValue);

        // Scale the clampedValue to the range [0, 1]
        float scaledValue = (clampedValue - minValue) / (maxValue - minValue);

        return scaledValue;
    }

    void FixedUpdate()
    {
        // Apply custom gravity in the negative y direction
        Vector3 customGravity = new Vector3(0, -gravity * rb.mass, 0);
        rb.AddForce(customGravity, ForceMode.Force);

        // Adjust the currentMoveSpeed based on the player state from PlayerVision
        if (playerVision != null)
        {
            if (playerVision.CurrentState == PlayerState.Normal)
            {
               // currentMoveSpeed = moveSpeed;
                maxMoveSpeed = moveSpeed; 
            }
            else
            {
                //currentMoveSpeed = visionMoveSpeed;
                maxMoveSpeed = visionMoveSpeed; 
            }
        }
        //rotation 
        ManageRotation();
        // Perform the ground check
        CheckGrounded();

        //anim work 
        if(anim != null)
        {
            float velocitySpeedValue = ClampValue(rb.velocity.magnitude, 0f, moveSpeed + 3f); 
            float velocityUpValue = ClampValue(Mathf.Abs(rb.velocity.y), 0f, 0.1f);
            anim.SetFloat("VelocitySpeed", velocitySpeedValue);
            anim.SetFloat("VelocityUp", velocityUpValue); 
        }
    }

    public void SetPlayerState(PlayerState state)
    {
        if (playerVision != null)
        {
            playerVision.SetCurrentState(state);
        }
    }

    private void CheckGrounded()
    {
        
        // Increase the ray length (e.g., to 2.0f)
        float rayLength = 1.1f;

        // Create a ray from the player's bottom to check for ground
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);

        // Visualize the ray in the scene view during play mode with red color
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, rayLength, groundLayer))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }


    public void ManageAllMovement()
    {
        ManageMovement();
       
    }

    public void OnSprint()
    {
        sprinting = !sprinting;

        if (playerStealth.isStealth)
        {
            if (!sprinting)
            {
                moveSpeed = sneakSpeed;
            }
            else
            {
                moveSpeed = sneakSpeed + sneakSpeedIncrease; 
            }
                     
        }
        else
        {
            if (!sprinting)
            {
                moveSpeed = defaultSpeed;
            }
            else
            {
                moveSpeed = defaultSpeed+ sprintSpeedIncrease;
            }

        }
    }

    public void ToggleVisionSpeed()
    {
        //currentMoveSpeed = visionMoveSpeed;

        maxMoveSpeed = visionMoveSpeed; 
    }

    public void ToggleNormalSpeed()
    {
        //currentMoveSpeed = moveSpeed;
        maxMoveSpeed = moveSpeed; 
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
    public void OnJump()
    {
        if (!isFrozen && isGrounded && !isJumping) // Check if the player is not frozen, is grounded, and not already jumping
        {
            StartCoroutine(ApplyJumpForce());
        }
    }

    private IEnumerator ApplyJumpForce()
    {
        isJumping = true;
        float jumpTimer = 0;

        while (jumpTimer < jumpTime)
        {
            float normalizedTime = jumpTimer / jumpTime;
            float jumpForceToApply = Mathf.Lerp(0, jumpForce, normalizedTime);
            rb.AddForce(Vector3.up * jumpForceToApply, ForceMode.VelocityChange);

            jumpTimer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        isJumping = false;
    }


    private void ManageMovement()
    {
        if (!isFrozen)
        {
            moveDirection = cameraObject.forward * inputManager.vInput;
            moveDirection = moveDirection + cameraObject.right * inputManager.hInput;
            moveDirection.Normalize();
            moveDirection.y = 0;
            //muiltiply the current speed by an accelerator value 
            //clamp the current move speed by the max speed 
            if(currentMoveSpeed < maxMoveSpeed)
            {
                currentMoveSpeed = currentMoveSpeed + acceleration;
            }
            if(currentMoveSpeed > maxMoveSpeed)
            {
                currentMoveSpeed = currentMoveSpeed - acceleration; 
            }
            
            
            currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, 0f, moveSpeed + sprintSpeedIncrease);

            moveDirection = moveDirection * currentMoveSpeed;

            Vector3 movementVelocity = moveDirection;
            rb.velocity = new Vector3(movementVelocity.x, rb.velocity.y, movementVelocity.z);
        }
        //freeze player? 
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
   
    private void ManageRotation()
    {
        if(!boxMover.isGrabbing)
        {
            if (!isFrozen)
            {
                if (!isFirst)
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
    }
    private void Update()
    {
  
      
        FirstPersonRotation();
    }
    private void FirstPersonRotation()
    {
         if(isFrozen)
        {
            if (isFirst)
            {
                if (camTransform != null)
                {
                    //this part is for no controller - or basically just mouse based on comp-3 interactiv first person controller tutorial

                    if (!isController)
                    {

                        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * mouseSpeedX, 0);
                    }
                    else
                    {
                        transform.rotation *= Quaternion.Euler(0, inputManager.GetMouseDelta().x * controllerSpeedX, 0);
                    }
                }
            }
        }
    }
}