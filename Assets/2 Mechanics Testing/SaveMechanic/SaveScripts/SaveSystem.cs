using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    public static void SavePlayer(Transform playerTransform, string sceneName)
    {
        string path = Application.persistentDataPath + "/PlayerData_" + sceneName + ".CatMeoir.json";

        PlayerData data = new PlayerData(playerTransform);

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonData);
    }

    public static PlayerData LoadPlayer(string sceneName)
    {
        string path = Application.persistentDataPath + "/PlayerData_" + sceneName + ".json";
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

    public static void DeletePlayer()
    {
        string directoryPath = Application.persistentDataPath;
        string searchPattern = "playerdata*";  // Remove the leading slash

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
        string path = Application.persistentDataPath + "/DoorData.json";
        DoorData data = new DoorData(doors);

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonData);

    }
    public static DoorData LoadDoors()
    {
        string path = Application.persistentDataPath + "/ClueData.json";
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
        string path = Application.persistentDataPath + "/DoorData.json";
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
        string path = Application.persistentDataPath + "/ClueData.json";
        ClueData data = new ClueData(clues);

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonData);
    }

    public static ClueData LoadClues()
    {
        string path = Application.persistentDataPath + "/ClueData.json";
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
        string path = Application.persistentDataPath + "/ClueData.json";
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        else
        {
            //Debug.LogError("Save File not found in " + path);

        }

    }
    public static void DeleteClueImages()
    {
        string folderPath = Application.persistentDataPath + "/CatMeoirSavedImages";

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
        else
        {
            //Debug.LogError("Folder not found: " + folderPath);
        }
    }


    //save npc functions
    public static void SaveNPC(string npcName, List<GameObject> dialogList, DialogData selectedOption)
    {
        string path = Application.persistentDataPath + "/NPCData_" + npcName + ".json";
        NPCData data = new NPCData(dialogList, selectedOption);

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonData);
    }

    public static NPCData LoadNPC(string npcName)
    {
        string path = Application.persistentDataPath + "/NPCData_" + npcName + ".json";
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
        string directoryPath = Application.persistentDataPath;
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
        string path = Application.persistentDataPath + "/CutData.json";
        CutData data = new CutData(toggles);

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(path, jsonData);
    }

    public static CutData LoadCuts()
    {
        string path = Application.persistentDataPath + "/CutData.json";
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
        string path = Application.persistentDataPath + "/CutData.json";
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




