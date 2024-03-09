using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class JustCruisingMode : MonoBehaviour
{
    private bool isInJustCruisingMode = false;
    private Rigidbody rb;

    [SerializeField]
    private UnityEvent onToggleOn;

    [SerializeField]
    private UnityEvent onToggleOff;

    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    public void ToggleObject(bool isToggled)
    {
        isInJustCruisingMode = isToggled;

        // Handle any additional logic based on the dynamic bool
        if (isInJustCruisingMode)
        {
            DeactivateObject();
            onToggleOff.Invoke();
            
        }
        else
        {
            ActivateObject();
            onToggleOn.Invoke();
        }
    }

    private void DeactivateObject()
    {
        gameObject.SetActive(false);
        // Additional logic for deactivation if needed
    }

    private void ActivateObject()
    {
        gameObject.SetActive(true);
        // Additional logic for activation if needed
    }

}
