using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int rotationIndex = 0; // Track the current rotation position index

    public PlayerMovement playerMovementScript; // Reference to the player's movement script
    public PlayerManager playerManagerScript; // Reference to the player manager script
    public Camera mainCamera; // Reference to the main camera

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
            // Rotate the dial
            RotateDial();

            // Check if the dial rotation matches the current unlock position
            if (Mathf.Abs(dialTransform.localRotation.eulerAngles.z - unlockPositionsFloat[rotationIndex]) < 1f)
            {
                // Move to the next unlock position
                rotationIndex++;

                // If reached the last unlock position, unlock the safe
                if (rotationIndex >= unlockPositionsFloat.Length)
                {
                    SafeUnlocked();
                }
                else
                {
                    // Debug message when reaching each unlock position
                    Debug.Log("Dial reached position " + (rotationIndex + 1));
                }
            }
        }
    }

    private void RotateDial()
    {
        // Get the input value for horizontal axis
        float input = Input.GetAxis("Horizontal");

        // Calculate the rotation amount based on the input value
        float rotationAmount = input * rotationSpeed * Time.deltaTime;

        // Apply rotation to the dial around the z-axis
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
        minigameActive = true;
        enabled = true; // Activate the SafeInteraction script

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

    // Method to exit the safe minigame
    public void ExitMinigame()
    {
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
        if (playerManagerScript != null)
        {
            playerManagerScript.enabled = true;
        }
        else
        {
            Debug.LogWarning("PlayerManager script reference not set.");
        }
    }
}
