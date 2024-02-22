using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractionSkip : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //Script made with unity forum help and chat gpt 
    private float seconds = 1.0f;
    private float secondsTwo = 2.0f;
    private float secondsThree = 3.0f;
    public UnityEvent noSeconds;
    public UnityEvent onPressedOverSeconds;
    public UnityEvent onPressedOverSecondsTwo;
    public UnityEvent onPressedOverSecondsThree;
    public UnityEvent turnOffTimer; 

    private bool isPointerDown = false;
    private bool isThree = false; 

    //for click skipping
    public delegate void OnSkipDelegate();
    public static event OnSkipDelegate onSkipDel;

    //for hold down skipping
    public delegate void OnMegaSkip();
    public static event OnMegaSkip onMegaSkip;

    //this is for controller support
    private bool isControllerDown = false;
    private PlayerController playerControls;

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerController();
        }

        playerControls.Player.Skip.performed += ctx => OnSkipDown();
        playerControls.Player.Skip.canceled += ctx => OnSkipUp();
        playerControls.Player.Skip.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Skip.performed -= ctx => OnSkipDown();
        playerControls.Player.Skip.canceled -= ctx => OnSkipUp();
        playerControls.Player.Skip.Disable();
    }

    private void OnSkipUp()
    {
        isControllerDown = false;
        turnOffTimer.Invoke();
        if (isThree == false)
        {
            if (onSkipDel != null)
            {
                onSkipDel();
            }
        }

        CancelInvoke();
    }

    private void OnSkipDown()
    {
        isControllerDown = true;
        noSeconds.Invoke();
        Invoke("InvokePressedOverSeconds", seconds);
        Invoke("InvokePressedOverSecondsTwo", secondsTwo);
        Invoke("InvokePressedOverSecondsThree", secondsThree);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        noSeconds.Invoke();
        Invoke("InvokePressedOverSeconds", seconds);
        Invoke("InvokePressedOverSecondsTwo", secondsTwo);
        Invoke("InvokePressedOverSecondsThree", secondsThree);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPointerDown = false;
        turnOffTimer.Invoke(); 
        if(isThree == false )
        {
            if (onSkipDel != null)
            {
                onSkipDel();
            }

        }


        CancelInvoke();
    }

    //if enabled on button a down and up do same



    public void InvokePressedOverSeconds()
    {
        if (isPointerDown)
            onPressedOverSeconds.Invoke();
        if(isControllerDown)
            onPressedOverSeconds.Invoke();
    }

    public void InvokePressedOverSecondsTwo()
    {
        if (isPointerDown)
        {
            onPressedOverSecondsTwo.Invoke();

        }
        if (isControllerDown)
        {
            onPressedOverSecondsTwo.Invoke();

        }
    }

    public void InvokePressedOverSecondsThree()
    {
        if (isPointerDown)
            onPressedOverSecondsThree.Invoke();
        if(isControllerDown)
            onPressedOverSecondsThree.Invoke();
        isThree = true;
        onMegaSkip();
    }
}