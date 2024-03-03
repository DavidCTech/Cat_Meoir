using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStealth : MonoBehaviour
{
    public float verticalOffset; // later change this to a gameobject reference on the hide location from a script you take in 
 
  
    public Vector3 playerOriginalPosition;

    //stealth should be in another script
    private bool isStealth = false;
    public bool isStealthed = false;

    public float timer = 1f;
    public float StealthState = 0f;

    public Animator anim;


    public void Hide(GameObject hidingSpot, PlayerInteractionCheck playerInteractionCheck)
    {
        Debug.Log(hidingSpot.transform.position);
        playerOriginalPosition = transform.position;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true; 
        transform.position = hidingSpot.transform.position + new Vector3(0f, verticalOffset, 0f);
        playerInteractionCheck.isHiding = true;
        this.gameObject.GetComponent<PlayerMovement>().isFrozen = true;


    }

    public void UnHide(PlayerInteractionCheck playerInteractionCheck)
    {

        transform.position = playerOriginalPosition;
        playerInteractionCheck.isHiding = false;

        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        this.gameObject.GetComponent<PlayerMovement>().isFrozen = false;

    }

    public void StealthTransitionTimer()
    {
        
        
        if (StealthState < 1)
        {
            StealthState = StealthState + 0.01f;
        }
        else
        {
            StealthState = 1f;
        }

    }

    public void ReverseTransitionTimer()
    {
       
        if (StealthState > 0)
        {
            StealthState = StealthState - 0.01f;
        }
        else
        {
            StealthState = 0f;
        }

    }
    public void Update()
    {
        if (isStealth)
        {
            isStealthed = true;
            StealthTransitionTimer();
        }
        else
        {
            isStealthed = false;
            ReverseTransitionTimer();
        }

        anim.SetFloat("StealthTransition", StealthState);
    }

    //onstealth should be in another script..
    public void OnStealth()
    {
        isStealth = !isStealth;
    }
}
