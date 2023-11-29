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

    public UnityEvent onStartPush;
    public UnityEvent onStopPush;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            boxPushAction = playerInput.actions["BoxPush"];
            boxPushAction.started += ctx => StartPush();
            boxPushAction.canceled += ctx => StopPush();
            boxPushAction.Enable();
        }
        else
        {
            Debug.LogError("PlayerInput component not found!");
        }
    }

    private void Update()
    {
        float pushValue = boxPushAction.ReadValue<float>();

        if (pushValue > 0.5f)
        {
            StartPush();
        }
        else
        {
            StopPush();
        }
    }
    private void StartPush()
    {
        // Broadcast the start push event to any listeners
        onStartPush.Invoke();
    }

    private void StopPush()
    {
        // Broadcast the stop push event to any listeners
        onStopPush.Invoke();
    }

    private void OnDisable()
    {
        // Disable the input action when the script is disabled or destroyed
        boxPushAction.Disable();
    }
}
