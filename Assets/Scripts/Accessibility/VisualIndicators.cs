using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class VisualIndicators : MonoBehaviour
{
    public static VisualIndicators instance;

    public InputActionAsset playerInputs;
    private InputAction catVision, sprinting, stealth;
    public TMP_Text visualIndicatorsText;
    public GameObject visualIndicatorsBG, visualIndicatorsTextObject;
    public bool isInCatVision, isSprinting, isInStealth;

    void Awake()
    {
        //Make script an instance
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {

    }

    public void OnEnable()
    {
        playerInputs.Enable();

        catVision = playerInputs.FindAction("CatVision");
        sprinting = playerInputs.FindAction("Sprint");
        stealth = playerInputs.FindAction("Stealth");
        catVision.performed += CatVisionMode;
        sprinting.performed += SprintingMode;
        stealth.performed += StealthMode;
    }

    public void OnDisable()
    {
        playerInputs.Disable();
        catVision.performed -= CatVisionMode;
        sprinting.performed -= SprintingMode;
        stealth.performed -= StealthMode;
    }

    public void CatVisionMode(InputAction.CallbackContext context)
    {
        Debug.Log("Cat Vision Mode Visual Indicator");
        isInCatVision = !isInCatVision;
        VisualIndicatorMode("Cat Vision On");
    }

    public void SprintingMode(InputAction.CallbackContext context)
    {
        Debug.Log("Sprinting Mode Visual Indicator");
        isSprinting = !isSprinting;
        VisualIndicatorMode("Sprinting On");
    }

    public void StealthMode(InputAction.CallbackContext context)
    {
        Debug.Log("Stealth Mode Visual Indicator");
        isInStealth = !isInStealth;
        VisualIndicatorMode("Stealth On");
    }

    public void VisualIndicatorMode(string visualText)
    {
        if (AccessibilityManager.instance.visualIndicatorsToggle.isOn)
        {
            if (isInCatVision)
            {
                visualIndicatorsBG.SetActive(true);
                visualIndicatorsTextObject.SetActive(true);
                visualIndicatorsText.text = visualText.ToString();
            }
            else
            {
                visualIndicatorsBG.SetActive(false);
                visualIndicatorsTextObject.SetActive(false);
            }

            if (isSprinting)
            {
                visualIndicatorsBG.SetActive(true);
                visualIndicatorsTextObject.SetActive(true);
                visualIndicatorsText.text = visualText.ToString();
            }
            else
            {
                visualIndicatorsBG.SetActive(false);
                visualIndicatorsTextObject.SetActive(false);
            }

            if (isInStealth)
            {
                visualIndicatorsBG.SetActive(true);
                visualIndicatorsTextObject.SetActive(true);
                visualIndicatorsText.text = visualText.ToString();
            }
            else
            {
                visualIndicatorsBG.SetActive(false);
                visualIndicatorsTextObject.SetActive(false);
            }
        }
    }
}
