using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HighContrastManager : MonoBehaviour
{
    public static HighContrastManager instance;

    public HighContrastMode[] highContrastScripts;

    void Awake()
    {
        if (instance == null)
        {
            //
            instance = this;
        }

        highContrastScripts = FindObjectsOfType<HighContrastMode>();
    }

    public void SwapMaterials()
    {
        foreach (HighContrastMode script in highContrastScripts)
        {
            script.SwapMaterials();
        }
    }
}
