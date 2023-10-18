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
    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }
    //method for changing the color
    public void ColorChange()
    {
        StartCoroutine(ChangeColorAfterFrame());
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