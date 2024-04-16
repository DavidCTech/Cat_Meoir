using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableDisable : MonoBehaviour
{
    public GameObject objectToControl;
    public GameObject pauseMenu;
    private bool isObjectActive = false;



    private void Update()
    {
        // Check if the pause menu is active
        bool isPaused = pauseMenu.activeSelf;

        // Call the method to enable/disable the object based on the pause menu state
        ObjectToControl(isPaused);
    }

    private void ObjectToControl(bool isPaused)
    {
        // Toggle the object based on the new pause menu state
        if (isObjectActive != isPaused)
        {
            isObjectActive = isPaused;

            if (isPaused)
            {
                // Call the function to turn on the object
                ActivateObject();
            }
            else
            {
                // Call the function to turn off the object
                DeactivateObject();
            }
        }
    }

    private void ActivateObject()
    {
        // Activate the object
        objectToControl.SetActive(true);
    }

    private void DeactivateObject()
    {
        // Deactivate the object
        objectToControl.SetActive(false);
    }
}
