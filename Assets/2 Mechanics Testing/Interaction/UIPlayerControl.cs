using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerControl : MonoBehaviour
{
    public GameObject player; 

    void OnEnable()
    {
        if(player != null)
        {
            player.GetComponent<PlayerMovement>().isFrozen = true;
            player.GetComponent<PlayerInteractionCheck>().isInteracting = true;
        }
        
    }


    void OnDisable()
    {
        if(player != null)
        {
            player.GetComponent<PlayerMovement>().isFrozen = false;
            player.GetComponent<PlayerInteractionCheck>().isInteracting = false;
        }
        

    }
}
