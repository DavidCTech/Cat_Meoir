using UnityEngine;
using UnityEngine.AI;
using System.Collections;

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

        // Start coroutine to periodically check if the car is still in the stop box
        StartCoroutine(CheckIfInsideStopBox());
    }

    public void ResumeMovement()
    {
        isStopped = false;
        navMeshAgent.isStopped = false; // Resume the NavMeshAgent
        SetDestination(); // Set new destination to continue movement
        Debug.Log("Car resumed movement.");

        // Stop coroutine if the car resumes movement
        StopCoroutine(CheckIfInsideStopBox());
    }

    private IEnumerator CheckIfInsideStopBox()
    {
        // Check if the car is still inside the stop box every second
        while (isStopped)
        {
            yield return new WaitForSeconds(1f);

            if (!IsInsideStopBox())
            {
                ResumeMovement();
                yield break; // Exit coroutine if the car is no longer inside the stop box
            }
        }
    }

    private bool IsInsideStopBox()
    {
        // Check if the car is inside the stop box by checking objects with the "StopBox" tag
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("StopBox"))
            {
                return true;
            }
        }
        return false;
    }
}