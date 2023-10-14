using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human_AI : MonoBehaviour
{
    //enums are state specific 
    private enum AIState
    {
        Passive,
        Hostile

    }
    private AIState _AIState;

    public GameObject Player;
    private PlayerStealth playerStealth;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    //global variables have _ under it, but locals dont its just another organizational tactic 
    public bool _canSeePlayer;
    public bool _isChasingPlayer;
    public bool _IAmWaiting;
    public bool _randomWaitTime;
    public bool _randomWander;
    public bool _alwaysMoving;
    public bool _fleeFromPlayer;

    [SerializeField]
    //serialized feilds allow you to see the following in the inspector 
    [Range(1, 7)] private int wait_time;
    //range is a slider to modify things in inspector

    [SerializeField]
    private float _speed;
    private float _timeSinceSeenPlayer;
    //time to lose the player

    [SerializeField]
    [Range(0, 500)] private float _walkRadius;
    //movement circle

    public float _FoVRadius;
    //vision system 

    [Range(0, 360)] public float _FoVAngle;



    public float _proximityRadius;
    [Range(0, 360)] public float _proximityAngle;


    [SerializeField]
    private Transform[] _waypoints;
    //this was an array of physical locations of points

    private int _nextWayPoint = 0;

    [HideInInspector]
    public GameObject _player;
    private Renderer _enemyColor;
    //change for visual feedback
    private NavMeshAgent _navMeshAgent;



    // Start is called before the first frame update
    void Start()
    {
        _enemyColor = GetComponent<Renderer>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GameObject.Find("Player");
        playerStealth = Player.GetComponent<PlayerStealth>();
    }


    IEnumerator RandomWaitTimer()
    {
        if (_alwaysMoving == false && _randomWaitTime == true)
        {
            wait_time = Random.Range(1, 5);
            _navMeshAgent.speed = 0;
            yield return new WaitForSeconds(wait_time);
        }
        else if (_alwaysMoving == false && _randomWaitTime == false)
        {
            _navMeshAgent.speed = 0;
            yield return new WaitForSeconds(wait_time);

        }
        _navMeshAgent.speed = _speed;
        _IAmWaiting = false;

    }

    public Vector3 RandomNavMeshLocation()
    {
        //setting it to zero at the beginning will allow it to not be transformed additively each time 
        Vector3 finalPosition = Vector3.zero;
        //the sphere is as big as your walk radius - imaginary sphere 
        Vector3 randomPosition = Random.insideUnitSphere * _walkRadius;
        //we are adding that location to transform position
        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, _walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    private void Wander()
    {
        if (_navMeshAgent != null && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance && _IAmWaiting == false)
        {
            _navMeshAgent.SetDestination(RandomNavMeshLocation());
            _IAmWaiting = true;
            StartCoroutine(RandomWaitTimer());
        }
    }

    void GoToNextPoint()
    {
        if (_waypoints.Length == 0)
        {
            return;
        }
        _navMeshAgent.destination = _waypoints[_nextWayPoint].position;
        //percent sign asks that you go back to the beginning when youre at the length 
        _nextWayPoint = (_nextWayPoint + 1) % _waypoints.Length;
        _IAmWaiting = true;
        StartCoroutine(RandomWaitTimer());
    }
    private void FieldOfViewCheck()
    {
        //make an array of range checks colliders which is made from the circle radius, take the fov angle, then ;look for the target player mask 
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _FoVRadius, targetMask);
        //the if statement is a null check for the raycast 
        if (rangeChecks.Length != 0)
        {
            //tells them the first range in target layer is the player 
            Transform target = rangeChecks[0].transform;
            //look towards the player 
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            //we divide by 2 to make the angle cone, is this thing actually in the fov 
            if (Vector3.Angle(transform.forward, directionToTarget) < _FoVAngle / 2)
            {
                //calculate distance between player and the ai 
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                //if the raycast isnt obstructued, you can see the player !
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    _canSeePlayer = true;
                }
                // if the raycast is obstructued, you cant see the player : ( 
                else
                {
                    _canSeePlayer = false;
                }
            }
            //inverse of the if we can actually see the player in the fov 
            else
            {
                _canSeePlayer = false;
            }
        }
        //if range check is 0, no collision? no player, cant see it
        else if (_canSeePlayer == false)
        {
            //you cant see the player i swear
            _canSeePlayer = false;
            //time from before where you go by evey second until you never lose player 
            _timeSinceSeenPlayer += Time.deltaTime;
            if (_timeSinceSeenPlayer >= 2f)
            {
                //if it has been 2 second since youve seen the player, you cant see them, chase them, so go be hostile, then 0 the time we have seen player again 
                _canSeePlayer = false;
                _isChasingPlayer = false;
                _AIState = AIState.Passive;
                _timeSinceSeenPlayer = 0;
            }
        }
    }
    private void ProximityCheck()
    {
        //raycast in all directions, this makes the Ai know you're there
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _proximityRadius, targetMask);
        //the if statement is a null check for the raycast 
        if (rangeChecks.Length != 0)
        {
            //tells them the first range in target layer is the player 
            Transform target = rangeChecks[0].transform;
            //look towards the player 
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            //we divide by 2 to make the angle cone, is this thing actually in the fov 
            if (Vector3.Angle(transform.forward, directionToTarget) < _proximityAngle / 2)
            {
                //calculate distance between player and the ai 
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                //if the raycast isnt obstructued, you can see the player !
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask) && playerStealth.isStealthed == false)
                {
                    _canSeePlayer = true;
                }
                // if the raycast is obstructued, you cant see the player : ( 
                else
                {
                    _canSeePlayer = false;
                }
            }
            //inverse of the if we can actually see the player in the fov 
            else
            {
                _canSeePlayer = false;
            }
        }
        //if range check is 0, no collision? no player, cant see it
        else if (_canSeePlayer == false)
        {
            //you cant see the player i swear
            _canSeePlayer = false;
            //time from before where you go by evey second until you never lose player 
            _timeSinceSeenPlayer += Time.deltaTime;
            if (_timeSinceSeenPlayer >= 2f)
            {
                //if it has been 2 second since youve seen the player, you cant see them, chase them, so go be hostile, then 0 the time we have seen player again 
                _canSeePlayer = false;
                _isChasingPlayer = false;
                _AIState = AIState.Passive;
                _timeSinceSeenPlayer = 0;
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
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void ChasePlayer()
    {
        _isChasingPlayer = true;
        _navMeshAgent.destination = _player.transform.position;
        if (_canSeePlayer == true)
        {
            _enemyColor.material.color = Color.red;
        }
        else
        {
            _enemyColor.material.color = Color.magenta;
        }
        FieldOfViewCheck();
    }
    public void FleeFromPlayer()
    {
        // the times one is a requirement to put in the function
        Vector3 runTo = transform.position + ((transform.position - _player.transform.position) * 1);
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance < _walkRadius)
        {
            _navMeshAgent.SetDestination(runTo);
        }
    }



    // Update is called once per frame
    private void Update()
    {

        if (playerStealth._isHidden == true)
        {
            _canSeePlayer = false;
            _isChasingPlayer = false;
            _AIState = AIState.Passive;
            _timeSinceSeenPlayer = 0;
        }

        switch (_AIState)
        {
            case AIState.Passive:
                _enemyColor.material.color = Color.yellow;
                if (_randomWander == true)
                {

                    Wander();
                    if (_canSeePlayer == true)
                    {
                        //for some reason can see player is never true

                        _enemyColor.material.color = Color.green;
                        _AIState = AIState.Hostile;
                    }
                }
                else
                {
                    if (_navMeshAgent.remainingDistance < 2f && _canSeePlayer == false)
                    {
                        GoToNextPoint();
                    }
                    if (_canSeePlayer == true)
                    {
                        _AIState = AIState.Hostile;
                    }
                }
                break;
            case AIState.Hostile:
                if (_fleeFromPlayer == true)
                {
                    FleeFromPlayer();
                    _navMeshAgent.speed = 30;
                    _enemyColor.material.color = Color.green;
                }
                else
                {
                    ChasePlayer();
                    _navMeshAgent.speed = 10;
                }
                if (_canSeePlayer == false)
                {
                    FieldOfViewCheck();
                }
                break;




        }
        FieldOfViewCheck();
        ProximityCheck();

        

    }
}
