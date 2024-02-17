using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DirectMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody theRB;
    private float valueX;
    private float valueY;
    private Vector2 direction;
    private Vector2 directionGamepad;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Keyboard myKeyboard = Keyboard.current;
        Gamepad myGamepad = Gamepad.current;

        valueX = 0;
        valueY = 0;

        if (myGamepad != null)
        {
            directionGamepad = myGamepad.leftStick.ReadValue();

            valueX = directionGamepad.x;
            valueY = directionGamepad.y;
        }


        /*if (myKeyboard != null)
        {
            if (myKeyboard.aKey.isPressed)
            {
                valueX = -1;
            }

            if (myKeyboard.dKey.isPressed)
            {
                valueX = 1;
            }

            if (myKeyboard.wKey.isPressed)
            {
                valueY = 1;
            }

            if (myKeyboard.sKey.isPressed)
            {
                valueY = -1;
            }

            if (myKeyboard.aKey.isPressed && myKeyboard.dKey.isPressed)
            {
                valueX = 0;
            }

            if (myKeyboard.wKey.isPressed && myKeyboard.sKey.isPressed)
            {
                valueY = 0;
            }

        }*/

        direction = new Vector2(valueX, valueY).normalized;
    }

    void FixedUpdate()
    {
        theRB.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }
}
