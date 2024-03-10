using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SafeCheck : MonoBehaviour
{
    public SafeInteraction safeInteraction; // Reference to the SafeInteraction script attached to the safe
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script
    public PlayerManager playerManager; // Reference to the PlayerManager script

    private bool isNearSafe = false; // Track if the player is near the safe
    private bool isInteractingWithSafe = false; // Track if the player is currently interacting with the safe

    public void HandleInteraction()
    {
        // Check for player input to interact with the safe
        if (isNearSafe)
        {
            // Toggle the safe interaction
            ToggleSafeInteraction();
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

    public void ToggleSafeInteraction()
    {
        // Toggle the interaction with the safe
        if (isInteractingWithSafe)
        {
            ExitSafeInteraction();
        }
        else
        {
            StartSafeInteraction();
        }
    }

    public void StartSafeInteraction()
    {
        // Start the safe interaction if the safe is not already unlocked
        if (!safeInteraction.IsSafeUnlocked())
        {
            isInteractingWithSafe = true; // Set the flag to indicate that the player is now interacting with the safe
            safeInteraction.StartInteraction();
        }
    }

    public void ExitSafeInteraction()
    {
        // Exit the safe interaction
        isInteractingWithSafe = false; // Reset the flag to indicate that the player has stopped interacting with the safe
        safeInteraction.ExitMinigame();
    }
}
