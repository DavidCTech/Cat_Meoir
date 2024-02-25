using System.Collections;
using System.IO;
using System; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFolder : MonoBehaviour
{
    public void LoadEverything()
    {
        Debug.Log("starting load");
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
                Debug.Log("this file " + file);
                string fileName = Path.GetFileName(file);
                // Check if the file starts with "PlayerData_"
                if (fileName.StartsWith("PlayerData_") && lastWriteTime > mostRecentTime)
                {
                    mostRecentTime = lastWriteTime;
                    mostRecentFile = fileName;
                }
            }
            Debug.Log("Most recent file: " + mostRecentFile);
            //get the scene name from this file 
            sceneName = mostRecentFile.StartsWith("PlayerData_") ? mostRecentFile.Substring("PlayerData_".Length) : string.Empty;
            sceneName = Path.GetFileNameWithoutExtension(sceneName);
            //load that scene 
            if(sceneName.Length !=  0 ){
                Debug.Log(sceneName);
                SceneManager.LoadScene(sceneName);

            }

        }
        Debug.Log("Finished Load");
    }

    
}
