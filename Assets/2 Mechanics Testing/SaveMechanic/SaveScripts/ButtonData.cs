using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ButtonData
{
    public bool[] boolChecks;

    public ButtonData(List<GameObject> buttons)
    {
        boolChecks = new bool[buttons.Count];


        for (int i = 0; i < buttons.Count; i++)
        {
            if (buttons[i] == true)
            {
                boolChecks[i] = true;
            }
            else
            {
                boolChecks[i] = false;
            }
        }


    }
}
