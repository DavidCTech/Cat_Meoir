using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlackAndWhiteToggle : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;

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

        // Find the player GameObject by tag (replace "Player" with your player's tag)
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            // Now that we have a reference to the player GameObject, we can check its vision state.
            PlayerVisionStates playerVisionStates = player.GetComponent<PlayerVisionStates>();
            if (playerVisionStates != null)
            {
                // Set the initial state based on the player's vision state.
                UpdateBlackAndWhiteState(playerVisionStates.currentState);
                Debug.Log("Player Vision State: " + playerVisionStates.currentState); // Add this line
            }
            else
            {
                Debug.LogError("PlayerVisionStates component not found on the player GameObject.");
            }
        }
        else
        {
            Debug.LogError("Player GameObject not found. Make sure it has the 'Player' tag.");
        }
    }

    private void Update()
    {
        // Check the player's vision state using the PlayerVisionStates script
        PlayerVisionStates playerVisionStates = GetComponent<PlayerVisionStates>();
        if (playerVisionStates != null && playerVisionStates.currentState == PlayerState.Vision)
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

    private void UpdateBlackAndWhiteState(PlayerState playerState)
    {
        // Set the saturation of the ColorGrading effect based on the player's vision state.
        if (playerState == PlayerState.Vision)
        {
            colorGrading.saturation.value = -100; // -100 for full black & white
        }
        else
        {
            colorGrading.saturation.value = 0; // 0 for full color
        }
    }
}