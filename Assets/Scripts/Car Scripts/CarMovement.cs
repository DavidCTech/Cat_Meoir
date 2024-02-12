using UnityEngine;
using UnityEngine.AI;

public class CarMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private NavMeshAgent navMeshAgent;
    public float detectionRadius = 5f; // Radius for detecting obstacles
    public LayerMask obstacleLayer; // Layer mask for obstacles

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

        // Check for obstacles in front of the car
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, detectionRadius, obstacleLayer))
        {
            // Slow down or stop the car
            navMeshAgent.isStopped = true;
        }
        else
        {
            // Continue moving towards the destination
            navMeshAgent.isStopped = false;
        }
    }
}