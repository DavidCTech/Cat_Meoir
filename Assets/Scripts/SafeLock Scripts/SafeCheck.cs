using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeCheck : MonoBehaviour
{
    public SafeInteraction safeInteraction; // Reference to the SafeInteraction script attached to the safe
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script
    public PlayerManager playerManager; // Reference to the PlayerManager script

    private bool isNearSafe = false; // Track if the player is near the safe

    void Update()
    {
        // Check for player input to interact with the safe
        if (isNearSafe && Input.GetKeyDown(KeyCode.E))
        {
            // Start the safe interaction
            StartSafeInteraction();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player is near the safe
        if (other.CompareTag("Safe"))
        {
            isNearSafe = true;
            // Disable player movement when near the safe
            if (playerMovement != null)
            {
                playerMovement.enabled = false;
            }
            if (playerManager != null)
            {
                playerManager.enabled = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player moves away from the safe
        if (other.CompareTag("Safe"))
        {
            isNearSafe = false;
            // Enable player movement when away from the safe
            if (playerMovement != null)
            {
                playerMovement.enabled = true;
            }
            if (playerManager != null)
            {
                playerManager.enabled = true;
            }
        }
    }

    void StartSafeInteraction()
    {
        // Start the safe interaction
        safeInteraction.StartInteraction();
    }
}
