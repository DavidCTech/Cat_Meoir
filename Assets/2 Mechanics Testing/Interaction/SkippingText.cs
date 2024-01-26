using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkippingText : MonoBehaviour
{
    //Script made with help from chatgpt
    private TextMeshProUGUI textComponent;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void ChangeText(string text)
    {
        if (textComponent != null)
        {
            textComponent.text = text;
        }
    }
}