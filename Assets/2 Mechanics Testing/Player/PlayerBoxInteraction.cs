using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerBoxInteraction : MonoBehaviour
{
    private BoxPusher boxPusher;
    private InputAction boxPushAction;
    private PlayerInput playerInput;

    public UnityEvent onStartAction;
    public UnityEvent onStopAction;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            boxPushAction = playerInput.actions["BoxPush"];
            boxPushAction.started += ctx => StartAction();
            boxPushAction.canceled += ctx => StopAction();
            boxPushAction.Enable();
        }
        else
        {
            Debug.LogError("PlayerInput component not found!");
        }
    }

    private void Update()
    {
        float actionValue = boxPushAction.ReadValue<float>();

        if (actionValue > 0.5f)
        {
            StartAction();
        }
        else
        {
            StopAction();
        }
    }

    private void StartAction()
    {
        // Broadcast the start action event to any listeners
        onStartAction.Invoke();
    }

    private void StopAction()
    {
        // Broadcast the stop action event to any listeners
        onStopAction.Invoke();
    }

    private void OnDisable()
    {
        // Disable the input action when the script is disabled or destroyed
        boxPushAction.Disable();
    }
}
