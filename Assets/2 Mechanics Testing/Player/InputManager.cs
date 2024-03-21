using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerController playerControls;


    private static InputManager instance;
    public Vector2 movementInput;
    public float vInput;
    public float hInput;


    public static InputManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerController();

            playerControls.Player.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
        }

        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            playerControls.asset.LoadBindingOverridesFromJson(rebinds);

        playerControls.Enable();
    }

    public void OnDisable()
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
    public Vector2 GetMouseDelta()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public void ActionsResetAndLoad()
    {
        playerControls.Disable();
        //- Im adding the following line to your code to take off the subscription in order to prevent memory leaks as general good practice 

        playerControls.Player.Movement.performed -= i => movementInput = i.ReadValue<Vector2>();

        playerControls = new PlayerController();

        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            playerControls.asset.LoadBindingOverridesFromJson(rebinds);

        playerControls.Player.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

        playerControls.Enable();
    }
}
