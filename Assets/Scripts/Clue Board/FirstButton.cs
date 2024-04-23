using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstButton : MonoBehaviour
{
    Button button;

    public void OnEnable()
    {
        button = GetComponent<Button>();
        button.Select();
    }
}
