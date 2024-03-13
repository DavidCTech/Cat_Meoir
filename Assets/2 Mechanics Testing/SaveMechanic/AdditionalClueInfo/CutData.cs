using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutData
{
    public bool[] boolChecks;

    public CutData(List<ClueEventTrigger> logicObject)
    {

        boolChecks = new bool[logicObject.Count];
        for (int i = 0; i < logicObject.Count; i++)
        {
            if(logicObject[i] != null)
            {
                if (logicObject[i].isActive)
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

    //this was when cut data altered button state- but this was changed to logic checking with completion 
    /*
    public CutData(List<GameObject> buttons)
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
    */

    //this was more specific data before the display for cut scenes changed 
    /*

    public CutData(List<UICutSceneToggle> toggles)
    {
        boolChecks = new bool[toggles.Count];


        for (int i = 0; i < toggles.Count; i++)
        {
            if (toggles[i] == true)
            {
                boolChecks[i] = true;
            }
            else
            {
                boolChecks[i] = false;
            }
        }


    }
    */
}
