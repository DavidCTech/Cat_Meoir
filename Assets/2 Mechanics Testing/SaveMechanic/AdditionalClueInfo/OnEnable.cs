using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class EnableDisableEvents : MonoBehaviour
{
    public UnityEvent onEnableEvent;
    public UnityEvent onDisableEvent;

    public void OnEnable()
    {
        if (onEnableEvent != null)
        {
            onEnableEvent.Invoke();
        }
    }

    public void OnDisable()
    {
        if (onDisableEvent != null)
        {
            onDisableEvent.Invoke();
        }
    }
}