using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HighContrastManager : MonoBehaviour
{
    public static HighContrastManager instance;
    public InputActionAsset playerInput;
    private InputAction catVision;

    public HighContrastMode[] highContrastScripts;
    public bool isUsingCatVision = false;

    private SwappableObjects swappableObjects;

    public Material[] materialColors;

    void Awake()
    {
        //Make script an instance
        if (instance == null)
        {
            instance = this;
        }

        highContrastScripts = FindObjectsOfType<HighContrastMode>();
        swappableObjects = FindFirstObjectByType<SwappableObjects>();
    }

    public void OnEnable()
    {
        playerInput.Enable();

        catVision = playerInput.FindAction("CatVision");
        catVision.performed += CatVision;
    }
    public void OnDisable()
    {
        playerInput.Disable();
        catVision.performed -= CatVision;
    }

    public void CatVision(InputAction.CallbackContext context)
    {
        if (PlayerPrefs.HasKey("CvHighContrastState"))
        {
            isUsingCatVision = !isUsingCatVision;
            SwapMaterialsInCatVision();
        }

        if (swappableObjects != null)
        {
            SwappableObjects.instance.SwapMaterials();
        }
    }

    public void SwapMaterials()
    {
        if (PlayerPrefs.HasKey("HighContrastState"))
        {
            foreach (HighContrastMode script in highContrastScripts)
            {
                script.SwapMaterials();
            }
        }
    }

    public void SwapMaterialsInCatVision()
    {
        foreach (HighContrastMode script in highContrastScripts)
        {
            script.SwapMaterialsInCatVision();
        }
    }

    public void SwapColorMaterial()
    {
        foreach (HighContrastMode script in highContrastScripts)
        {
            script.SwapColorMaterial();
        }
    }
}
