using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBoardCheck : MonoBehaviour
{
    public ClueBoard clueBoard; // Reference to the ClueBoard script attached to the clue board
    public PlayerMovement playerMovement; // Reference to the PlayerMovement script
    public PlayerManager playerManager; // Reference to the PlayerManager script

    private bool isNearClueBoard = false; // Track if the player is near the clue board

    void Update()
    {
        // Check for player input to interact with the clue board
        if (isNearClueBoard && Input.GetKeyDown(KeyCode.E))
        {
            // Toggle the clue board
            ToggleClueBoard();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player is near the clue board
        if (other.CompareTag("ClueBoard"))
        {
            isNearClueBoard = true;
            // Disable player movement when near the clue board
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
        // Check if the player moves away from the clue board
        if (other.CompareTag("ClueBoard"))
        {
            isNearClueBoard = false;
            // Enable player movement when away from the clue board
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

    void ToggleClueBoard()
    {
        // Toggle the clue board activation
        if (clueBoard != null)
        {
            if (clueBoard.isActiveAndEnabled)
            {
                clueBoard.DeactivateClueBoard();
            }
            else
            {
                clueBoard.ActivateClueBoard();
            }
        }
    }
}
