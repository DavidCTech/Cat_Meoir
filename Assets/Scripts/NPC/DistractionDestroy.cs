using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionDestroy : MonoBehaviour
{
    public float delay = 3f; // Delay in seconds before the object is destroyed

    private void Start()
    {
        // Invoke the DestroyObject function after the specified delay
        Invoke("DestroyObject", delay);
    }

    private void DestroyObject()
    {
        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }
}
