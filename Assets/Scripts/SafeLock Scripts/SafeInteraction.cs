using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField]
    private int[] failPositions = new int[3]; // Array to hold the fail positions as integers

    public Image[] correctPositionImages; // Array to hold the UI images for correct positions

    private float[] unlockPositionsFloat; // Array to hold the unlock positions as floats
    private float[] failPositionsFloat; // Array to hold the fail positions as floats
    private bool minigameActive = false;
    private bool safeUnlocked = false; // Track if the safe is unlocked
    private bool safeAlreadyUnlocked = false; // Track if the safe has already been unlocked
    public GameObject objectToActivateOnUnlock; // Object to activate when the safe unlocks (optional)

    public AudioSource audioSource; // Reference to the AudioSource component
    public AudioClip unlockSound; // Audio clip to play when reaching a position
    public AudioClip failSound; // Audio clip to play when the player hits a fail state

    private enum TurnDirection { None, Left, Right }

    private TurnDirection lastTurnDirection = TurnDirection.None;
    private TurnDirection[] expectedTurnSequence = { TurnDirection.Right, TurnDirection.Left, TurnDirection.Right };
    private int currentExpectedTurnIndex = 0;

    private int rotationIndex = 0; // Track the current rotation position index

    public PlayerMovement playerMovementScript; // Reference to the player's movement script
    public PlayerManager playerManagerScript; // Reference to the player manager script
    public Camera mainCamera; // Reference to the main camera

    private bool isInteracting = false; // Track if the player is currently interacting with the safe

    public TMP_Text instructionsText; // Reference to the TextMeshProUGUI component for instructions
    public TMP_Text rotationText; // Reference to the TextMeshProUGUI component for displaying rotation

    private float lastRotationZ = 0f; // Track the previous rotation value

    private void Start()
    {
        // Convert integer unlock positions to floats
        unlockPositionsFloat = new float[unlockPositions.Length];
        for (int i = 0; i < unlockPositions.Length; i++)
        {
            unlockPositionsFloat[i] = (float)unlockPositions[i];
        }

        // Convert integer fail positions to floats
        failPositionsFloat = new float[failPositions.Length];
        for (int i = 0; i < failPositions.Length; i++)
        {
            failPositionsFloat[i] = (float)failPositions[i];
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

        if (instructionsText == null)
        {
            Debug.LogError("Instructions Text reference not set.");
        }

        // Check if AudioSource is assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource reference not set.");
        }

        // Check if AudioClip is assigned
        if (unlockSound == null)
        {
            Debug.LogError("Unlock Sound reference not set.");
        }

        // Hide all correct position UI images at start
        foreach (Image image in correctPositionImages)
        {
            image.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (minigameActive && !safeUnlocked)
        {
            RotateDial();
            UpdateRotationText();

            // Check if the dial rotation matches the current unlock position and direction
            if (Mathf.Abs(dialTransform.localRotation.eulerAngles.z - unlockPositionsFloat[currentExpectedTurnIndex]) < 1f &&
                lastTurnDirection == expectedTurnSequence[currentExpectedTurnIndex])
            {
                // Player reached an unlock position
                PlayUnlockSound();
                currentExpectedTurnIndex++;
                lastTurnDirection = TurnDirection.None;

                if (currentExpectedTurnIndex >= expectedTurnSequence.Length)
                {
                    SafeUnlocked();
                }
                else
                {
                    Debug.Log("Correct turn. Next expected turn: " + expectedTurnSequence[currentExpectedTurnIndex]);
                }
                // Enable UI image for correct position
                correctPositionImages[currentExpectedTurnIndex].gameObject.SetActive(true);
            }

            // Check for fail state after reaching the first and second unlock positions
            if (currentExpectedTurnIndex == 0 || currentExpectedTurnIndex == 1 || currentExpectedTurnIndex == 2)
            {
                CheckFailState();
            }
        }
    }

    private void UpdateRotationText()
    {
        // Get the current rotation of the dial as a percentage of the full rotation
        float normalizedRotation = dialTransform.localRotation.eulerAngles.z / 360f; // Normalize rotation to range [0, 1]

        // Map the normalized rotation to the range of 0 to 100
        int currentRotationPercentage = Mathf.RoundToInt(normalizedRotation * 100);

        // Update the text to display the current rotation percentage
        rotationText.text = "Rotation: " + currentRotationPercentage.ToString();
    }

    // Method to play the unlock sound
    private void PlayUnlockSound()
    {
        if (audioSource != null && unlockSound != null)
        {
            audioSource.PlayOneShot(unlockSound);
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

        // Update last rotation value
        lastRotationZ = dialTransform.localRotation.eulerAngles.z;
    }

    private void CheckFailState()
    {
        // Check if the current dial rotation is within any of the fail positions
        foreach (float failPos in failPositionsFloat)
        {
            if (Mathf.Abs(dialTransform.localRotation.eulerAngles.z - failPos) < 1f)
            {
                // Player hits a fail state
                Debug.Log("Fail state reached. Player failed the minigame.");

                // Play the fail sound
                if (audioSource != null && failSound != null)
                {
                    audioSource.PlayOneShot(failSound);
                }

                // Reset UI images for correct positions
                foreach (Image image in correctPositionImages)
                {
                    image.gameObject.SetActive(false);
                }

                ResetMinigame(); // Restart the minigame
                return;
            }
        }
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

        // Hide instructions text
        if (instructionsText != null)
        {
            instructionsText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Instructions Text reference not set.");
        }
        // Deactivate the rotation text
        if (rotationText != null)
        {
            rotationText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Rotation Text reference not set.");
        }
        foreach (Image image in correctPositionImages)
        {
            image.gameObject.SetActive(false);
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
            // Display instructions text
            if (instructionsText != null)
            {
                instructionsText.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Instructions Text reference not set.");
            }
            // Activate the rotation text
            if (rotationText != null)
            {
                rotationText.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Rotation Text reference not set.");
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
        // Hide instructions text
        if (instructionsText != null)
        {
            instructionsText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Instructions Text reference not set.");
        }
        // Deactivate the rotation text
        if (rotationText != null)
        {
            rotationText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Rotation Text reference not set.");
        }
        foreach (Image image in correctPositionImages)
        {
            image.gameObject.SetActive(false);
        }
    }

    private void ResetMinigame()
    {
        // Reset all necessary variables and states to their initial values
        currentExpectedTurnIndex = 0;
        lastTurnDirection = TurnDirection.None;

        // Hide all correct position UI images
        foreach (Image image in correctPositionImages)
        {
            image.gameObject.SetActive(false);
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
