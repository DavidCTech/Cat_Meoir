using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public Canvas dialogue;
    private GameObject player; 
    
    public void Interact(GameObject playerObject)
    {
        //lock the player
        player = playerObject;
        player.GetComponent<PlayerMovement>().isFrozen = true; 
        dialogue.gameObject.SetActive(true);
    }
    public void TurnCanvasOff(Canvas previousCanvas)
    {
        previousCanvas.gameObject.SetActive(false);
    }
    public void TurnCanvasOn(Canvas nextCanvas)
    {
        nextCanvas.gameObject.SetActive(true);
    }
    public void End(Canvas current)
    {
        //unlock the player
        player.GetComponent<PlayerMovement>().isFrozen = false;
        current.gameObject.SetActive(false);
    }
}
