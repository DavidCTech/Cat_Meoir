using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBoneWarning : MonoBehaviour
{
    public TBone tboneS; // Reference to the other script where the boolean variable is defined
    public GameObject uiElement; // Reference to the UI element you want to enable

    void Start()
    {
        // Attempt to find the other script component on the same GameObject
        tboneS = GetComponent<TBone>();

        // Check if the other script component was found
        if (tboneS == null)
        {
            Debug.LogError("EnableUIOnBool: Failed to find YourOtherScript component!");
        }
    }

    void Update()
    {
        // Check if the boolean variable in the other script is true
        if (tboneS != null && tboneS.near)
        {
            // Enable the UI element
            if (uiElement != null)
            {
                uiElement.SetActive(true);
            }
        }
        else
        {
            // Disable the UI element
            if (uiElement != null)
            {
                uiElement.SetActive(false);
            }
        }
    }
}
