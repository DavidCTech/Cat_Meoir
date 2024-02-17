using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DirectSyntax : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        //Direct Workflow Example
        Keyboard myKeyboard = Keyboard.current;
        Gamepad myGamepad = Gamepad.current;
        Mouse myMouse = Mouse.current;

        if (myMouse != null)
        {
            if (myMouse.leftButton.wasPressedThisFrame)
            {
                Debug.Log("left Mouse Button Was Pressed this frame");
            }

            if (myMouse.scroll.ReadValue().y > 0)
            {
                Debug.Log("Scrolling Up");
            }

            Debug.Log(myMouse.position.ReadValue());
        }

        /*if (myGamepad != null)
        {
            if (myGamepad.buttonWest.wasPressedThisFrame)
            {
                Debug.Log("X Button Was Pressed");
            }

            if (myGamepad.dpad.left.wasPressedThisFrame)
            {
                Debug.Log("D Pad Was Pressed");
            }

            Debug.Log(myGamepad.leftStick.ReadValue());
        }*/

        /*if (myKeyboard != null)
        {
            //Replaces Input.GetKeyDown
            if (myKeyboard.spaceKey.wasPressedThisFrame)
            {
                Debug.Log("Space Key Was Pressed");
            }

            //Replaces Input.GetKeyUp
            if (myKeyboard.spaceKey.wasReleasedThisFrame)
            {
                Debug.Log("Space Key Was Released");
            }

            //Replaces Input.GetKey
            if (myKeyboard.spaceKey.isPressed)
            {
                Debug.Log("Space Key is Pressed");
            }
        }*/
    }
}
