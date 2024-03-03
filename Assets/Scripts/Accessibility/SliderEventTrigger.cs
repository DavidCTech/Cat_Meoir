using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderEventTrigger : MonoBehaviour
{
    public GameObject sliderTipText;

    void Start()
    {
        sliderTipText.gameObject.SetActive(false);
    }

    public void OnPointerEnter(BaseEventData baseEventData)
    {
        sliderTipText.gameObject.SetActive(true);
    }

    public void OnPointerExit(BaseEventData baseEventData)
    {
        sliderTipText.gameObject.SetActive(false);
    }

    public void OnSelect(BaseEventData baseEventData)
    {
        sliderTipText.gameObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData baseEventData)
    {
        sliderTipText.gameObject.SetActive(false);
    }
}
