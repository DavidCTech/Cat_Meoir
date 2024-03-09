using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenGammaPanel : MonoBehaviour
{
    public CanvasGroup gammaPanel;
    public GameObject gammaButton, gammaBackButton;

    public void OpenPanel()
    {
        gammaPanel.alpha = 1;
        gammaPanel.interactable = true;
        gammaPanel.blocksRaycasts = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gammaBackButton);
    }

    public void ClosePanel()
    {
        gammaPanel.alpha = 0;
        gammaPanel.interactable = false;
        gammaPanel.blocksRaycasts = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gammaButton);
    }
}
