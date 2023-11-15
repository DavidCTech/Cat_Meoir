using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPopup : MonoBehaviour
{
    public GameObject interactUI;
    public GameObject hideUI;
    public GameObject jumpUI;
    public GameObject climbUI;
    public GameObject doorUI; 

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
            Debug.Log(objectName);
            if (objectName == "Interact")
            {
                GameObject newObject = Instantiate(interactUI, playerCheck.closestPoint , Quaternion.identity);
                newObject.transform.LookAt(this.gameObject.transform);

            }
            if (objectName == "Hide")
            {

            }
            if (objectName == "Jump")
            {

            }
            if (objectName == "Climb")
            {
                GameObject newObject = Instantiate(climbUI, playerCheck.closestPoint, Quaternion.identity);
                newObject.transform.LookAt(this.gameObject.transform);
            }
            if (objectName == "Door")
            {

            }

            
        }
    }


}
