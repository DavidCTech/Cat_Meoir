using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script goes on any clue object that appears to change color in cat vision 
public class ClueObjectColor : MonoBehaviour
{
    [Header("Set the color you want for the Vision state.")]
    public Color visionColor = Color.yellow;

    private Renderer objectRenderer;
    private Color[] originalColors;
    private bool isInVisionMode = false;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColors = new Color[objectRenderer.materials.Length];

        // Store original colors
        for (int i = 0; i < originalColors.Length; i++)
        {
            originalColors[i] = objectRenderer.materials[i].color;
        }

        // Initially hide the object
        SetVisibility(false);
    }

    public void SetInVisionMode(bool inVisionMode)
    {
        isInVisionMode = inVisionMode;

        if (isInVisionMode)
        {
            ColorChange();
        }
        else
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

    public void ColorChange()
    {
        // Change color for all materials
        for (int i = 0; i < objectRenderer.materials.Length; i++)
        {
            objectRenderer.materials[i].color = visionColor;
        }

        // Show the object
        SetVisibility(true);
    }

    public void ColorRevert()
    {
        // Revert colors for all materials
        for (int i = 0; i < objectRenderer.materials.Length; i++)
        {
            objectRenderer.materials[i].color = originalColors[i];
        }

        // Hide the object
        SetVisibility(false);
    }

    private void SetVisibility(bool visible)
    {
        objectRenderer.enabled = visible;
    }
}