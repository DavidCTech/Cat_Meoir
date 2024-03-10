using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SafeInteraction : MonoBehaviour
{
    public Transform dialTransform; // Reference to the transform of the lock dial
    public GameObject closedSafeModel; // Reference to the closed safe model
    public GameObject openSafeModel; // Reference to the open safe model
    public Camera safeCamera; // Reference to the safe camera
    public GameObject dialGameObject; // Reference to the dial GameObject
    public float rotationSpeed = 50f; // Speed of dial rotation based on input

    [SerializeField]
    private int[] unlockPositions = new int[3]; // Array to hold the unlock positions as integers

    private float[] unlockPositionsFloat; // Array to hold the unlock positions as floats
    private bool minigameActive = false;
    private bool safeUnlocked = false; // Track if the safe is unlocked
    private bool safeAlreadyUnlocked = false; // Track if the safe has already been unlocked
    public GameObject objectToActivateOnUnlock; // Object to activate when the safe unlocks (optional)

    private enum TurnDirection { None, Left, Right }

    private TurnDirection lastTurnDirection = TurnDirection.None;
    private TurnDirection[] expectedTurnSequence = { TurnDirection.Right, TurnDirection.Left, TurnDirection.Right };
    private int currentExpectedTurnIndex = 0;

    private int rotationIndex = 0; // Track the current rotation position index

    public PlayerMovement playerMovementScript; // Reference to the player's movement script
    public PlayerManager playerManagerScript; // Reference to the player manager script
    public Camera mainCamera; // Reference to the main camera

    private bool isInteracting = false; // Track if the player is currently interacting with the safe


    private void Start()
    {
        // Convert integer unlock positions to floats
        unlockPositionsFloat = new float[unlockPositions.Length];
        for (int i = 0; i < unlockPositions.Length; i++)
        {
            unlockPositionsFloat[i] = (float)unlockPositions[i];
        }

        if (playerMovementScript == null)
        {
            Debug.LogError("PlayerMovement script reference not set.");
        }

        if (mainCamera == null)
        {
            Debug.LogError("Main camera reference not set.");
        }

        if (playerManagerScript == null)
        {
            Debug.LogError("PlayerManager script reference not set.");
        }
    }

    private void Update()
    {
        if (minigameActive && !safeUnlocked)
        {
            RotateDial();

            // Check if the dial rotation matches the current unlock position and direction
            if (Mathf.Abs(dialTransform.localRotation.eulerAngles.z - unlockPositionsFloat[currentExpectedTurnIndex]) < 1f &&
                lastTurnDirection == expectedTurnSequence[currentExpectedTurnIndex])
            {
                currentExpectedTurnIndex++;
                lastTurnDirection = TurnDirection.None; // Reset last turn direction

                if (currentExpectedTurnIndex >= expectedTurnSequence.Length)
                {
                    SafeUnlocked();
                }
                else
                {
                    Debug.Log("Correct turn. Next expected turn: " + expectedTurnSequence[currentExpectedTurnIndex]);
                }
            }
        }
    }

    private void RotateDial()
    {
        float input = Input.GetAxis("Horizontal");

        // Determine the direction of the turn
        TurnDirection currentTurnDirection = TurnDirection.None;
        if (input > 0)
        {
            currentTurnDirection = TurnDirection.Right;
        }
        else if (input < 0)
        {
            currentTurnDirection = TurnDirection.Left;
        }

        // Update last turn direction
        lastTurnDirection = currentTurnDirection;

        float rotationAmount = input * rotationSpeed * Time.deltaTime;
        dialTransform.Rotate(Vector3.forward, rotationAmount);
    }

    private void SafeUnlocked()
    {
        Debug.Log("Safe unlocked!");

        // Deactivate the closed safe model
        closedSafeModel.SetActive(false);

        // Activate the open safe model
        openSafeModel.SetActive(true);

        // Set the safe as unlocked
        safeUnlocked = true;

        // Set the flag to indicate the safe has been unlocked
        safeAlreadyUnlocked = true;

        // Disable minigame
        minigameActive = false;

        // Deactivate the safe camera
        safeCamera.gameObject.SetActive(false);

        // Deactivate the dial GameObject
        dialGameObject.SetActive(false);

        // Activate the player's main camera
        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Main camera reference not set.");
        }

        // Activate the specified object if it's assigned
        if (objectToActivateOnUnlock != null)
        {
            objectToActivateOnUnlock.SetActive(true);
        }

        // Enable player movement
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }
        else
        {
            Debug.LogWarning("PlayerMovement script reference not set.");
        }
        // Enable player manager
        if (playerManagerScript != null)
        {
            playerManagerScript.enabled = true;
        }
        else
        {
            Debug.LogWarning("PlayerManager script reference not set.");
        }
    }

    // Method to start the safe interaction
    public void StartInteraction()
    {
        // Check if the safe has already been unlocked
        if (!safeAlreadyUnlocked)
        {
            minigameActive = true;
            enabled = true; // Activate the SafeInteraction script
            isInteracting = true; // Set the flag to indicate that the player is now interacting with the safe

            // Activate the dial GameObject
            dialGameObject.SetActive(true);

            // Deactivate the player's main camera
            if (mainCamera != null)
            {
                mainCamera.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Main camera reference not set.");
            }

            // Activate the safe camera
            safeCamera.gameObject.SetActive(true);

            // Disable player movement
            if (playerMovementScript != null)
            {
                playerMovementScript.enabled = false;
            }
            else
            {
                Debug.LogWarning("PlayerMovement script reference not set.");
            }
            // Disable player manager
            if (playerManagerScript != null)
            {
                playerManagerScript.enabled = false;
            }
            else
            {
                Debug.LogWarning("PlayerManager script reference not set.");
            }
        }
    }

    // Method to exit the safe minigame
    public void ExitMinigame()
    {
        // Exit the safe interaction if currently interacting
        if (isInteracting)
        {
            isInteracting = false; // Reset the flag to indicate that the player has stopped interacting with the safe
            return; // Skip further processing to maintain the interaction state
        }

        // Reset any necessary variables or states
        minigameActive = false;
        enabled = false; // Deactivate the SafeInteraction script
        dialGameObject.SetActive(false); // Deactivate the dial GameObject

        // Deactivate the safe camera
        safeCamera.gameObject.SetActive(false);

        // Activate the player's main camera
        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Main camera reference not set.");
        }

        // Enable player movement
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }
        else
        {
            Debug.LogWarning("PlayerMovement script reference not set.");
        }

        // Enable player manager
        if (playerManagerScript != null)
        {
            playerManagerScript.enabled = true;
        }
        else
        {
            Debug.LogWarning("PlayerManager script reference not set.");
        }
    }

    public bool IsSafeUnlocked()
    {
        return safeAlreadyUnlocked;
    }

    public bool IsMinigameActive()
    {
        return minigameActive;
    }
}
