using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{

    public List<ClueEventTrigger> triggers = new List<ClueEventTrigger>();


    public void LoadCuts()
    {
        Debug.Log("loading cuts");
        CutData data = SaveSystem.LoadCuts();

        if (data != null)
        {
            for (int i = 0; i < data.boolChecks.Length; i++)
            {
                if (data.boolChecks[i] == true)
                {
                    Debug.Log("bool checks is true?");
                    triggers[i].isActive= true;
                    //you also want to set the associated cut scene to disable, so you can call the on enable event for this object 
                    
                }
                else
                {
                    triggers[i].isActive = false;
                }
            }
        }
        else
        {
            Debug.Log("data null");
        }
    }

    public void SaveCuts()
    {
        Debug.Log("saving cuts");
        SaveSystem.SaveCuts(triggers);
    }
    public void DeleteCuts()
    {
        SaveSystem.DeleteCuts();
    }

    //this was the old scripting where it uses more specific code- this was before cut scene display decisions
    /*
    public List<UICutSceneToggle> toggles = new List<UICutSceneToggle>();


    public void LoadCuts()
    {
        CutData data = SaveSystem.LoadCuts();

        if (data != null)
        {
            for (int i = 0; i < data.boolChecks.Length; i++)
            {
                if(data.boolChecks[i] == true)
                {
                    toggles[i].RenderTextureAdd(); 
                }
            }
        }
    }

   public void SaveCuts()
   {
        SaveSystem.SaveCuts(toggles);
   }
    public void DeleteCuts()
    {
        SaveSystem.DeleteCuts(); 
    }
    */
}
