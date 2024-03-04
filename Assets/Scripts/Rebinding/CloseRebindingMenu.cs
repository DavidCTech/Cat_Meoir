using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CloseRebindingMenu : MonoBehaviour
{
    public CanvasGroup accessibilityCanvasGroup, rebindingCanvasGroup;
    public GameObject rebindingCancelMenu;

    void Start()
    {

    }

    void Update()
    {
        Gamepad gamepad = Gamepad.current;
        Keyboard keyboard = Keyboard.current;

        if (keyboard != null)
        {
            if (keyboard.backspaceKey.wasPressedThisFrame)
            {
                if (!rebindingCancelMenu.activeInHierarchy && accessibilityCanvasGroup.gameObject.activeInHierarchy)
                {
                    rebindingCanvasGroup.alpha = 0;
                    rebindingCanvasGroup.interactable = false;
                    rebindingCanvasGroup.blocksRaycasts = false;

                    accessibilityCanvasGroup.alpha = 1;
                    accessibilityCanvasGroup.interactable = true;
                    accessibilityCanvasGroup.blocksRaycasts = true;
                }
            }
        }

        if (gamepad != null)
        {
            if (gamepad.buttonEast.wasPressedThisFrame)
            {
                if (!rebindingCancelMenu.activeInHierarchy && accessibilityCanvasGroup.gameObject.activeInHierarchy)
                {
                    rebindingCanvasGroup.alpha = 0;
                    rebindingCanvasGroup.interactable = false;
                    rebindingCanvasGroup.blocksRaycasts = false;

                    accessibilityCanvasGroup.alpha = 1;
                    accessibilityCanvasGroup.interactable = true;
                    accessibilityCanvasGroup.blocksRaycasts = true;
                }
            }
        }
    }
}
