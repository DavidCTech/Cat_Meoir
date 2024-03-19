using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighContrastMode : MonoBehaviour
{
    public Material targetMaterial;
    public Material[] originalMaterials;
    private SkinnedMeshRenderer rendererComponent;

    public string layerToSwitchName = "Clue";
    private string defaultLayerName;

    void Awake()
    {
        rendererComponent = GetComponent<SkinnedMeshRenderer>();

        if (rendererComponent != null)
        {
            originalMaterials = rendererComponent.materials;
        }

        defaultLayerName = LayerMask.LayerToName(0);
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

    public void SwapMaterialsInCatVision()
    {
        if (AccessibilityManager.instance.cvHighContrastToggle.isOn && HighContrastManager.instance.isUsingCatVision)
        {
            this.gameObject.layer = LayerMask.NameToLayer(layerToSwitchName);

            Material[] newMaterials = new Material[originalMaterials.Length];

            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = targetMaterial;
            }
            rendererComponent.materials = newMaterials;

            Debug.Log("Swapping Out Materials");
        }
        else
        {
            this.gameObject.layer = LayerMask.NameToLayer(defaultLayerName);
            rendererComponent.materials = originalMaterials;

            if (AccessibilityManager.instance.highContrastToggle.isOn && !HighContrastManager.instance.isUsingCatVision)
            {
                SwapMaterials();
                Debug.Log("Swapping Out Materials to Normal If");
            }

        }
    }
}
