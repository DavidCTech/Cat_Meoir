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

    public bool isShadow, isNpc, isEnemy;

    void Awake()
    {
        if (isShadow)
        {
            targetMaterial = HighContrastManager.instance.materialColors[PlayerPrefs.GetInt("ShadowColor")];
        }

        if (isEnemy)
        {
            targetMaterial = HighContrastManager.instance.materialColors[PlayerPrefs.GetInt("EnemyColor")];
        }

        if (isNpc)
        {
            targetMaterial = HighContrastManager.instance.materialColors[PlayerPrefs.GetInt("NpcColor")];
        }

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

        if (AccessibilityManager.instance.isUsingHighContrastMode)
        {
            Material[] newMaterials = new Material[originalMaterials.Length];

            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = targetMaterial;
            }
            rendererComponent.materials = newMaterials;
        }
        else if (!AccessibilityManager.instance.isUsingHighContrastMode)
        {
            rendererComponent.materials = originalMaterials;
        }
    }

    public void SwapMaterialsInCatVision()
    {
        if (AccessibilityManager.instance.isUsingCvHighContrastMode && HighContrastManager.instance.isUsingCatVision)
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

            if (AccessibilityManager.instance.isUsingHighContrastMode && !HighContrastManager.instance.isUsingCatVision)
            {
                SwapMaterials();
                Debug.Log("Swapping Out Materials to Normal If");
            }
        }
    }

    public void SwapColorMaterial()
    {
        if (isShadow)
        {
            targetMaterial = HighContrastManager.instance.materialColors[PlayerPrefs.GetInt("ShadowColor")];
            SwapMaterials();
            SwapMaterialsInCatVision();
        }

        if (isNpc)
        {
            targetMaterial = HighContrastManager.instance.materialColors[PlayerPrefs.GetInt("NpcColor")];
            SwapMaterials();
            SwapMaterialsInCatVision();
        }

        if (isEnemy)
        {
            targetMaterial = HighContrastManager.instance.materialColors[PlayerPrefs.GetInt("EnemyColor")];
            SwapMaterials();
            SwapMaterialsInCatVision();
        }
    }
}
