using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class CarMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private NavMeshAgent navMeshAgent;
    private bool isStopped = false; // Flag to track if the car movement is stopped
    private bool isInsideStopBox = false; // Flag to track if the trigger box is inside a stop box

    private GameObject triggerBox; // Reference to the trigger box object

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetDestination();

        // Find the trigger box by searching in children
        triggerBox = FindTriggerBox();
    }

    private void SetDestination()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    public void Update()
    {
        if (!isStopped)
        {
            if (navMeshAgent.remainingDistance < 0.1f && !navMeshAgent.pathPending)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= waypoints.Length)
                {
                    Destroy(gameObject);
                }
                else
                {
                    SetDestination();
                }
            }
        }

        if (isInsideStopBox && !isStopped)
        {
            StopMovement();
        }
        else if (!isInsideStopBox && isStopped)
        {
            ResumeMovement();
        }
    }

    public void StopMovement()
    {
        isStopped = true;
        navMeshAgent.isStopped = true; // Stop the NavMeshAgent
        Debug.Log("Car stopped.");
    }

    public void ResumeMovement()
    {
        isStopped = false;
        navMeshAgent.isStopped = false; // Resume the NavMeshAgent
        SetDestination(); // Set new destination to continue movement
        Debug.Log("Car resumed movement.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StopBox"))
        {
            isInsideStopBox = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("StopBox"))
        {
            isInsideStopBox = false;
        }
    }

    private GameObject FindTriggerBox()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("TriggerBox"))
            {
                return child.gameObject;
            }
        }
        Debug.LogWarning("Trigger box not found!");
        return null;
    }
}