using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionObject : MonoBehaviour
{
    private Renderer objectRenderer;
    private Color originalColor;
    public Color visionColor = Color.yellow; // Set the color you want for the Vision state in the Inspector
    public GameObject player; // Assign the player GameObject in the Inspector

    private PlayerVisionStates playerVisionStates; // Reference to the PlayerVisionStates script

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;

        // Get the PlayerVisionStates script from the player GameObject
        playerVisionStates = player.GetComponent<PlayerVisionStates>();
    }

    private void Update()
    {
        if (player != null && playerVisionStates != null)
        {
            // Check if the player is in the Vision state using the currentState variable
            if (playerVisionStates.currentState == PlayerState.Vision)
            {
                // Show the object when in Vision state
                objectRenderer.enabled = true;
            }
            else
            {
                // Hide the object when not in Vision state
                objectRenderer.enabled = false;
            }
        }
    }
}