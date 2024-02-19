using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class PressSlotFile : MonoBehaviour
{
    //chat gpt helped write this script 
    public int slotNumber;
    public GameObject overWrite;
    public GameObject loadYes; 
    // Start is called before the first frame update
    public void ShowOverWrite()
    {
        Debug.Log("click");
        string directoryPath = Application.persistentDataPath;
        string folderName = "Slot" + slotNumber;
        string folderPath = Path.Combine(directoryPath, folderName);

       

        //search in the persistent data path for the folder name and look for the files in that folder
        if (Directory.Exists(folderPath))
        {
            string[] filesInSlot = Directory.GetFiles(folderPath);
            if (filesInSlot.Length > 0)
            {
                //give this script to the overwrite Yes 
                loadYes.GetComponent<YesOverwrite>().slotScript = this;
                //show overwrite 
                Debug.Log("this");
                overWrite.SetActive(true);
                
            }
        }
        
    }
    //if this slot file is being rewriteen by the data in auto save 
    public void RewriteSlot()
    {
        string directoryPath = Application.persistentDataPath;
        string slotFolderName = "Slot" + slotNumber;
        string autoSaveFolderName = "AutoSave";

        string slotFolderPath = Path.Combine(directoryPath, slotFolderName);
        string autoSaveFolderPath = Path.Combine(directoryPath, autoSaveFolderName);

        // Make sure the Auto Save folder exists
        if (!Directory.Exists(autoSaveFolderPath))
        {
            Debug.LogWarning("AutoSave folder does not exist: " + autoSaveFolderPath);
            return;
        }

        // Make sure the slot folder exists
        if (!Directory.Exists(slotFolderPath))
        {
            Directory.CreateDirectory(slotFolderPath);
        }

        // Get all files in the Auto Save folder
        string[] filesInAutoSave = Directory.GetFiles(autoSaveFolderPath);
        string[] filesInSlot = Directory.GetFiles(slotFolderPath);
   
        foreach ( string filePath in filesInSlot)
        {
            string fileName = Path.GetFileName(filePath);
            string fileNameInAuto = Path.Combine(autoSaveFolderPath, fileName);
            if(!File.Exists(fileNameInAuto))
            {
                File.Delete(filePath);
            }


        }

        foreach (string filePath in filesInAutoSave)
        {
            //auto to slot- check if they dont contain something 
           

            // Get the file name without the directory path
            string fileName = Path.GetFileName(filePath);

            // Construct the destination file path in the slot folder
            string destinationFilePath = Path.Combine(slotFolderPath, fileName);

            // Overwrite the file in the slot folder with the file from Auto Save
            File.Copy(filePath, destinationFilePath, true);
        }

        Debug.Log("Slot " + slotNumber + " overwritten with data from AutoSave");
    }

    //if this slot file is rewriting the data in the auto save 
    public void RewriteAuto()
    {
        string directoryPath = Application.persistentDataPath;
        string slotFolderName = "Slot" + slotNumber;
        string autoSaveFolderName = "AutoSave";

        string slotFolderPath = Path.Combine(directoryPath, slotFolderName);
        string autoSaveFolderPath = Path.Combine(directoryPath, autoSaveFolderName);

        // Make sure the slot folder exists
        if (!Directory.Exists(slotFolderPath))
        {
            Debug.LogWarning("Slot folder does not exist: " + slotFolderPath);
            return;
        }

        // Make sure the Auto Save folder exists
        if (!Directory.Exists(autoSaveFolderPath))
        {
            Directory.CreateDirectory(autoSaveFolderPath);
        }

        // Get all files in the slot folder
        string[] filesInSlot = Directory.GetFiles(slotFolderPath);
        string[] filesInAuto = Directory.GetFiles(autoSaveFolderPath);
        foreach ( string filePath in filesInAuto)
        {
            string fileName = Path.GetFileName(filePath);
            string fileNameInSlot = Path.Combine(slotFolderPath, fileName);
            if(!File.Exists(fileNameInSlot))
            {
                File.Delete(filePath);
            }


        }


        foreach (string filePath in filesInSlot)
        {
            // Get the file name without the directory path
            string fileName = Path.GetFileName(filePath);

            // Construct the destination file path in the Auto Save folder
            string destinationFilePath = Path.Combine(autoSaveFolderPath, fileName);

            // Overwrite the file in Auto Save with the file from the slot folder
            File.Copy(filePath, destinationFilePath, true);
        }

        Debug.Log("Auto save overwritten with data from slot " + slotNumber);
    }
}
