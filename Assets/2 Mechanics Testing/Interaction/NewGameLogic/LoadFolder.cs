using System.Collections;
using System.IO;
using System; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFolder : MonoBehaviour
{
    public void LoadEverything()
    {
        string directoryPath = Application.persistentDataPath;
        string folderName = "AutoSave";

        string folderPath = Path.Combine(directoryPath, folderName);

        string[] files = Directory.GetFiles(folderPath);

        if (files.Length > 0)
        {
            DateTime mostRecentTime = DateTime.MinValue;
            string mostRecentFile = string.Empty;
            string sceneName = string.Empty; 
            foreach (string file in files)
            {
                DateTime lastWriteTime = File.GetLastWriteTime(file);

                // Check if the file starts with "PlayerData_"
                if (file.StartsWith("PlayerData_") && lastWriteTime > mostRecentTime)
                {
                    mostRecentTime = lastWriteTime;
                    mostRecentFile = file;
                }
            }

            //get the scene name from this file 
            sceneName = mostRecentFile.StartsWith("PlayerData_") ? mostRecentFile.Substring("PlayerData_".Length) : string.Empty;
            //load that scene 
            if(sceneName.Length !=  0 ){
                SceneManager.LoadScene(sceneName);

            }

        }
    }

    
}
