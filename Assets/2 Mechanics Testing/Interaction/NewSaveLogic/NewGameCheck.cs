using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NewGameCheck : MonoBehaviour
{
    public GameObject warning; 
    //get the slot and max slot value 

    public void CheckSlots()
    {
        
        string directoryPath = Application.persistentDataPath;
        string folderName = "AutoSave";

       

        string folderPath = Path.Combine(directoryPath, folderName);



        if (Directory.Exists(folderPath))
        {
            string[] filesInSlot = Directory.GetFiles(folderPath);
            if (filesInSlot.Length > 0)
            {
                warning.SetActive(true);
            }
            else
            {
                this.gameObject.GetComponent<VS_StartGame>().StartGame(); 
            }
        }
        else
        {
            Directory.CreateDirectory(folderPath);
            this.gameObject.GetComponent<VS_StartGame>().StartGame();
        }
        
    }


}
