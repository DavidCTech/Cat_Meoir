using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 originalScale;
    private bool isCentered = false;

    public GameObject clueNameText;
    public GameObject clueDescriptionText;

    void Start()
    {
        // Get the original position and scale of the object
        originalPosition = transform.position;
        originalScale = transform.localScale;
    }

    public void CenterAndZoom(string parentName)
    {
        if (!isCentered)
        {
            // Find the parent GameObject with the specified name
            GameObject parentObject = FindParentByName(parentName);
            if (parentObject == null)
            {
                Debug.LogError("Parent with name '" + parentName + "' not found.");
                return;
            }

            // Get the center position of the parent in world coordinates
            Vector3 centerPosition = parentObject.transform.position;

            // Set the position of the object to the center position
            transform.position = centerPosition;
            //transform.localScale *= 7f;
            transform.localScale = new Vector3(7f, 7f, 7f);


            isCentered = true;

            // Disable the disabledTextBox and enable the enabledTextBox
            clueNameText.SetActive(false);
            clueDescriptionText.SetActive(true);
        }
        else
        {
            // Reset to original position and scale
            transform.localScale = originalScale;
            transform.position = originalPosition;

            isCentered = false;

            // Enable the disabledTextBox and disable the enabledTextBox
            clueNameText.SetActive(true);
            clueDescriptionText.SetActive(false);
        }
    }

    private GameObject FindParentByName(string parentName)
    {
        Transform parent = transform.parent;
        while (parent != null)
        {
            if (parent.name == parentName)
            {
                return parent.gameObject;
            }
            parent = parent.parent;
        }
        return null;
    }
}