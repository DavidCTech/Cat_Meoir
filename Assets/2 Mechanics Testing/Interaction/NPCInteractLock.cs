using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractLock : MonoBehaviour
{

    public GameObject player;
    private PlayerInteractionCheck playerInteract;
   
    private void OnEnable()
    {
        if(player != null)
        {
            playerInteract = player.GetComponent<PlayerInteractionCheck>(); 

        }
        if(playerInteract != null)
        {
            playerInteract.isInteracting = true; 
        }


    }

    //OnDisable turns on when the script disables itself 
    private void OnDisable()
    {
        
            Invoke("KeepOnCheck", 0.1f);
        
    }
    private void KeepOnCheck()
    {
        if (!this.gameObject.activeInHierarchy)
        {
            if (playerInteract != null)
            {
                playerInteract.isInteracting = false;
            }


        }
    }
}
