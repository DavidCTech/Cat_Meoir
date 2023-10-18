using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlackAndWhiteToggle : MonoBehaviour
{
    public GameObject player; // Assign the player GameObject in the Inspector

    private PlayerVisionStates playerVisionStates; // Reference to the PlayerVisionStates script

    private Volume volume;
    private FilmGrain filmGrain;

    private void Start()
    {
        // Get the PlayerVisionStates script from the player GameObject
        playerVisionStates = player.GetComponent<PlayerVisionStates>();

        if (playerVisionStates == null)
        {
            Debug.LogError("PlayerVisionStates component not found on the player GameObject.");
        }

        // Find the Volume component on this GameObject
        volume = GetComponent<Volume>();

        if (volume == null)
        {
            Debug.LogError("Volume component not found on this GameObject.");
            return;
        }

        // Check if the Volume has a FilmGrain effect
        if (volume.profile.TryGet(out filmGrain))
        {
            // The FilmGrain component was found, you can proceed safely.
        }
        else
        {
            Debug.LogError("FilmGrain component not found in the Volume.");
        }
    }

    private void Update()
    {
        if (player != null && playerVisionStates != null && filmGrain != null)
        {
            // Check if the player is in the Vision state using the PlayerVisionStates script
            PlayerState playerState = playerVisionStates.currentState;

            // Check if the current object is in the "nopostprocessing" layer.
            bool isInNoPostProcessingLayer = gameObject.layer == LayerMask.NameToLayer("NoPostProcessing");

            // Determine whether to apply the black and white effect based on the player's state and object's layer.
            if (playerState == PlayerState.Vision && !isInNoPostProcessingLayer)
            {
                // Enable the FilmGrain effect for a black and white look
                filmGrain.intensity.value = 1.0f; // Adjust the intensity as needed
            }
            else
            {
                // Disable the FilmGrain effect for full color
                filmGrain.intensity.value = 0.0f;
            }
        }
    }
}