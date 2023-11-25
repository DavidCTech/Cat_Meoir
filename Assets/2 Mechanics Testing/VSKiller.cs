using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class VSKiller : MonoBehaviour
{
    NavMeshAgent agent;

    public bool bloodLusted;
    public LayerMask targetMask;


    public GameObject Player;
    public Transform player;

    public float _proximityRadius;
    [Range(0, 360)] public float _proximityAngle;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = player.transform.position;
    }

    private void ProximityCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _proximityRadius, targetMask);

        bloodLusted = rangeChecks.Length > 0;

        if (bloodLusted)
        {
            Transform target = rangeChecks[0].transform;
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            // Assuming no obstruction check and no consideration of FOV
            bloodLusted = distanceToTarget <= _proximityRadius;
        }
    }



    void Update()
    {
        agent.destination = player.transform.position;
        ProximityCheck();


    }
}
