using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenGammaPanel : MonoBehaviour
{
    public CanvasGroup gammaPanel;
    public CanvasGroup gammaOverlayPanel;
    public GameObject gammaButton, gammaBackButton;


    void Start()
    {

    }

    public void OpenPanel()
    {
        gammaPanel.alpha = 1;
        gammaPanel.interactable = true;
        gammaPanel.blocksRaycasts = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gammaBackButton);
        gammaOverlayPanel.alpha = 0;
    }

    public void ClosePanel()
    {
        gammaPanel.alpha = 0;
        gammaPanel.interactable = false;
        gammaPanel.blocksRaycasts = false;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(gammaButton);
        gammaOverlayPanel.alpha = 0;
    }
}
