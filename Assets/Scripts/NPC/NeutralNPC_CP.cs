using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NeutralNPC_CP : MonoBehaviour
{
    public enum NPCState
    {
        Idle,
        Walk,
    }

    public NPCState currentState = NPCState.Idle;

    NavMeshAgent agent;
    private Animator anim;

    public float range;
    public Transform centrePoint;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public float timer = 0f;
    public float duration = 15f; // Adjust the duration as needed

    public GameObject Player;
    public Transform player;
    public float rotationSpeed = 5f; // Adjust the rotation speed as needed

    public float _proximityRadius;
    [Range(0, 360)] public float _proximityAngle;

    public bool tired;
    public bool awake;
    public bool interested;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        //tired = true;
        currentState = NPCState.Idle;
        anim.SetBool("Idle", true);
        anim.SetBool("Walk", false);
        Player = GameObject.FindGameObjectWithTag("Player");

        if (Player != null)
        {
            player = Player.transform;
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

    public bool IsTimerFinished()
    {
        return timer >= duration;
    }

    void Update()
    {

        ProximityCheck();
        timer += Time.deltaTime;

        if (timer >= duration)
        {
            timer = 0f;
        }

        switch (currentState)
        {
            case NPCState.Idle:
                Idling();
                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);
                if (tired && interested)
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
                if (awake)
                {
                    //StartCoroutine(Walking());
                    Walking();
                }
                else if (tired && interested)
                {
                    currentState = NPCState.Idle;
                }
                break;

            /*case NPCState.Interact:
                InteractWithPlayer();
                break;*/

            // Add more cases for additional states

            default:
                break;
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
        else
        {
            StartCoroutine(IdleWaitTimer());
        }

    }


    /*IEnumerator*/void Walking()
    {
        //Random Movement
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point) && timer <= 10)
            {
                agent.SetDestination(point);
                //yield return new WaitForSeconds(10.0f);
                
            }
            else
            {
                tired = true;
                anim.SetBool("Idle", true);
                anim.SetBool("Walk", false);
                if (tired && interested)
                {
                    currentState = NPCState.Idle;
                }
                else
                {
                    awake = true;
                }
            }
        }

        //Random Movement
        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {

            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            //randomPoint.y = 0;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                //Debug.Log(name + "Random position = " + hit.position);
                return true;
            }
            result = Vector3.zero;
            //Debug.Log(name + "Random position = " + hit.position);
            return false;
        }

    }

    IEnumerator IdleWaitTimer()
    {
        float randomWaitTime = Random.Range(10.0f, 15.0f); // Adjust the range as needed
        yield return new WaitForSeconds(randomWaitTime);
        tired = false;
        awake = true;

        /*if (stop) 
        {
            float randomWaitTime = Random.Range(10.0f, 15.0f); // Adjust the range as needed
            yield return new WaitForSeconds(randomWaitTime);
            stop = false;
        }*/
    }

}
