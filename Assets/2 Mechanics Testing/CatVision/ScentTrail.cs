using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

//This script goes on a scent trail nav mesh agent ( parent of the particle effect of the scent trail )
public class ScentTrail : MonoBehaviour
{
    
    public NavMeshAgent navMeshAgent;//please drag in the navmesh - assigning it in code for some reason didnt work for me ;-; 

    public void SetDestination(Transform destination)
    {
        // Set the destination for the NavMeshAgent
        navMeshAgent.SetDestination(destination.position);
    }
}