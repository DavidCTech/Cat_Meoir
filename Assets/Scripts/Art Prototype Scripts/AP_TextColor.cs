using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AP_TextColor : MonoBehaviour
{
    public TMP_Text Text;
    public Image pawPrint;
    public Color selected, deselected;

    public void OnSelect()
    {
        Text.color = selected;
        pawPrint.gameObject.SetActive(true);
    }
    public void OnDeselect()
    {
        Text.color = deselected;
        pawPrint.gameObject.SetActive(false);
    }
    public void OnPointerEnter()
    {
        Text.color = selected;
        pawPrint.gameObject.SetActive(true);
    }
    public void OnPointerExit()
    {
        Text.color = selected;
        pawPrint.gameObject.SetActive(true);
    }
}