using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPopup : MonoBehaviour
{
    /*
    public GameObject interactUI;
    public GameObject hideUI;
    public GameObject jumpUI;
    public GameObject climbUI;
    public GameObject doorUI; 
    */

    private float checkTime = 1f;
    private PlayerInteractionCheck playerCheck; 

    private void Start()
    {
        playerCheck = this.gameObject.GetComponent<PlayerInteractionCheck>(); 
        StartCoroutine(TurnOnButton());
    }

    //THIS CODE DOES NOT DO INTERACTION VVVV ( it makes an interaction button appear- just that. ) 
    private IEnumerator TurnOnButton()
    {
        while (true) // Infinite loop to repeat the coroutine
        {
            yield return new WaitForSeconds(checkTime);

            

            string objectName = playerCheck.CheckObject();
            GameObject obj = playerCheck.ReturnTarget();
            Vector3 closePoint = playerCheck.closestPoint; 
            
            if (objectName == "Interact")
            {
                ClimbUI climbUI = obj.GetComponent<ClimbUI>();

                if (climbUI != null)
                {
                    climbUI.TurnOn(closePoint);
                }
            }
            if (objectName == "Hide")
            {
                ClimbUI climbUI = obj.GetComponent<ClimbUI>();

                if (climbUI != null)
                {
                    climbUI.TurnOn(closePoint);
                }
            }
            if (objectName == "Jump")
            {

            }
            if (objectName == "Climb")
            {
                /*
                GameObject newObject = Instantiate(climbUI, playerCheck.closestPoint, Quaternion.identity);
                newObject.transform.LookAt(this.gameObject.transform);
                */
                ClimbUI climbUI = obj.GetComponent<ClimbUI>();
                
                if(climbUI != null)
                {
                    climbUI.TurnOn(closePoint); 
                }
            }
            if (objectName == "Door")
            {

            }

            
        }
    }


}
