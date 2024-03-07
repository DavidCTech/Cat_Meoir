using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnActivation : MonoBehaviour
{
    public GameObject objectToRotate; // Reference to the object to rotate
    public Vector3 rotationAmount; // Amount of rotation to apply when activated

    private void Start()
    {
        // Ensure object to rotate is provided
        if (objectToRotate == null)
        {
            Debug.LogError("Object to rotate is not assigned in RotateOnActivation script!");
        }
        else
        {
            // Rotate the object immediately upon start
            objectToRotate.transform.Rotate(rotationAmount);
        }
    }
}
