using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using static NeutralNPC_CP;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerInteractionCheck : MonoBehaviour
{
    //the following commented out code should be reimplemented when the issue in the "PlayerInteraction" script is fixed. 
    [Tooltip("The layers it looks for + angle of sight")]
    public float fovRadius;
    public float fovAngle;
    public LayerMask interactionMask;
    public LayerMask obstructionMask;

    private GameObject targetObject;
    private string passString;

    public bool isHiding;
    public bool isInteracting; 
    [HideInInspector]
    public Vector3 closestPoint = new Vector3();

    private Swipe swipeScript;

    private void Start()
    {
        swipeScript = FindObjectOfType<Swipe>();
    }

    public string CheckObject()
    {
        //the following code is the interaction logic 
        Collider[] rangeChecks = Physics.OverlapSphere(this.gameObject.transform.position, fovRadius, interactionMask);
        passString = "nothing";
        
        if (rangeChecks.Length != 0)
        {


            for (int i = 0; i < rangeChecks.Length; i++)
            {
               
                Transform target = rangeChecks[i].transform;
                closestPoint = rangeChecks[i].ClosestPoint(this.gameObject.transform.position);
                //distance to target can be addedinto an array same with the game object to determine which object is closest within given parameters
                Vector3 directionToTarget = (closestPoint - this.gameObject.transform.position).normalized;
               
                if (Vector3.Angle(transform.forward, directionToTarget) < fovAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(this.gameObject.transform.position, target.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        
                        targetObject = target.gameObject;
                        //Debug.Log(targetObject);

                        passString = target.gameObject.tag;
                        //Debug.Log(passString);
                      
                        if (target.gameObject.tag != "Untagged")
                        {
                            return passString;
                        }
                        
                    }
                }
                else
                {
                    //Debug.Log("Not in the angle " );
                }
            }
        }

        return passString;
    }
    public GameObject ReturnTarget()
    {
        return targetObject; 
    }


    public void OnInteraction()
    {
       
        if (!isHiding)
        {
            //checks a raycast for the interactable objects to see what the tag is - depending on tag this script will call other scripts. 
            string objectName = CheckObject();
            if (!isInteracting)
            {
                Debug.Log("Object name "+ objectName);
                if (objectName == "Interact")
                {
                    Debug.Log("Interact");

                    //should turn the player here    
                    //targetObject.GetComponent<NPCInteraction>().Interact(this.gameObject);
                    //targetObject.GetComponent<NPCTalk>().Interact();
                    targetObject.GetComponent<NPCSpeak>().Interact();
                    //reference the interaction code
                }
                if (objectName == "Hide")
                {
                    this.gameObject.GetComponent<PlayerStealth>().Hide(targetObject, this);
                    //reference the Hide code
                }
                if (objectName == "Jump")
                {
                    //refence the jump code
                }
                if (objectName == "Climb")
                {
                    Debug.Log("object ", targetObject);
                    this.gameObject.GetComponent<ClimbRaycast>().Jump(targetObject);
                }
                if (objectName == "Door")
                {
                    //Debug.Log("should have");
                    targetObject.GetComponent<DoorOpener>().CheckDoorUnlock();
                }
                if (objectName == "Safe")
                {
                    SafeInteraction safeInteraction = targetObject.GetComponent<SafeInteraction>();
                    if (safeInteraction != null)
                    {
                        if (safeInteraction.IsMinigameActive())
                        {
                            // Exit the minigame
                            safeInteraction.ExitMinigame();
                        }
                        else
                        {
                            // Start safe interaction
                            safeInteraction.StartInteraction();
                        }
                    }
                    else
                    {
                        Debug.LogError("SafeInteraction component not found on the Safe object.");
                    }
                }
                if (!isInteracting)
                {
                    swipeScript.Swiping();
                }
                else
                {
                    //reference the meow code
                }
            }
        }
        else
        {
            this.gameObject.GetComponent<PlayerStealth>().UnHide(this);
        }  
    }
}
