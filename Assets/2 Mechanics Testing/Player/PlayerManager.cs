using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerMovement playerMovement;


    void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    private void Update()
    {
        inputManager.HandleAllInputs();
    }
    
    private void FixedUpdate()
    {
        playerMovement.ManageAllMovement();
    }
    
}
