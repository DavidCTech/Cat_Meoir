using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEventTriggers : MonoBehaviour
{

    public void OnPointerEnter(BaseEventData baseEventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

    public void OnPointerExit(BaseEventData baseEventData)
    {

    }

    public void OnSelect(BaseEventData baseEventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

    public void OnDeselect(BaseEventData baseEventData)
    {

    }
}
