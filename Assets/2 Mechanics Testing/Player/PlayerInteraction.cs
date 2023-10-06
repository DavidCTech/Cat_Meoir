using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    public GameObject player;

    //Hiding Start
    public bool _isHidden;
    private bool _isHiding = false;
    private bool isHidinginProgress = false;
    private GameObject hidingSpot;
    public Vector3 playerOriginalPosition;
    private Vector3 lastHidingSpotPosition;

    PlayerMovement playerMovement;
    private bool isStealth = false;
    public bool isStealthed = false;
    //Hiding End

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }


    void OnInteraction()
    {






        //Hiding Start
        if (isHidinginProgress)
        {
            // If a hiding/unhiding process is already in progress, do nothing
            return;
        }
        if (_isHiding)
        {
            // Unhide the player
            unHide();
        }
        else if (hidingSpot != null)
        {
            // Hide the player
            lastHidingSpotPosition = hidingSpot.transform.position;
            Hide();
        }
        //Hiding End
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Hiding Start
        if (collision.gameObject.CompareTag("Hide"))
        {
            hidingSpot = collision.gameObject;
        }
        //Hiding End
    }

    private void OnCollisionExit(Collision collision)
    {
        //Hiding Start
        if (collision.gameObject.CompareTag("Hide"))
        {
            hidingSpot = null;
        }
        //Hiding End
    }

    //Hiding Start
    void Hide()
    {
        isHidinginProgress = true;
        // Set the player position inside the hiding spot
        float verticalOffset = 2.0f;
        //GetComponent<Collider>().enabled = false;
        transform.position = hidingSpot.transform.position + new Vector3(0f, verticalOffset, 0f);
        _isHiding = true;
        // Optionally, you can disable player controls or change the camera view here
        _isHidden = true;
        isHidinginProgress = false;
    }
    void unHide()
    {
        isHidinginProgress = true;
        // Reset the player position to exit the hiding spot
        // You may also need to reset player controls and camera view here
        //GetComponent<Collider>().enabled = true;
        transform.position = lastHidingSpotPosition;
        _isHiding = false; 
        _isHidden = false;
        isHidinginProgress = false;
    }

    void OnStealth()
    {
        //ToggleStealth();
        isStealth = !isStealth; // Toggle the stealth state

        // Activate or deactivate the player's stealth-related components here
        if (isStealth)
        {
            // Code to activate stealth mode
            //player.SetActive(false);
            isStealthed = true;
        }
        else
        {
            // Code to deactivate stealth mode
            //player.SetActive(true);
            isStealthed = false;
        }
    }
    //Hiding End
}
