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
            // Calculate the direction from the player to the box
            Vector3 directionToBox = collision.transform.position - transform.position;

            // Ensure the player is pushing the box along the Z-axis
            if (Mathf.Abs(directionToBox.z) > Mathf.Abs(directionToBox.x))
            {
                // Apply force to move the box only if X-constraint is not active
                if (!freezeXConstraint)
                {
                    float mass = 1.0f; // Assume the box's mass is 1 kg
                    float acceleration = 1.0f; // The acceleration required to move 1 meter
                    float force = mass * acceleration;
                    boxRigidbody.AddForce(transform.forward * force, ForceMode.Force);
                }
            }
            else
            {
                // Player is trying to push the box in the X-direction, don't allow it to move.
                boxRigidbody.velocity = Vector3.zero; // Stop the box's current movement.
            }
        }
    }
}