using UnityEngine;
using UnityEngine.InputSystem;

public class BoxPusher : MonoBehaviour
{
    private Rigidbody boxRigidbody;
    private bool isGrabbing = false;
    private Transform playerTransform;
    private InputAction boxPushAction;
    private bool collisionWithPlayer = false;

    private void Start()
    {
        boxRigidbody = GetComponent<Rigidbody>();
        if (boxRigidbody == null)
        {
            Debug.LogError("Rigidbody not found on BoxPusher GameObject!");
        }

        playerTransform = FindObjectOfType<PlayerBoxInteraction>()?.transform;
        if (playerTransform == null)
        {
            Debug.LogError("Player transform not found!");
        }

        // Set the box to be initially kinematic
        SetBoxKinematic(true);
    }

    private void OnEnable()
    {
        // Register to the events in PlayerBoxInteraction when this script is enabled
        var playerInteraction = FindObjectOfType<PlayerBoxInteraction>();
        if (playerInteraction != null)
        {
            playerInteraction.onStartAction.AddListener(StartAction);
            playerInteraction.onStopAction.AddListener(StopAction);
        }
    }

    private void OnDisable()
    {
        // Unregister from the events when this script is disabled
        var playerInteraction = FindObjectOfType<PlayerBoxInteraction>();
        if (playerInteraction != null)
        {
            playerInteraction.onStartAction.RemoveListener(StartAction);
            playerInteraction.onStopAction.RemoveListener(StopAction);
        }
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
        if (isGrabbing)
        {
            if (collisionWithPlayer)
            {
                // Attach the box to the player
                transform.parent = playerTransform;

                // Set to non-kinematic when attached to the player
                SetBoxKinematic(false);
            }
            else
            {
                // If not colliding with the player, stop grabbing
                StopAction();
            }
        }
        else
        {
            // Set the box to be kinematic when not grabbing
            SetBoxKinematic(true);

            // Unparent the box from the player
            transform.parent = null;
        }
    }

    private void SetBoxKinematic(bool value)
    {
        if (boxRigidbody != null && boxRigidbody.isKinematic != value)
        {
            boxRigidbody.isKinematic = value;
        }
    }

    public void StartAction()
    {
        // Called from PlayerBoxInteraction script
        isGrabbing = true;
    }

    public void StopAction()
    {
        // Called from PlayerBoxInteraction script
        isGrabbing = false;
    }
}