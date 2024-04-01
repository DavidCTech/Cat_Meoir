using System.Collections.Generic;
using UnityEngine;

public class ClimbUIManager : MonoBehaviour
{
    //written by chat gpt
    public GameObject climbUIPrefab;
    public GameObject player;
    public float upValue; 
    private Dictionary<GameObject, GameObject> climbUIObjects = new Dictionary<GameObject, GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Climb"))
        {
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

                climbUIObjects.Add(climbUI, other.gameObject);
                climbUI.GetComponent<PopUpDespawn>().SetObject(highestPoint);
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climb"))
        {
            foreach (KeyValuePair<GameObject, GameObject> pair in climbUIObjects)
            {
                if (pair.Value == other.gameObject)
                {
                    Destroy(pair.Key);
                    climbUIObjects.Remove(pair.Key);
                    break;
                }
            }
        }
    }

}