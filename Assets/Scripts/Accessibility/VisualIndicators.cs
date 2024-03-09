using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class VisualIndicators : MonoBehaviour
{
    public InputActionAsset playerInputs;
    private InputAction catVision, sprinting, stealth, pushing;

    void Awake()
    {

    }

    void Start()
    {

    }

    public void OnEnable()
    {
        playerInputs.Enable();

        catVision = playerInputs.FindAction("CatVision");
        catVision.performed += CatVision;
    }
    public void OnDisable()
    {
        playerInputs.Disable();
        catVision.performed -= CatVision;
    }

    public void CatVision(InputAction.CallbackContext context)
    {

    }
}
