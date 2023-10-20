using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.PostProcessing.PostProcessResources;

public class AP_ShaderSwap : MonoBehaviour
{
    public Shader shaderCell;
    public Shader shaderDefault; 
    private Renderer objectRenderer; // Reference to the object's renderer component
    private bool isShaderCell = true; // Flag to track which shader is active

    void Start()
    {
        // Get the renderer component of the object
        objectRenderer = GetComponent<Renderer>();

        // Set the initial shader to shaderA
        objectRenderer.material.shader = shaderCell;
    }

    void Update()
    {
        // Check for the "C" key press
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Toggle between the two shaders
            if (isShaderCell)
            {
                objectRenderer.material.shader = shaderDefault;
            }
            else
            {
                objectRenderer.material.shader = shaderCell;
            }

            isShaderCell = !isShaderCell; // Toggle the flag
        }
    }
}
