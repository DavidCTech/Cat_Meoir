using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlackAndWhiteToggle : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;

    public GameObject player; // Assign the player GameObject in the Inspector

    private PlayerVisionStates playerVisionStates; // Reference to the PlayerVisionStates script

    private void Start()
    {
        // Get the ColorGrading effect from the Post-Processing Volume
        if (postProcessVolume.profile.TryGetSettings(out colorGrading))
        {
            // The ColorGrading component was found, you can proceed safely.
        }
        else
        {
            Debug.LogError("ColorGrading component not found in the Post-Processing Volume.");
        }

        // Get the PlayerVisionStates script from the player GameObject
        playerVisionStates = player.GetComponent<PlayerVisionStates>();

        if (playerVisionStates == null)
        {
            Debug.LogError("PlayerVisionStates component not found on the player GameObject.");
        }
    }

    private void Update()
    {
        if (player != null && playerVisionStates != null)
        {
            // Check if the player is in the Vision state using the PlayerVisionStates script
            PlayerState playerState = playerVisionStates.currentState;

            // Check if the current object is in the "nopostprocessing" layer.
            bool isInNoPostProcessingLayer = gameObject.layer == LayerMask.NameToLayer("NoPostProcessing");

            // Determine whether to apply the black and white effect based on the player's state and object's layer.
            if (playerState == PlayerState.Vision && !isInNoPostProcessingLayer)
            {
                // Set the saturation of the ColorGrading effect for black and white
                colorGrading.saturation.value = -100; // -100 for full black & white
            }
            else
            {
                // Set the saturation back to 0 for full color
                colorGrading.saturation.value = 0;
            }
        }
    }
}