using UnityEngine;
using UnityEngine.AI;

public class CarMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private NavMeshAgent navMeshAgent;
    private bool isStopped = false; // Flag to track if the car movement is stopped

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetDestination();
    }

    private void SetDestination()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    private void Update()
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
}