using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetToDefaultsPanel : MonoBehaviour
{
    public GameObject resetPanel;
    public GameObject yesButton;
    public GameObject resetButton;

    public void OpenPanel()
    {
        resetPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(yesButton);
    }

    public void ClosePanel()
    {
        resetPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resetButton);
    }
}
