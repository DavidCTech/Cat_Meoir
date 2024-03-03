using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    public List<GameObject> cutSceneButtons = new List<GameObject>();


    public void LoadCuts()
    {
        CutData data = SaveSystem.LoadCuts();

        if (data != null)
        {
            for (int i = 0; i < data.boolChecks.Length; i++)
            {
                if (data.boolChecks[i] == true)
                {
                    cutSceneButtons[i].SetActive(true);
                }
                else
                {
                    cutSceneButtons[i].SetActive(false);
                }
            }
        }
    }

    public void SaveCuts()
    {
        SaveSystem.SaveCuts(cutSceneButtons);
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
