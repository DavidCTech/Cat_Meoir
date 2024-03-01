using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleTextTip : MonoBehaviour
{
    [TextArea(3, 5)] public string accessibilityTipString;
    public TMP_Text accessibilityTipText;

    void Start()
    {

    }

    public void OnPointerEnter(BaseEventData baseEventData)
    {
        ChangeText();
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

    public void OnPointerExit(BaseEventData baseEventData)
    {

    }

    public void OnSelect(BaseEventData baseEventData)
    {
        ChangeText();
    }

    public void OnDeselect(BaseEventData baseEventData)
    {
        ResetText();
    }

    public void ChangeText()
    {
        accessibilityTipText.text = accessibilityTipString.ToString();
    }

    public void ResetText()
    {
        accessibilityTipText.text = "*What Does This Accessibility Feature Do...".ToString();
    }


}
