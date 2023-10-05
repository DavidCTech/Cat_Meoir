using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerController playerControls;

    public Vector2 movementInput;
    public float vInput;
    public float hInput;

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerController();

            playerControls.Player.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
        //- Im adding the following line to your code to take off the subscription in order to prevent memory leaks as general good practice 
        playerControls.Player.Movement.performed -= i => movementInput = i.ReadValue<Vector2>();
    }

    public void HandleAllInputs()
    {
        ManageMovementInput();
    }

    private void ManageMovementInput()
    {
        vInput = movementInput.y;
        hInput = movementInput.x;
    }
}
