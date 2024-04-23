using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour
{
    public GameObject[] buttonsToSelect;

    public void SelectButtons(int index)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttonsToSelect[index]);
    }
}
