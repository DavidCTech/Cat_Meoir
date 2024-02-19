using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeInteraction : MonoBehaviour
{
    public Transform dialTransform; // Reference to the transform of the lock dial
    public GameObject closedSafeModel; // Reference to the closed safe model
    public GameObject openSafeModel; // Reference to the open safe model
    public float lockRotationSpeed = 5f; // Speed of dial rotation during minigame
    public float rotationSpeed = 50f; // Speed of dial rotation based on input
    public float rotationThreshold = 90f; // Rotation angle threshold to unlock the safe

    private bool minigameActive = false;
    private bool safeUnlocked = false; // Track if the safe is unlocked

    private void Start()
    {
        // Start minigame
        minigameActive = true;
    }

    private void Update()
    {
        if (minigameActive && !safeUnlocked)
        {
            // Rotate the dial
            RotateDial();

            // Check if the dial rotation angle reaches the threshold
            if (dialTransform.localRotation.eulerAngles.z >= rotationThreshold)
            {
                // Open the safe
                SafeUnlocked();
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
    }
}
