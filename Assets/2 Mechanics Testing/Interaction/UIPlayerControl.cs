using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerControl : MonoBehaviour
{
    public GameObject player; 

    void OnEnable()
    {
        player.GetComponent<PlayerMovement>().isFrozen = true;
        player.GetComponent<PlayerInteractionCheck>().isInteracting = true; 
    }


    void OnDisable()
    {
        player.GetComponent<PlayerMovement>().isFrozen = false;
        player.GetComponent<PlayerInteractionCheck>().isInteracting = false;

    }
}
