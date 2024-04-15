using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        // Ensure there is a Rigidbody attached to the object
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("GrabbableObject script requires a Rigidbody component.");
            enabled = false; // Disable the script if there's no Rigidbody
        }
    }

    public void SetKinematic(bool isKinematic)
    {
        if (rb != null)
        {
            rb.isKinematic = isKinematic;
        }
    }

    public bool IsKinematic()
    {
        return rb != null && rb.isKinematic;
    }
}
