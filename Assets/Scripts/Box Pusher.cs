using UnityEngine;
using UnityEngine.InputSystem;

public class BoxPusher : MonoBehaviour
{
    private Rigidbody boxRigidbody;
    private bool isPushing = false;
    private InputAction boxPushAction;
    private bool collisionWithPlayer = false;


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

    private void OnEnable()
    {
        // Register to the events in PlayerBoxInteraction when this script is enabled
        FindObjectOfType<PlayerBoxInteraction>().onStartPush.AddListener(StartPush);
        FindObjectOfType<PlayerBoxInteraction>().onStopPush.AddListener(StopPush);
    }

    private void OnDisable()
    {
        // Unregister from the events when this script is disabled
        FindObjectOfType<PlayerBoxInteraction>().onStartPush.RemoveListener(StartPush);
        FindObjectOfType<PlayerBoxInteraction>().onStopPush.RemoveListener(StopPush);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            collisionWithPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the collision is with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            collisionWithPlayer = false;
        }
    }

    private void Update()
    {
        if (isPushing)
        {
            // Check if colliding with the player
            if (collisionWithPlayer)
            {
                // Use the new Input System to get input direction
                Vector2 inputVector = boxPushAction.ReadValue<Vector2>();
                Vector3 movementDirection = new Vector3(inputVector.x, 0, inputVector.y).normalized;

                // Check if the movement direction is significant (not close to zero)
                if (Mathf.Abs(movementDirection.z) > 0.1f)
                {
                    Debug.Log("Pushing");
                    // Apply force to move the box in the Z direction
                    float forceMagnitude = 10.0f; // Adjust this value as needed
                    Vector3 force = new Vector3(0, 0, movementDirection.z) * forceMagnitude;
                    boxRigidbody.AddForce(force, ForceMode.Force);

                    // Set the box to be dynamic when pushing
                    SetBoxKinematic(false);
                }
                else
                {
                    StopPush();
                }
            }
            else
            {
                // If not colliding with the player, stop pushing
                StopPush();
            }
        }
        else
        {
            // Set the box to be kinematic when not pushing
            SetBoxKinematic(true);
        }
    }

    private void SetBoxKinematic(bool value)
    {
        if (boxRigidbody.isKinematic != value)
        {
            boxRigidbody.isKinematic = value;
        }
    }

    public void StartPush()
    {
        // Called from PlayerBoxInteraction script
        isPushing = true;
        SetBoxKinematic(false);
    }

    public void StopPush()
    {
        // Called from PlayerBoxInteraction script
        isPushing = false;
        // Set the box to be kinematic when the push action is released
        SetBoxKinematic(true);
    }
}