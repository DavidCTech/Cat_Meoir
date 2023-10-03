using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionObjectColor : MonoBehaviour
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
                // Delay the color change by one frame
                StartCoroutine(ChangeColorAfterFrame());
            }
            else
            {
                // Restore the object's original color
                objectRenderer.material.color = originalColor;
            }
        }
    }

    private IEnumerator ChangeColorAfterFrame()
    {
        // Wait for one frame to ensure that the layer mask is updated
        yield return null;

        // Change the object's color to the vision color
        objectRenderer.material.color = visionColor;
    }
}