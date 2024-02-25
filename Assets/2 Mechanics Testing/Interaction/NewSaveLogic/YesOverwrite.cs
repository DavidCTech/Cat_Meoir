using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesOverwrite : MonoBehaviour
{
    public PressSlotFile slotScript;
    private GameObject slotObj; 

    // Start is called before the first frame update
    public void PressDown()
    {
        Debug.Log("Starting rewrite");
        slotScript.RewriteAuto();
        slotObj = slotScript.gameObject;
        slotObj.GetComponentInChildren<GetImage>().ChangeImage();
        slotObj.GetComponentInChildren<GetLocation>().ChangeLocation();
        slotObj.GetComponentInChildren<GetTime>().ChangeTime();
    }
    public void SaveDown()
    {

     
        slotScript.RewriteSlot();
        slotObj = slotScript.gameObject;
        slotObj.GetComponentInChildren<GetImage>().ChangeImage();
        slotObj.GetComponentInChildren<GetLocation>().ChangeLocation();
        slotObj.GetComponentInChildren<GetTime>().ChangeTime();
    }

  
}
