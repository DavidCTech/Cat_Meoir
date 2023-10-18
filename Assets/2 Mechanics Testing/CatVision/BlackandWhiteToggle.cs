using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlackAndWhiteToggle : MonoBehaviour
{
    public GameObject player; // Assign the player GameObject in the Inspector
    private PlayerVision playerVision; // Reference to the PlayerVision script

    public Volume postProcessingVolume; // Reference to the Volume component

    private Volume volume;
    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        // Get the PlayerVision script from the player GameObject
        playerVision = player.GetComponent<PlayerVision>();

        if (playerVision == null)
        {
            Debug.LogError("PlayerVision component not found on the player GameObject.");
            return;
        }

        // Find the Volume component on this GameObject
        volume = GetComponent<Volume>();

        if (volume == null)
        {
            Debug.LogError("Volume component not found on this GameObject.");
            return;
        }

        // Check if the Volume has a ColorAdjustments effect
        if (volume.profile.TryGet(out colorAdjustments))
        {
            // The ColorAdjustments component was found, you can proceed safely.
        }
        else
        {
            Debug.LogError("ColorAdjustments component not found in the Volume.");
        }
    }

    private void Update()
    {
        if (player != null && playerVision != null && colorAdjustments != null)
        {
            // Access the player's current state via the PlayerVision script
            PlayerState playerState = playerVision.CurrentState;

            // Determine whether to apply the black and white effect based on the player's state.
            if (playerState == PlayerState.Vision)
            {
                // Set the saturation of the ColorAdjustments effect for black and white
                colorAdjustments.saturation.value = -100; // -100 for full black & white
            }
            else
            {
                // Set the saturation back to 0 for full color
                colorAdjustments.saturation.value = 0;
            }
        }
    }
}