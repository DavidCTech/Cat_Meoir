using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JustCruisingMode : MonoBehaviour
{


    private bool isObjectActive = true;

    public void ToggleObject(bool isToggled)
    {
        gameObject.SetActive(false);
    }

}
