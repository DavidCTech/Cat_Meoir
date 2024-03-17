using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBackManager : MonoBehaviour
{

    public List<GameObject> flashButtons = new List<GameObject>();


    public void LoadButton()
    {
        ButtonData data = SaveSystem.LoadButton();

        if (data != null)
        {
            for (int i = 0; i < data.boolChecks.Length; i++)
            {
                if(data.boolChecks[i] == true)
                {
                    flashButtons[i].SetActive(true);
                }
                else
                {
                    flashButtons[i].SetActive(false);
                }
            }
        }
    }

   public void SaveButton()
   {
        SaveSystem.SaveButton(flashButtons);
   }
    public void DeleteButton()
    {
        SaveSystem.DeleteButton(); 
    }
    
}
