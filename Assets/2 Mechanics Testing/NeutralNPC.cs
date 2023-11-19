using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NeutralNPC : MonoBehaviour
{
    public enum NPCState
    {
        Idle,
        Walk,
    }

    public NPCState currentState = NPCState.Idle;
    //private Animator anim;

    NavMeshAgent agent;

    public float range;
    public Transform centrePoint;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public GameObject Player;
    public float _proximityRadius;
    [Range(0, 360)] public float _proximityAngle;

    public bool tired;
    public bool awake;
    public bool interested;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        tired = true;
        currentState = NPCState.Idle;

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

    void Update()
    {

        ProximityCheck();

        switch (currentState)
        {
            case NPCState.Idle:
                Idling();
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
                if (awake)
                {
                    StartCoroutine(Walking());
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
        StartCoroutine(IdleWaitTimer());
    }


    IEnumerator Walking()
    {
        //Random Movement
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                agent.SetDestination(point);
                yield return new WaitForSeconds(10.0f);
                tired = true;
                if(tired && interested)
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
