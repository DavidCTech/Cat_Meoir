using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenAccessibilityMenu : MonoBehaviour
{
    public CanvasGroup accessibilityMenu;
    public CanvasGroup gammaPanel;
    public GameObject highContrastToggle, buttonWhenClosed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (accessibilityMenu.alpha == 0)
            {
                accessibilityMenu.alpha = 1;
                accessibilityMenu.interactable = true;
                accessibilityMenu.blocksRaycasts = true;
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(highContrastToggle);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                if (gammaPanel.alpha == 1)
                {
                    gammaPanel.alpha = 0;
                }

                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                accessibilityMenu.alpha = 0;
                accessibilityMenu.interactable = false;
                accessibilityMenu.blocksRaycasts = false;
                EventSystem.current.SetSelectedGameObject(null);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                if (gammaPanel.alpha == 0)
                {
                    gammaPanel.alpha = 1;
                }
            }
        }
    }

    public void OpenTheAccessibilityMenu()
    {
        if (accessibilityMenu.alpha == 0)
        {
            accessibilityMenu.alpha = 1;
            accessibilityMenu.interactable = true;
            accessibilityMenu.blocksRaycasts = true;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(highContrastToggle);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if (gammaPanel.alpha == 1)
            {
                gammaPanel.alpha = 0;
            }
        }
    }

    public void CloseTheAccessibilityMenu()
    {
        accessibilityMenu.alpha = 0;
        accessibilityMenu.interactable = false;
        accessibilityMenu.blocksRaycasts = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttonWhenClosed);
    }
}
