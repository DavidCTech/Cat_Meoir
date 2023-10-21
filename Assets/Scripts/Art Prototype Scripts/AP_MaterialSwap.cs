using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AP_MaterialSwap : MonoBehaviour
{
    public Material[] originalMaterial, newMaterial;

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
        Material[] mats = GetComponent<MeshRenderer>().materials;

        int l = originalMaterial.Length;

        if (!toggled)
        {

            for (int i = 0; i < l; i++)
            {
                mats[i] = newMaterial[i];
            }
            //Debug.LogError("changed to new");
            toggled = true;
        }
        else
        {
            for (int i = 0; i < l; i++)
            {
                mats[i] = originalMaterial[i];
            }
            //Debug.LogError("changed to original");
            toggled = false;
        }

        GetComponent<MeshRenderer>().materials = mats;

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
