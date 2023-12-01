using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//This script goes on the player 
public enum PlayerState
{
    Normal,
    Vision
}

public class PlayerVision : MonoBehaviour
{
    [Header("The Scent Particle is the object with a parent of a nav mesh and scent trail and Child of the particles")]
    public GameObject scentParticle;
    [Header("This FOV radius controls how far the scent trail will track if there's something present")]
    public float fovRadius;
    [Header("Clue mask should be on no post processing for this script")]
    public LayerMask clueMask;
    [Header("Obstruction mask controls what blocks the clues from being tracked.")]

    public LayerMask obstructionMask;


    [Header("These are Events that can call methods for the vision.")]
    public UnityEvent visionOn;
    public UnityEvent visionOff;

    public PlayerState CurrentState { get; private set; }


    private PlayerState currentState = PlayerState.Normal;
    private GameObject targetObject;
    private List<GameObject> clues = new List<GameObject>();
    private List<GameObject> particles = new List<GameObject>(); 

    public PlayerState GetCurrentState()
    {
        return currentState;
    }

    public void SetCurrentState(PlayerState state)
    {
        currentState = state;
    }


    void OnCatVision()
    {
        // Check for input to toggle between Vision and Normal states
        if (CurrentState == PlayerState.Normal)
        {
            CurrentState = PlayerState.Vision;
            visionOn.Invoke();
            GetComponent<PlayerMovement>().SetPlayerState(PlayerState.Vision);
        }
        else
        {
            CurrentState = PlayerState.Normal;
            visionOff.Invoke();
            for (int i = 0; i < particles.Count; i++)
            {
                GameObject destoryObj = particles[i];
                particles.Remove(particles[i]);
                Destroy(destoryObj); 
                
            
            
            }
                GetComponent<PlayerMovement>().SetPlayerState(PlayerState.Normal);
        }
    }

    public void OutRay()
    {
        //ray cast out for clues around - so you dont get clues very far away 
        Collider[] rangeChecks = Physics.OverlapSphere(this.gameObject.transform.position, fovRadius, clueMask);
        if (rangeChecks.Length != 0)
        {
            for (int i = 0; i < rangeChecks.Length; i++)
            {
                Transform target = rangeChecks[i].transform;
                Vector3 directionToTarget = (target.position - this.gameObject.transform.position).normalized;
                float distanceToTarget = Vector3.Distance(this.gameObject.transform.position, target.position);
                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    targetObject = target.gameObject;
                    if(targetObject.GetComponent<ClueObjectColor>() != null)
                    {
                        targetObject.GetComponent<ClueObjectColor>().ColorChange();
                        //Instantiate a scent trail going to that object.
                        ScentTrail(targetObject);
                        //add the object to the list so that it can be reverted later without using any search functions 
                        clues.Add(targetObject);
                    }
                }
            }
        }
    }

    //uses a list of the objects found that are clues to turn them all off (better than dragging in every clue or searching it up) 
    public void TurnOffColors()
    {
        for (int i = 0; i < clues.Count; i++)
        {
            if (clues[i].GetComponent<ClueObjectColor>() != null)
            {
                clues[i].GetComponent<ClueObjectColor>().ColorRevert();
            }
        }
        clues.Clear();
    }


    //this just spawns the scent trail ai that goes to the clue 
    private void ScentTrail(GameObject target)
    {
        GameObject spawnedObject = Instantiate(scentParticle, this.gameObject.transform.position, this.gameObject.transform.rotation);
        particles.Add(spawnedObject);
        spawnedObject.SetActive(true);
        spawnedObject.GetComponent<ScentTrail>().SetDestination(target.transform);

    }
}

