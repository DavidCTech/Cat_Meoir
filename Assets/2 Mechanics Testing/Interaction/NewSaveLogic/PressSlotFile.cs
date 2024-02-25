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

        // Make sure the slot folder exists
        if (!Directory.Exists(slotFolderPath))
        {
            Directory.CreateDirectory(slotFolderPath);
        }

        // Get all files and directories in the Auto Save folder
        string[] itemsInAutoSave = Directory.GetFileSystemEntries(autoSaveFolderPath);
        string[] filesInSlot = Directory.GetFiles(slotFolderPath);

        foreach (string filePath in filesInSlot)
        {
            string fileName = Path.GetFileName(filePath);
            string filePathInAuto = Path.Combine(autoSaveFolderPath, fileName);

            // Check if the file in the slot exists in Auto Save
            if (!File.Exists(filePathInAuto))
            {
                File.Delete(filePath);
            }
        }

        foreach (string itemPath in itemsInAutoSave)
        {
            string itemName = Path.GetFileName(itemPath);
            string destinationPath = Path.Combine(slotFolderPath, itemName);

            if (Directory.Exists(itemPath))
            {
                // It's a directory, copy it recursively
                CopyDirectory(itemPath, destinationPath);
            }
            else
            {
                // It's a file, copy it
                File.Copy(itemPath, destinationPath, true);
            }
        }
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

        // Make sure the Auto Save folder exists
        if (!Directory.Exists(autoSaveFolderPath))
        {
            Debug.LogWarning("AutoSave folder does not exist: " + autoSaveFolderPath);
            return;
        }

        // Get all files in the Auto Save folder
        string[] filesInAutoSave = Directory.GetFiles(autoSaveFolderPath);
        string[] filesInSlot = Directory.GetFiles(slotFolderPath);

        foreach (string filePath in filesInAutoSave)
        {
            string fileName = Path.GetFileName(filePath);
            string filePathInSlot = Path.Combine(slotFolderPath, fileName);

            // Check if the file in Auto Save exists in the Slot folder
            if (!File.Exists(filePathInSlot))
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

            // Overwrite the file in Auto Save with the file from the Slot folder
            File.Copy(filePath, destinationFilePath, true);
        }
    }
}
