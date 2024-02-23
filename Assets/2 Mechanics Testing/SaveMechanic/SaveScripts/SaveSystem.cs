using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    

    public static void SavePlayer(Transform playerTransform, string sceneName)
    {
        string defaultPath = Application.persistentDataPath;
        string folderName = "AutoSave";

        string folderPath = Path.Combine(defaultPath, folderName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        string fileName = "PlayerData_" + sceneName + ".json"; 
        string path = Path.Combine(folderPath, fileName);
        PlayerData data = new PlayerData(playerTransform);

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonData);
    }

    public static PlayerData LoadPlayer(string sceneName)
    {
        string defaultPath = Application.persistentDataPath;
        string folderName = "AutoSave";

        string path = Path.Combine(defaultPath, folderName);
        string fileName = ("/ PlayerData_" + sceneName + ".json"); 
        path = Path.Combine(path, fileName);

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(jsonData);
            return data;
        }
        else
        {
            return null;
        }
    }

    public static void DeletePlayer(string slotName)
    {
        string defaultPath = Application.persistentDataPath;
        string folderName = slotName;

        string directoryPath = Path.Combine(defaultPath, folderName);
        
        string searchPattern = "playerdata_*";  // Remove the leading slash

        // Combine the directory path and search pattern to create a platform-specific path
        string[] filesToDelete = Directory.GetFiles(directoryPath, searchPattern);

        foreach (string filePath in filesToDelete)
        {
            try
            {
                // Delete each file
                File.Delete(filePath);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error deleting file {filePath}: {e.Message}");
            }
        }
    }
  




    //door data
    public static void SaveDoors(GameObject[] doors)
    {
        string defaultPath = Application.persistentDataPath;

        string folderName = "AutoSave";

        string folderPath = Path.Combine(defaultPath, folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        string fileName = "DoorData.json"; 
        string path = Path.Combine(folderPath, fileName);




        DoorData data = new DoorData(doors);

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonData);

    }
    public static DoorData LoadDoors()
    {   
        string defaultPath = Application.persistentDataPath;

        string folderName = "AutoSave";

        string folderPath = Path.Combine(defaultPath, folderName);
        string fileName = "DoorData.json"; 
        string path = Path.Combine(folderPath, fileName);
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            DoorData data = JsonUtility.FromJson<DoorData>(jsonData);
            return data;
        }
        else
        {
            return null;
        }

    }

    public static void DeleteDoors()
    {
        string defaultPath = Application.persistentDataPath;

        string folderName = "AutoSave";

        string folderPath = Path.Combine(defaultPath, folderName);
        string fileName = "DoorData.json"; 
        string path = Path.Combine(folderPath, fileName);
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        else
        {
            //Debug.LogError("Save File not found in " + path);

        }

    }


    public static void SaveClues(List<PhotoScriptable> clues)
    {
        string defaultPath = Application.persistentDataPath;

        string folderName = "AutoSave";

        string folderPath = Path.Combine(defaultPath, folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        string fileName = "ClueData.json"; 
        string path = Path.Combine(folderPath, fileName);
        ClueData data = new ClueData(clues);

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonData);
    }

    public static ClueData LoadClues()
    {
        
        string defaultPath = Application.persistentDataPath;

        string folderName = "AutoSave";

        string folderPath = Path.Combine(defaultPath, folderName);
        string fileName = "ClueData.json"; 
        string path = Path.Combine(folderPath, fileName);

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            ClueData data = JsonUtility.FromJson<ClueData>(jsonData);
            return data;
        }
        else
        {
            return null;
        }
    }


    public static void DeleteClues()
    {
        
        string defaultPath = Application.persistentDataPath;

        string folderName = "AutoSave";

        string folderPath = Path.Combine(defaultPath, folderName);
        string fileName = "ClueData.json"; 
        string path = Path.Combine(folderPath, fileName);

        if (File.Exists(path))
        {
            File.Delete(path);
        }

        else
        {
            //Debug.LogError("Save File not found in " + path);

        }

    }


    //save load and delete clue images will have to be taken care of differently 
    public static void DeleteClueImages()
    {
        string defaultPath = Application.persistentDataPath;
        string folderName = "AutoSave";

        string firstFolderPath = Path.Combine(defaultPath, folderName);
        string secondFolderName = "CatMeoirSavedImages";
        string folderPath = Path.Combine(firstFolderPath, secondFolderName);


        if (Directory.Exists(folderPath))
        {
            // Get all files in the folder
            string[] files = Directory.GetFiles(folderPath);

            // Iterate through the files and delete each one
            foreach (string file in files)
            {
                File.Delete(file);
                Debug.Log("Deleted file: " + file);
            }

            Debug.Log("All files in the folder have been deleted.");
        }
        
    }


    //save npc functions
    public static void SaveNPC(string npcName, List<GameObject> dialogList, DialogData selectedOption)
    {
        string defaultPath = Application.persistentDataPath;

        string folderName = "AutoSave";

        string folderPath = Path.Combine(defaultPath, folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        string fileName = "NPCData_" + npcName + ".json"; 
        string path = Path.Combine(folderPath, fileName);


        NPCData data = new NPCData(dialogList, selectedOption);

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonData);
    }

    public static NPCData LoadNPC(string npcName)
    {
        string defaultPath = Application.persistentDataPath;

        string folderName = "AutoSave";

        string folderPath = Path.Combine(defaultPath, folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        string fileName = "NPCData_" + npcName + ".json"; 
        string path = Path.Combine(folderPath, fileName);

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            NPCData data = JsonUtility.FromJson<NPCData>(jsonData);
            return data;
        }
        else
        {
            return null;
        }
    }

    public static void DeleteNPC()
    {
        string defaultPath = Application.persistentDataPath;

        string folderName = "AutoSave";

        string directoryPath = Path.Combine(defaultPath, folderName);
       
        string searchPattern = "NPCData*";  

        // Combine the directory path and search pattern to create a platform-specific path
        string[] filesToDelete = Directory.GetFiles(directoryPath, searchPattern);

        foreach (string filePath in filesToDelete)
        {
            try
            {
                // Delete each file
                File.Delete(filePath);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error deleting file {filePath}: {e.Message}");
            }
        }
    }

    //cut save functions
    public static void SaveCuts(List<UICutSceneToggle> toggles)
    {

        string defaultPath = Application.persistentDataPath;

        string folderName = "AutoSave";

        string folderPath = Path.Combine(defaultPath, folderName);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        string fileName = "CutData.json"; 
        string path = Path.Combine(folderPath, fileName);

        CutData data = new CutData(toggles);

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonData);
    }

    public static CutData LoadCuts()
    {
        string defaultPath = Application.persistentDataPath;

        string folderName = "AutoSave";

        string folderPath = Path.Combine(defaultPath, folderName);
        string fileName = "CutData.json"; 
        string path = Path.Combine(folderPath, fileName);

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            CutData data = JsonUtility.FromJson<CutData>(jsonData);
            return data;
        }
        else
        {
            return null;
        }
    }

    public static void DeleteCuts()
    {
         string defaultPath = Application.persistentDataPath;

        string folderName = "AutoSave";

        string folderPath = Path.Combine(defaultPath, folderName);
        string fileName = "CutData.json"; 
        string path = Path.Combine(folderPath, fileName);
        
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        else
        {
            //Debug.LogError("Save File not found in " + path);

        }

    }



}




