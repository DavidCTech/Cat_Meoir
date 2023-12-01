using UnityEngine;
using UnityEngine.AI;

public class CarMovement : MonoBehaviour
{
    public Transform[] waypoints; // No need for an array, just reference the waypoints directly
    private int currentWaypointIndex = 0;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetDestination();
    }

    private void SetDestination()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            // Set the destination to the position of the current waypoint
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    private void Update()
    {
        // Check if the car has reached its destination waypoint
        if (navMeshAgent.remainingDistance < 0.1f && !navMeshAgent.pathPending)
        {
            currentWaypointIndex++;

            // Check if all waypoints have been reached
            if (currentWaypointIndex >= waypoints.Length)
            {
                // All waypoints reached, destroy the car
                Destroy(gameObject);
            }
            else
            {
                // Set the next destination
                SetDestination();
            }
        }
    }
}