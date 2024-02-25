using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesOverwrite : MonoBehaviour
{
    public PressSlotFile slotScript;

    // Start is called before the first frame update
    public void PressDown()
    {
        slotScript.RewriteAuto(); 
    }
    public void SaveDown(){
        slotScript.RewriteSlot();
    }

  
}
