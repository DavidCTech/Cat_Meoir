using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class VisualIndicators : MonoBehaviour
{
    public static VisualIndicators instance;

    public InputActionAsset playerInputs, cameraInputs;
    private InputAction catVision, sprinting, stealth, catMemory;
    public TMP_Text visualIndicatorsText;
    public GameObject visualIndicatorsBG, visualIndicatorsTextObject;
    public bool isInCatVision, isSprinting, isInStealth, isPushing, isHiding, isInCatMemory;

    void Awake()
    {
        //Make script an instance
        if (instance == null)
        {
            instance = this;
        }
    }

    public void OnEnable()
    {
        playerInputs.Enable();
        cameraInputs.Enable();

        catVision = playerInputs.FindAction("CatVision");
        sprinting = playerInputs.FindAction("Sprint");
        stealth = playerInputs.FindAction("Stealth");
        catMemory = cameraInputs.FindAction("CatMemoryZoom");
        catVision.performed += CatVisionMode;
        sprinting.performed += SprintingMode;
        stealth.performed += StealthMode;
        catMemory.performed += CatMemoryMode;
    }

    public void OnDisable()
    {
        playerInputs.Disable();
        cameraInputs.Disable();

        catVision.performed -= CatVisionMode;
        sprinting.performed -= SprintingMode;
        stealth.performed -= StealthMode;
        catMemory.performed -= CatMemoryMode;
    }

    public void CatVisionMode(InputAction.CallbackContext context)
    {
        Debug.Log("Cat Vision Mode Visual Indicator");
        isInCatVision = !isInCatVision;
        VisualIndicatorCatVision("Cat Vision On");
    }

    public void SprintingMode(InputAction.CallbackContext context)
    {
        Debug.Log("Sprinting Mode Visual Indicator");
        isSprinting = !isSprinting;
        VisualIndicatorSprinting("Sprinting On");
    }

    public void StealthMode(InputAction.CallbackContext context)
    {
        Debug.Log("Stealth Mode Visual Indicator");
        isInStealth = !isInStealth;
        VisualIndicatorStealth("Stealth On");
    }

    public void CatMemoryMode(InputAction.CallbackContext context)
    {
        Debug.Log("Cat Memory Mode Visual Indicator");
        isInCatMemory = !isInCatMemory;
        VisualIndicatorCatMemory("Cat Memory On");
    }

    public void HidingMode()
    {
        Debug.Log("Hiding Mode Visual Indicator");
        isHiding = !isHiding;
        VisualIndicatorHiding("Is Hiding");
    }

    public void PushingMode()
    {
        Debug.Log("Pushing Mode Visual Indicator");
        isPushing = !isPushing;
        VisualIndicatorPushing("Is Pushing");
    }

    public void VisualIndicatorCatVision(string visualText)
    {
        if (AccessibilityManager.instance.visualIndicatorsToggle.isOn)
        {
            if (isInCatVision)
            {
                visualIndicatorsBG.SetActive(true);
                visualIndicatorsText.text = visualText.ToString();
            }
            else
            {
                visualIndicatorsBG.SetActive(false);
            }
        }
    }

    public void VisualIndicatorSprinting(string visualText)
    {
        if (AccessibilityManager.instance.visualIndicatorsToggle.isOn)
        {
            if (isSprinting)
            {
                visualIndicatorsBG.SetActive(true);
                visualIndicatorsText.text = visualText.ToString();
            }
            else
            {
                visualIndicatorsBG.SetActive(false);
            }
        }
    }

    public void VisualIndicatorStealth(string visualText)
    {
        if (AccessibilityManager.instance.visualIndicatorsToggle.isOn)
        {
            if (isInStealth)
            {
                visualIndicatorsBG.SetActive(true);
                visualIndicatorsText.text = visualText.ToString();
            }
            else
            {
                visualIndicatorsBG.SetActive(false);
            }
        }
    }

    public void VisualIndicatorCatMemory(string visualText)
    {
        if (AccessibilityManager.instance.visualIndicatorsToggle.isOn)
        {
            if (isInCatMemory)
            {
                visualIndicatorsBG.SetActive(true);
                visualIndicatorsText.text = visualText.ToString();
            }
            else
            {
                visualIndicatorsBG.SetActive(false);
            }
        }
    }

    public void VisualIndicatorHiding(string visualText)
    {
        if (AccessibilityManager.instance.visualIndicatorsToggle.isOn)
        {
            if (isHiding)
            {
                visualIndicatorsBG.SetActive(true);
                visualIndicatorsText.text = visualText.ToString();
            }
            else
            {
                visualIndicatorsBG.SetActive(false);
            }
        }
    }

    public void VisualIndicatorPushing(string visualText)
    {
        if (AccessibilityManager.instance.visualIndicatorsToggle.isOn)
        {
            if (isPushing)
            {
                visualIndicatorsBG.SetActive(true);
                visualIndicatorsText.text = visualText.ToString();
            }
            else
            {
                visualIndicatorsBG.SetActive(false);
            }
        }
    }
}
