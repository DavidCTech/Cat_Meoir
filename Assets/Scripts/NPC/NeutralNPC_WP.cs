using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NeutralNPC_WP : MonoBehaviour
{
    public enum NPCState
    {
        Idle,
        Walk,
    }

    public NPCState currentState = NPCState.Idle;

    NavMeshAgent agent;
    private Animator anim;

    public LayerMask targetMask;
    //public LayerMask obstructionMask;

    public GameObject Player;
    public Transform player;
    public float rotationSpeed = 5f; // Adjust the rotation speed as needed

    public float _proximityRadius;
    [Range(0, 360)] public float _proximityAngle;

    public Transform[] waypoints;
    public int currentWaypointIndex = 0;

    public float waitTime = 1.0f; // Wait time in seconds
    private float waitTimer = 0f; // Timer to track the wait time



    //public bool tired;
    public bool awake;
    public bool interested;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        currentState = NPCState.Idle;
        anim.SetBool("Idle", true);
        anim.SetBool("Walk", false);
        awake = true;
        Player = GameObject.FindGameObjectWithTag("Player");

        if (Player != null)
        {
            player = Player.transform;
        }
    }

    void Update()
    {

        ProximityCheck();

        switch (currentState)
        {
            case NPCState.Idle:
                Idling();
                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);
                if (awake && interested)
                {
                    Idling();
                }
                else if (awake && !interested)
                {
                    currentState = NPCState.Walk;
                }
                break;

            case NPCState.Walk:
                anim.SetBool("Idle", false);
                anim.SetBool("Walk", true);
                if (agent.remainingDistance == 0f && awake && !interested)
                {
                    Walking();
                }
                else if (agent.remainingDistance == 0f && awake && interested)
                {
                    currentState = NPCState.Idle;
                }
                break;



            default:
                break;
        }
    }

    private void ProximityCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _proximityRadius, targetMask);

        interested = rangeChecks.Length > 0;

        if (interested)
        {
            Transform target = rangeChecks[0].transform;
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // Assuming no obstruction check and no consideration of FOV
            interested = distanceToTarget <= _proximityRadius;
        }
    }

    void Idling()
    {
        if (player != null && interested)
        {
            // Calculate the direction from the NPC to the player
            Vector3 directionToPlayer = player.position - transform.position;

            // Ignore the y-component to only rotate on the horizontal plane
            directionToPlayer.y = 0;

            // Set the rotation to smoothly look at the player
            if (directionToPlayer != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer.normalized, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }


    void Walking()
    {
        /*if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned.");
            return;
        }

        agent.destination = waypoints[currentWaypointIndex].position;
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;*/
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned.");
            return;
        }

        // Move to the waypoint
        agent.destination = waypoints[currentWaypointIndex].position;
        anim.SetBool("Idle", false);
        anim.SetBool("Walk", true);

        // If reached the waypoint, start waiting
        if (agent.remainingDistance == 0f)
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                // Reset the wait timer
                waitTimer = 0f;

                // Move to the next waypoint
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
    }
}
