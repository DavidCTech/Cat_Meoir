using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwap : MonoBehaviour
{
    public Material originalMaterial; // Assign the original material in the Unity Editor
    public Material newMaterial; // Assign the new material in the Unity Editor

    private MeshRenderer render;

    private bool toggled = false;

    void Start()
    {
        // // Check if the object has a renderer component
        // render = gameObject.GetComponent<MeshRenderer>();

        // if (render == null)
        // {
        //     Debug.LogError("Renderer component not found on " + gameObject.name);
        // }
        // else
        // {
        //     // Set the initial material to the original material
        //     render.material = originalMaterial;
        // }
    }

    void Update()
    {
        // Check for a button press, e.g., the 'x' key
        if (Input.GetButtonDown("Shader Toggle"))
        {
            //Debug.LogError("space pressed");
            // Toggle between originalMaterial and newMaterial
            ToggleMaterial();
        }
    }

    void ToggleMaterial()
    {
        if (!toggled)
        {
            GetComponent<MeshRenderer>().sharedMaterial = newMaterial;
            //Debug.LogError("changed to new");
            toggled = true;
        }
        else
        {
            GetComponent<MeshRenderer>().sharedMaterial = originalMaterial;
            //Debug.LogError("changed to original");
            toggled = false;
        }
        // if (render != null)
        // {
        //     // Check the current material and toggle to the other one
        //     if (render.material == originalMaterial)
        //     {
        //         Debug.LogError("changed to new");
        //         render.material = newMaterial;
        //     }
        //     else
        //     {
        //         render.material = originalMaterial;
        //     }
        // }

    }
}
