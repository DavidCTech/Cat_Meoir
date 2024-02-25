using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TBone : MonoBehaviour
{
    //enums are state specific 
    private enum AIState
    {
        Passive,
        Hostile
    }
    private AIState _AIState;

    //private Animator anim;

    public GameObject Player;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    //global variables have _ under it, but locals dont its just another organizational tactic 
    public bool canSeePlayer;
    public bool isChasingPlayer;
    public bool IAmWaiting;
    public bool randomWaitTime;
    public bool randomWander;
    public bool alwaysMoving;
    public bool fleeFromPlayer;
    public bool hasSeenPlayer;
    public bool tired;
    public bool near;

    public float nearDistance = 5f;

    [SerializeField]
    //serialized feilds allow you to see the following in the inspector 
    [Range(1, 7)] private int wait_time;
    //range is a slider to modify things in inspector

    [SerializeField]
    private float speed;
    private float timeSinceSeenPlayer;
    //time to lose the player

    [SerializeField]
    [Range(0, 500)] private float walkRadius;
    //movement circle

    public float FoVRadius;
    //vision system 

    [Range(0, 360)] public float FoVAngle;



    public float proximityRadius;
    [Range(0, 360)] public float proximityAngle;


    [SerializeField]
    private Transform[] waypoints;
    //this was an array of physical locations of points

    private int nextWayPoint = 0;

    [HideInInspector]
    public GameObject player;
    private Renderer _enemyColor;
    //change for visual feedback
    private NavMeshAgent navMeshAgent;

    private GameObject spawnedObject; // Reference to the recently spawned object
    private bool objectSpawned = false; // Flag to track if the object has been spawned




    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Relentless());

        _enemyColor = GetComponent<Renderer>();
        //anim = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private IEnumerator Relentless()
    {
        //yield return new WaitForSeconds(5f);
        canSeePlayer = true;

        yield return new WaitForSeconds(20f);
        tired = true;

        yield return new WaitForSeconds(15f);
        tired = false;
        yield return new WaitForSeconds(30f);
    }

    public void CheckNearPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);
        if (distanceToPlayer <= nearDistance)
        {
            near = true;
        }
        else
        {
            // Set the 'Near' bool to false if the AI is not near the player
            near = false;
        }
    }

    IEnumerator RandomWaitTimer()
    {
        if (alwaysMoving == false && randomWaitTime == true)
        {
            wait_time = Random.Range(1, 5);
            navMeshAgent.speed = 0;
            yield return new WaitForSeconds(wait_time);
        }
        else if (alwaysMoving == false && randomWaitTime == false)
        {
            navMeshAgent.speed = 0;
            yield return new WaitForSeconds(wait_time);

        }
        navMeshAgent.speed = speed;
        IAmWaiting = false;

    }

    public Vector3 RandomNavMeshLocation()
    {
        //setting it to zero at the beginning will allow it to not be transformed additively each time 
        Vector3 finalPosition = Vector3.zero;
        //the sphere is as big as your walk radius - imaginary sphere 
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        //we are adding that location to transform position
        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private void Wander()
    {
        if (navMeshAgent != null && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && IAmWaiting == false)
        {
            navMeshAgent.SetDestination(RandomNavMeshLocation());
            IAmWaiting = true;
            StartCoroutine(RandomWaitTimer());
        }
    }

    void GoToNextPoint()
    {
        if (waypoints.Length == 0)
        {
            return;
        }
        navMeshAgent.destination = waypoints[nextWayPoint].position;
        //percent sign asks that you go back to the beginning when youre at the length 
        nextWayPoint = (nextWayPoint + 1) % waypoints.Length;
        IAmWaiting = true;
        StartCoroutine(RandomWaitTimer());
    }
    private void FieldOfViewCheck()
    {
        //make an array of range checks colliders which is made from the circle radius, take the fov angle, then ;look for the target player mask 
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, FoVRadius, targetMask);
        //the if statement is a null check for the raycast 
        if (rangeChecks.Length != 0)
        {
            //tells them the first range in target layer is the player 
            Transform target = rangeChecks[0].transform;
            //look towards the player 
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            //we divide by 2 to make the angle cone, is this thing actually in the fov 
            if (Vector3.Angle(transform.forward, directionToTarget) < FoVAngle / 2)
            {
                //calculate distance between player and the ai 
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                //if the raycast isnt obstructued, you can see the player !
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask) && !tired)
                {
                    canSeePlayer = true;
                    hasSeenPlayer = true;
                }
                // if the raycast is obstructued, you cant see the player : ( 
                else
                {
                    canSeePlayer = false;
                }
            }
            //inverse of the if we can actually see the player in the fov 
            else
            {
                canSeePlayer = false;
            }
        }
        //if range check is 0, no collision? no player, cant see it
        else if (canSeePlayer == false)
        {
            //you cant see the player i swear
            canSeePlayer = false;
            //time from before where you go by evey second until you never lose player 
            timeSinceSeenPlayer += Time.deltaTime;
            if (timeSinceSeenPlayer >= 2f)
            {
                //if it has been 2 second since youve seen the player, you cant see them, chase them, so go be hostile, then 0 the time we have seen player again 
                canSeePlayer = false;
                isChasingPlayer = false;
                _AIState = AIState.Passive;
                timeSinceSeenPlayer = 0;
            }
        }
    }
    private void ProximityCheck()
    {
        //raycast in all directions, this makes the Ai know you're there
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, proximityRadius, targetMask);
        //the if statement is a null check for the raycast 
        if (rangeChecks.Length != 0)
        {
            //tells them the first range in target layer is the player 
            Transform target = rangeChecks[0].transform;
            //look towards the player 
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            //we divide by 2 to make the angle cone, is this thing actually in the fov 
            if (Vector3.Angle(transform.forward, directionToTarget) < proximityAngle / 2)
            {
                //calculate distance between player and the ai 
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                //if the raycast isnt obstructued, you can see the player !
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask) && !tired)
                {
                    canSeePlayer = true;
                    hasSeenPlayer = true;
                }
                // if the raycast is obstructued, you cant see the player : ( 
                else
                {
                    canSeePlayer = false;
                }
            }
            //inverse of the if we can actually see the player in the fov 
            else
            {
                canSeePlayer = false;
            }
        }
        //if range check is 0, no collision? no player, cant see it
        else if (canSeePlayer == false)
        {
            //you cant see the player i swear
            canSeePlayer = false;
            //time from before where you go by evey second until you never lose player 
            timeSinceSeenPlayer += Time.deltaTime;
            if (timeSinceSeenPlayer >= 2f)
            {
                //if it has been 2 second since youve seen the player, you cant see them, chase them, so go be hostile, then 0 the time we have seen player again 
                canSeePlayer = false;
                isChasingPlayer = false;
                _AIState = AIState.Passive;
                timeSinceSeenPlayer = 0;
            }
        }

    }
    // The nmect function checks for the player, not on every frame
    private IEnumerator CheckForPlayer()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            //run the scan every point 2 seconds
            //anim.SetBool("Waiting", true);
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void ChasePlayer()
    {
        isChasingPlayer = true;
        navMeshAgent.destination = player.transform.position;
        if (canSeePlayer == true && !tired)
        {
            _enemyColor.material.color = Color.red;
            //anim.SetBool("Chase", true);
            //anim.SetBool("Patrol", false);
        }
        else
        {
            isChasingPlayer = false;
            _AIState = AIState.Passive;
            _enemyColor.material.color = Color.magenta;
            //anim.SetBool("Chase", false);
            //anim.SetBool("Patrol", true);

        }
        FieldOfViewCheck();
    }
    public void FleeFromPlayer()
    {
        // the times one is a requirement to put in the function
        Vector3 runTo = transform.position + ((transform.position - player.transform.position) * 1);
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance < walkRadius)
        {
            navMeshAgent.SetDestination(runTo);
        }
    }

    /*private void SetDestinationToSpawnLocation()
    {
        // Check if NavMeshAgent component is present
        if (navMeshAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on NPC GameObject.");
            return;
        }

        // Set NPC's destination to the position of the spawned object
        navMeshAgent.SetDestination(spawnedObject.transform.position);
    }

    private void FixedUpdate()
    {
        // Check if the object has been spawned and if not, look for it
        if (!objectSpawned)
        {
            spawnedObject = GameObject.FindWithTag("Distraction"); // Change "YourSpawnedObjectTag" to the tag of your spawned object

            // If the object is found, set the destination and flag that the object is spawned
            if (spawnedObject != null)
            {
                SetDestinationToSpawnLocation();
                objectSpawned = true;
            }
        }
        else
        {
            objectSpawned = false;
        }
    }*/

    // Update is called once per frame
    private void Update()
    {

        /*if (IAmWaiting == true)
        {
            anim.SetBool("Waiting", true);
        }
        else
        {
            anim.SetBool("Waiting", false);
        }*/
        CheckNearPlayer();

        switch (_AIState)
        {
            case AIState.Passive:
                //anim.SetBool("Patrol", true);
                //anim.SetBool("Chase", false);
                _enemyColor.material.color = Color.yellow;
                if (randomWander == true)
                {

                    Wander();
                    if (canSeePlayer == true)
                    {
                        //for some reason can see player is never true

                        _enemyColor.material.color = Color.green;
                        _AIState = AIState.Hostile;
                    }
                }
                else
                {
                    if (navMeshAgent.remainingDistance == 0f && canSeePlayer == false)
                    {
                        GoToNextPoint();
                    }
                    if (canSeePlayer == true)
                    {
                        _AIState = AIState.Hostile;
                    }
                }
                break;
            case AIState.Hostile:
                //anim.SetBool("Patrol", false);
                //anim.SetBool("Chase", true);
                if (fleeFromPlayer == true)
                {
                    FleeFromPlayer();
                    navMeshAgent.speed = 30;
                    _enemyColor.material.color = Color.green;
                }
                else
                {
                    ChasePlayer();
                    navMeshAgent.speed = 6;
                }
                if (canSeePlayer == false)
                {
                    FieldOfViewCheck();
                }
                break;
        }
        FieldOfViewCheck();
        ProximityCheck();
    }
}
