using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script goes on any clue object that appears to change color in cat vision 
public class ClueObjectColor : MonoBehaviour
{
    [Header("Set the color you want for the Vision state.")]
    public Color visionColor = Color.yellow;

    private Renderer objectRenderer;
    private Color originalColor;
    //start assigns the rendering and color
    private bool isInVisionMode = false;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }

    public void SetInVisionMode(bool inVisionMode)
    {
        isInVisionMode = inVisionMode;

        // If not in vision mode, revert to the original color
        if (!isInVisionMode)
        {
            ColorRevert();
        }
    }
    public void ActivateColor()
    {
        isInVisionMode = true;
        ColorChange();
    }

    public void DeactivateColor()
    {
        isInVisionMode = false;
        ColorRevert();
    }

    // Modify ColorChange to check for both vision mode and range
    public void ColorChange()
    {
        if (isInVisionMode)
        {
            objectRenderer.material.color = visionColor;
        }
    }
    //method for reverting the color
    public void ColorRevert()
    {
        objectRenderer.material.color = originalColor;
    }


    private IEnumerator ChangeColorAfterFrame()
    {
        // Wait for one frame to ensure that the layer mask is updated
        yield return null;

        // Change the object's color to the vision color
        objectRenderer.material.color = visionColor;
    }
}