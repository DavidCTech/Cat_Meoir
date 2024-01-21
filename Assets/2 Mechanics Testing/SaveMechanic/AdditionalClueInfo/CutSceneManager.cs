using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
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
}
