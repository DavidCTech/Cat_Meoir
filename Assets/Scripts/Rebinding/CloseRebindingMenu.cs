using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CloseRebindingMenu : MonoBehaviour
{
    public GameObject accessibilityMenu, rebindingMenu, rebindingCancelMenu;

    void Start()
    {

    }

    void Update()
    {
        Gamepad gamepad = Gamepad.current;
        Keyboard keyboard = Keyboard.current;

        if (keyboard != null)
        {
            if (keyboard.escapeKey.wasPressedThisFrame)
            {
                if (!rebindingCancelMenu.activeInHierarchy && rebindingMenu.activeInHierarchy)
                {
                    rebindingMenu.SetActive(false);
                    accessibilityMenu.SetActive(true);
                }
            }
        }

        if (gamepad != null)
        {
            if (gamepad.buttonEast.wasPressedThisFrame)
            {
                if (!rebindingCancelMenu.activeInHierarchy && rebindingMenu.activeInHierarchy)
                {
                    rebindingMenu.SetActive(false);
                    accessibilityMenu.SetActive(true);
                }
            }
        }

    }
}
