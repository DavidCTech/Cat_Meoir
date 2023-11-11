using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
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
 



 
    private string checkObject()
    {
        //the following code is the interaction logic 
        Collider[] rangeChecks = Physics.OverlapSphere(this.gameObject.transform.position, fovRadius, interactionMask);
        passString = "nothing";
        if (rangeChecks.Length != 0)
        {


            for (int i = 0; i < rangeChecks.Length; i++)
            {
                Transform target = rangeChecks[i].transform;
                Vector3 directionToTarget = (target.position - this.gameObject.transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < fovAngle / 2)
                {
                    float distanceToTarget = Vector3.Distance(this.gameObject.transform.position, target.position);
                    if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                    {
                        targetObject = target.gameObject;
                        passString = target.gameObject.tag;
                        if(target.gameObject.tag != "Untagged")
                        {
                            return passString;
                        }
                        
                    }
                }
            }
        }

        return passString;
    }
    public void OnInteraction()
    {
       
        if (!isHiding)
        {
            
            //checks a raycast for the interactable objects to see what the tag is - depending on tag this script will call other scripts. 
            string objectName = checkObject();
            
            if (objectName == "Interact")
            {

                //should turn the player here    
                //targetObject.GetComponent<NPCInteraction>().Interact(this.gameObject);
                targetObject.GetComponent<NPCTalk>().Interact();
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
                //old climb data
                //Get the transforms from the climb object 
                //Transform middleTransform = targetObject.GetComponent<ClimbData>().middleTransform;
                //Transform endTransform = targetObject.GetComponent<ClimbData>().endTransform;

                //reference the climbing code
                //this.gameObject.GetComponent<PlayerClimb>().Climb(middleTransform, endTransform);


                //new climb data 
                this.gameObject.GetComponent<ClimbRaycast>().Jump(targetObject);
            }
            if (objectName == "Door")
            {
                Debug.Log("should have");
                targetObject.GetComponent<ClueTriggerEvents>().CheckDoorUnlock();
            }
            else
            {
                //reference the meow code
            }
        }
        else
        {
            this.gameObject.GetComponent<PlayerStealth>().UnHide(this);
        }
        
    }

   
}
