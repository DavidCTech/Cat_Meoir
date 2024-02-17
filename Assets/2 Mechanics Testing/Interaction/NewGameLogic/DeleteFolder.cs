using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DeleteFolder : MonoBehaviour
{
    //made with chat gpt help 
    public string folderName = "AutoSave";

    public void DeleteContents()
    {
        string directoryPath = Path.Combine(Application.persistentDataPath, folderName);

        // Check if the folder exists
        if (Directory.Exists(directoryPath))
        {
            // Get all files and subdirectories in the folder
            string[] files = Directory.GetFiles(directoryPath);
            string[] subdirectories = Directory.GetDirectories(directoryPath);

            // Delete files
            foreach (string file in files)
            {
                File.Delete(file);
            }

            // Delete subdirectories and their contents
            foreach (string subdirectory in subdirectories)
            {
                Directory.Delete(subdirectory, true);
            }

            Debug.Log("Contents of folder deleted: " + directoryPath);
        }
        else
        {
            Debug.LogWarning("Folder does not exist: " + directoryPath);
        }
    }
}