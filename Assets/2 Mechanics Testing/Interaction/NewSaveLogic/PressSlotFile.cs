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
                
                overWrite.SetActive(true);
                
            }
        }
        
    }
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

        // Remove the existing slot folder and recreate it
        if (Directory.Exists(slotFolderPath))
        {
            Directory.Delete(slotFolderPath, true);
        }

        Directory.CreateDirectory(slotFolderPath);

        // Copy the contents of Auto Save to the slot folder
        CopyDirectory(autoSaveFolderPath, slotFolderPath);
    }

    // Helper method to copy directories recursively
    private void CopyDirectory(string sourceDir, string destinationDir)
    {
        Directory.CreateDirectory(destinationDir);

        foreach (string file in Directory.GetFiles(sourceDir))
        {
            string dest = Path.Combine(destinationDir, Path.GetFileName(file));
            File.Copy(file, dest, true);
        }

        foreach (string folder in Directory.GetDirectories(sourceDir))
        {
            string dest = Path.Combine(destinationDir, Path.GetFileName(folder));
            CopyDirectory(folder, dest);
        }
    }

    //if this slot file is rewriting the data in the auto save 
    public void RewriteAuto()
    {
        string directoryPath = Application.persistentDataPath;
        string slotFolderName = "Slot" + slotNumber;
        string autoSaveFolderName = "AutoSave";

        string slotFolderPath = Path.Combine(directoryPath, slotFolderName);
        string autoSaveFolderPath = Path.Combine(directoryPath, autoSaveFolderName);

        // Make sure the Slot folder exists
        if (!Directory.Exists(slotFolderPath))
        {
            Debug.LogWarning("Slot folder does not exist: " + slotFolderPath);
            return;
        }

        // Remove the existing slot folder and recreate it
        if (Directory.Exists(autoSaveFolderPath))
        {
            Directory.Delete(autoSaveFolderPath, true);
        }

        Directory.CreateDirectory(autoSaveFolderPath);

        // Copy the contents of Auto Save to the slot folder
        CopyDirectory(slotFolderPath, autoSaveFolderPath);

        Debug.Log("Auto rewrite complete");
    }
}
