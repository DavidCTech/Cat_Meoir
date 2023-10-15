using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    //This script can be attached to NPCs OR any UI that might have turning off and on canvases to display information. If it is an npc, toggle isNPC
    public bool isNPC;
    public bool isLockingPlayer;
    [Header("You only need dialogue if this object is an NPC")]
    public Canvas dialogue;
    private GameObject player; 
    
    public void Interact(GameObject playerObject)
    {
       
        if (isNPC)
        {
            dialogue.gameObject.SetActive(true);
            //lock the player
            if (isLockingPlayer)
            {
                player = playerObject;
                player.GetComponent<PlayerMovement>().isFrozen = true;
            }
        }

    }
    public void TurnCanvasOff(Canvas previousCanvas)
    {
        previousCanvas.gameObject.SetActive(false);
    }
    public void TurnCanvasOn(Canvas nextCanvas)
    {
        nextCanvas.gameObject.SetActive(true);
    }

    public void TurnMenuOff(GameObject pauseMenu)
    {
        pauseMenu.gameObject.SetActive(false);
    }
    public void End(Canvas current)
    {
        //unlock the player
        player.GetComponent<PlayerMovement>().isFrozen = false;
        current.gameObject.SetActive(false);
    }
}
