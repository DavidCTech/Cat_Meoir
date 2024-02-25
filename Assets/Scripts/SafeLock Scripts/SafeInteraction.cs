using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeInteraction : MonoBehaviour
{
    public Transform dialTransform; // Reference to the transform of the lock dial
    public GameObject closedSafeModel; // Reference to the closed safe model
    public GameObject openSafeModel; // Reference to the open safe model
    public Camera safeCamera; // Reference to the safe camera
    public float rotationSpeed = 50f; // Speed of dial rotation based on input

    [SerializeField]
    private int[] unlockPositions = new int[3]; // Array to hold the unlock positions as integers

    private float[] unlockPositionsFloat; // Array to hold the unlock positions as floats
    private bool minigameActive = false;
    private bool safeUnlocked = false; // Track if the safe is unlocked

    private int rotationIndex = 0; // Track the current rotation position index

    private void Start()
    {
        // Initially deactivate the SafeInteraction script
        enabled = false;

        // Convert integer unlock positions to floats
        unlockPositionsFloat = new float[unlockPositions.Length];
        for (int i = 0; i < unlockPositions.Length; i++)
        {
            unlockPositionsFloat[i] = (float)unlockPositions[i];
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

        // Switch back to the player's main camera
        // Assuming the player has a Camera component attached to it
        Camera.main.gameObject.SetActive(true);

        // Enable player movement
        // Assuming the player has a PlayerMovement script attached to it
        GetComponent<PlayerMovement>().enabled = true;
    }

    // Method to start the safe interaction
    public void StartInteraction()
    {
        minigameActive = true;
        enabled = true; // Activate the SafeInteraction script

        // Deactivate the player's main camera
        Camera.main.gameObject.SetActive(false);

        // Activate the safe camera
        safeCamera.gameObject.SetActive(true);

        // Disable player movement
        // Assuming the player has a PlayerMovement script attached to it
        GetComponent<PlayerMovement>().enabled = false;
    }
}
