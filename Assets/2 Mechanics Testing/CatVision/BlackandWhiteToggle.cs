using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class BlackAndWhiteToggle : MonoBehaviour
{
    public PostProcessVolume postProcessVolume;
    private ColorGrading colorGrading;

    private bool isBlackAndWhite = false;

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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Toggle between color and black & white
            isBlackAndWhite = !isBlackAndWhite;

            // Set the saturation of the ColorGrading effect
            colorGrading.saturation.value = isBlackAndWhite ? -100 : 0; // -100 for full black & white, 0 for full color
        }
    }
}
