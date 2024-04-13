using System.Collections.Generic;
using UnityEngine;

public class ClimbUIManager : MonoBehaviour
{
    //written by chat gpt
    public GameObject climbUIPrefab;
    public GameObject hideUIPrefab; 
    public GameObject InteractUIPrefab; 
    public GameObject SafeUIPrefab; 
    
    
    public GameObject player;
    public float upValue;
    private Dictionary<GameObject, GameObject> allUIObjects = new Dictionary<GameObject, GameObject>();
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Climb"))
        {
            Debug.Log("climb object: " + other.gameObject + "object name: " + other.gameObject.name);
            // closest point 
            Vector3 closestPoint = other.ClosestPoint(player.transform.position);
            Vector3 highestPoint = other.bounds.center + Vector3.up * other.bounds.extents.y;
            //Vector3 lowestPoint = other.bounds.center + Vector3.down * other.bounds.extents.y;
            Vector3 location =new Vector3(closestPoint.x, player.transform.position.y + upValue, closestPoint.z);
            if (player != null)
            {
                float heightDistance = highestPoint.y - player.transform.position.y;
                ClimbRaycast climbRaycast = player.GetComponent<ClimbRaycast>();
                /*
                if (heightDistance <= climbRaycast.maxDistance && heightDistance >= 0)
                {
                    
                }
                */
                GameObject climbUI = Instantiate(climbUIPrefab, location, Quaternion.identity);

                allUIObjects.Add(climbUI, other.gameObject);
                climbUI.GetComponent<PopUpDespawn>().SetObject(highestPoint);
            }
            
        }
        if (other.CompareTag("Hide"))
        {
          
            // closest point 
            Vector3 closestPoint = other.ClosestPoint(player.transform.position);
            Vector3 highestPoint = other.bounds.center + Vector3.up * other.bounds.extents.y;

            Vector3 location = new Vector3(closestPoint.x, player.transform.position.y + upValue, closestPoint.z);
            if (player != null)
            {
                float heightDistance = highestPoint.y - player.transform.position.y;
                ClimbRaycast climbRaycast = player.GetComponent<ClimbRaycast>();
              
                GameObject hideUI = Instantiate(hideUIPrefab, location, Quaternion.identity);

                allUIObjects.Add(hideUI, other.gameObject);
                hideUI.GetComponent<PopUpDespawn>().SetObject(highestPoint);
            }

        }
        if (other.CompareTag("Interact"))
        {

            // closest point 
            Vector3 closestPoint = other.ClosestPoint(player.transform.position);
            Vector3 highestPoint = other.bounds.center + Vector3.up * other.bounds.extents.y;

            Vector3 location = new Vector3(closestPoint.x, player.transform.position.y + upValue, closestPoint.z);
            if (player != null)
            {
                float heightDistance = highestPoint.y - player.transform.position.y;
                ClimbRaycast climbRaycast = player.GetComponent<ClimbRaycast>();

                GameObject interactUI = Instantiate(InteractUIPrefab, location, Quaternion.identity);

                allUIObjects.Add(interactUI, other.gameObject);
                interactUI.GetComponent<PopUpDespawn>().SetObject(highestPoint);
            }

        }
        if (other.CompareTag("Safe"))
        {

            // closest point 
            Vector3 closestPoint = other.ClosestPoint(player.transform.position);
            Vector3 highestPoint = other.bounds.center + Vector3.up * other.bounds.extents.y;

            Vector3 location = new Vector3(closestPoint.x, player.transform.position.y + upValue, closestPoint.z);
            if (player != null)
            {
                float heightDistance = highestPoint.y - player.transform.position.y;
                ClimbRaycast climbRaycast = player.GetComponent<ClimbRaycast>();

                GameObject safeUI = Instantiate(SafeUIPrefab, location, Quaternion.identity);

                allUIObjects.Add(safeUI, other.gameObject);
                safeUI.GetComponent<PopUpDespawn>().SetObject(highestPoint);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climb") || other.CompareTag("Hide")|| other.CompareTag("Interact") || other.CompareTag("Safe"))
        {
            foreach (KeyValuePair<GameObject, GameObject> pair in allUIObjects)
            {
                if (pair.Value == other.gameObject)
                {
                    Destroy(pair.Key);
                    allUIObjects.Remove(pair.Key);
                    break;
                }
            }
        }
    }

}