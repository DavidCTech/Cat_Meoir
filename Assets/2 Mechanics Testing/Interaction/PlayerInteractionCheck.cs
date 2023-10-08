using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
  

    /*
    private InputManager inputManager;

    private void OnEnable()
    {
        inputManager = InputManager.Instance;

        // Check if the reference is not null
        if (inputManager != null)
        {
            // Subscribe to the OnInteraction event
            inputManager.playerControls.Player.Interaction.performed += OnInteraction;
        }
        else
        {
            Debug.LogError("InputManager instance not found!");
        }
    }

    private void OnDisable()
    {
        // Unsubscribe when the script is disabled
        inputManager.playerControls.Player.Interaction.performed -= OnInteraction;
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        Debug.Log("Interaction.");
        // Implement your interaction logic here
    }

    */
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
                    }
                }
            }
        }

        return passString;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            //checks a raycast for the interactable objects to see what the tag is - depending on tag this script will call other scripts. 
            string objectName = checkObject();
            if(objectName == "Interact")
            {
                //should turn the player here    
                targetObject.GetComponent<NPCInteraction>().Interact(this.gameObject);
                //reference the interaction code
            }
            if(objectName == "Hide")
            {
                //reference the Hide code
            }
            if(objectName == "Jump")
            {
                //refence the jump code
            }
            if(objectName == "Climb")
            {
                //reference the climbing code
            }
            else
            {
                //reference the meow code
            }


        }
       
    }


}
