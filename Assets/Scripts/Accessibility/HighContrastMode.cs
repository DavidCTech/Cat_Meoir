using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighContrastMode : MonoBehaviour
{
    public Material targetMaterial;
    public Material[] originalMaterials;
    private SkinnedMeshRenderer rendererComponent;

    void Start()
    {
        //
        rendererComponent = GetComponent<SkinnedMeshRenderer>();

        if (rendererComponent != null)
        {
            originalMaterials = rendererComponent.materials;
        }
    }

    public void SwapMaterials()
    {
        if (rendererComponent == null || originalMaterials == null)
        {
            Debug.Log("Error in Array");
            return;
        }

        if (AccessibilityManager.instance.highContrastToggle.isOn)
        {
            Material[] newMaterials = new Material[originalMaterials.Length];

            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = targetMaterial;
            }
            rendererComponent.materials = newMaterials;
        }
        else if (!AccessibilityManager.instance.highContrastToggle.isOn)
        {
            rendererComponent.materials = originalMaterials;
        }
    }
}
