using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetCaseNum : MonoBehaviour
{
    public YesOverwrite overwriteScript;
    private TextMeshProUGUI textObj;
    private PressSlotFile slotScript;
    private int slot; 

    // Start is called before the first frame update
    void OnEnable()
    {

        textObj = GetComponent<TextMeshProUGUI>();
        if (overwriteScript != null)
        {
            slotScript = overwriteScript.slotScript;
            slot = slotScript.slotNumber;
        }

       if(slot == 0)
        {
            textObj.text= "Auto Save";
        }
        else
        {
            textObj.text = "Case #" + slot.ToString(); 
        }
    }

    
}
