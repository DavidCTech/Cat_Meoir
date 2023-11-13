using UnityEngine;

public class BoxPusher : MonoBehaviour
{
    private Rigidbody boxRigidbody;
    private bool isPushing = false;

    private void Start()
    {
        boxRigidbody = GetComponent<Rigidbody>();
        if (boxRigidbody == null)
        {
            Debug.LogError("Rigidbody not found on BoxPusher GameObject!");
        }

        // Set the box to be initially kinematic
        SetBoxKinematic(true);
    }

    private void Update()
    {
        // Calculate the direction based on the player's input
        Vector3 movementDirection = GetPlayerInputDirection();

        // Move the box only when the player holds the "E" button and is pushing in the Z direction
        if (Input.GetKey(KeyCode.E) && Mathf.Abs(movementDirection.z) > Mathf.Abs(movementDirection.x))
        {
            PushBox(movementDirection);
        }
        else
        {
            // Set the box to be kinematic when not pushing or pushing in a different direction
            SetBoxKinematic(true);
        }
    }

    private Vector3 GetPlayerInputDirection()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        return new Vector3(horizontal, 0, vertical).normalized;
    }

    private void PushBox(Vector3 movementDirection)
    {
        // Apply force to move the box in the Z direction
        float forceMagnitude = 5.0f; // Adjust this value as needed
        Vector3 force = new Vector3(0, 0, movementDirection.z) * forceMagnitude;
        boxRigidbody.AddForce(force, ForceMode.Force);

        // Set the box to be dynamic when pushing
        SetBoxKinematic(false);
    }

    private void SetBoxKinematic(bool value)
    {
        if (boxRigidbody.isKinematic != value)
        {
            boxRigidbody.isKinematic = value;
        }
    }
}