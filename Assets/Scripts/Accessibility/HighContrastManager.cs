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

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        highContrastScripts = FindObjectsOfType<HighContrastMode>();
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
            SwapMaterialsInCatVision();
            Debug.Log("Swapping Materials in Cat Vision");
        }

        if (FindObjectOfType<BookstorePosters>() != null)
        {
            BookstorePosters.instance.SwapMaterials();
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
        isUsingCatVision = !isUsingCatVision;
        foreach (HighContrastMode script in highContrastScripts)
        {
            script.SwapMaterialsInCatVision();
        }
    }
}
