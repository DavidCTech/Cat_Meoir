using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

public class SetSlotActive : MonoBehaviour
{
    public int slotNumber;
    public GameObject dataButton; 
    // Start is called before the first frame update
    private void OnEnable()
    {
        string directoryPath = Application.persistentDataPath;
        string folderName = "Slot" + slotNumber;

        if (slotNumber == 0)
        {
            folderName = "AutoSave"; 
        }
        
        string folderPath = Path.Combine(directoryPath, folderName);



        if (Directory.Exists(folderPath))
        {
            string[] filesInSlot = Directory.GetFiles(folderPath);
            if (filesInSlot.Length > 0)
            {
                dataButton.SetActive(true);
            }

        }
    }
    public void CheckSlot()
    {
        string directoryPath = Application.persistentDataPath;
        string folderName = "Slot" + slotNumber;

        if (slotNumber == 0)
        {
            folderName = "AutoSave"; 
        }
        
        string folderPath = Path.Combine(directoryPath, folderName);



        if (Directory.Exists(folderPath))
        {
            string[] filesInSlot = Directory.GetFiles(folderPath);
            if (filesInSlot.Length > 0)
            {
                dataButton.SetActive(true);
            }

        }
    }
    
}
