using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Normal,
    Vision
}

public class PlayerVisionStates : MonoBehaviour
{
    public PlayerState currentState = PlayerState.Normal;

    void OnCatVision()
    {
        // Check for input to toggle between Vision and Normal states
        if (currentState == PlayerState.Normal)
        {
            currentState = PlayerState.Vision;
        }
        else
        {
            currentState = PlayerState.Normal;
        }
    }
}
