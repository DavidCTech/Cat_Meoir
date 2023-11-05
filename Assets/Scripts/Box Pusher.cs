using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPusher : MonoBehaviour
{
    private Rigidbody boxRigidbody;
    private bool freezeXConstraint = false; // Example constraint state

    private void Start()
    {
        boxRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the box
        if (collision.gameObject.CompareTag("Box"))
        {
            // Freeze or unfreeze X constraint as needed
            if (freezeXConstraint)
            {
                boxRigidbody.constraints |= RigidbodyConstraints.FreezePositionX;
            }
            else
            {
                boxRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionX;
            }

            // Apply force to move the box
            float mass = 1.0f; // Assume the box's mass is 1 kg
            float acceleration = 1.0f; // The acceleration required to move 1 meter
            float force = mass * acceleration;
            boxRigidbody.AddForce(transform.forward * force, ForceMode.Force);
        }
    }
}
