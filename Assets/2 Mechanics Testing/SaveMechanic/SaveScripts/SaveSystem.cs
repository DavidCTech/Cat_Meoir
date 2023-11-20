using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    
    //player data
    public static void SavePlayer(Transform playerTransform, string sceneName)
    {
        BinaryFormatter formatter = new BinaryFormatter();


        string path = Application.persistentDataPath + "/PlayerData_" + sceneName +".CatMeoir";
        FileStream stream = new FileStream(path, FileMode.Create);


        PlayerData data = new PlayerData(playerTransform);

        formatter.Serialize(stream, data);

        stream.Close();

    }
    public static PlayerData LoadPlayer(string sceneName)
    {
        string path = Application.persistentDataPath + "/PlayerData_" + sceneName + ".CatMeoir";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            return data;

        }

        else
        {
           // Debug.LogError("Save File not found in " + path);
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
        BinaryFormatter formatter = new BinaryFormatter();


        string path = Application.persistentDataPath + "/DoorData.CatMeoir";
        FileStream stream = new FileStream(path, FileMode.Create);


        DoorData data = new DoorData(doors);

        formatter.Serialize(stream, data);

        stream.Close();

    }
    public static DoorData LoadDoors()
    {
        string path = Application.persistentDataPath + "/DoorData.CatMeoir";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DoorData data = formatter.Deserialize(stream) as DoorData;

            stream.Close();
            return data;

        }

        else
        {
            //Debug.LogError("Save File not found in " + path);
            return null;
        }

    }

    public static void DeleteDoors()
    {
        string path = Application.persistentDataPath + "/DoorData.CatMeoir";
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        else
        {
            //Debug.LogError("Save File not found in " + path);

        }

    }





    //Clue Data
    public static void SaveClues(List<PhotoScriptable> clues)
    {
        BinaryFormatter formatter = new BinaryFormatter();


        string path = Application.persistentDataPath + "/ClueData.CatMeoir";
        FileStream stream = new FileStream(path, FileMode.Create);


        ClueData data = new ClueData(clues);

        formatter.Serialize(stream, data);

        stream.Close();

    }
    public static ClueData LoadClues()
    {
        string path = Application.persistentDataPath + "/ClueData.CatMeoir";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            ClueData data = formatter.Deserialize(stream) as ClueData;

            stream.Close();
            return data;

        }

        else
        {
            //Debug.LogError("Save File not found in " + path);
            return null;
        }

    }

    public static void DeleteClues()
    {
        string path = Application.persistentDataPath + "/ClueData.CatMeoir";
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

    public static void SaveNPC(string npcName, List<GameObject> dialogList, DialogData selectedOption)
    {
        BinaryFormatter formatter = new BinaryFormatter();


        string path = Application.persistentDataPath + "/NPCData_" + npcName +".CatMeoir";
        FileStream stream = new FileStream(path, FileMode.Create);


        NPCData data = new NPCData(dialogList, selectedOption);

        formatter.Serialize(stream, data);
        


        stream.Close();

    }
    public static NPCData LoadNPC(string npcName)
    {
        string path = Application.persistentDataPath + "/NPCData_" + npcName + ".CatMeoir";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            NPCData data = formatter.Deserialize(stream) as NPCData;

            stream.Close();
            return data;

        }

        else
        {
            //Debug.LogError("Save File not found in " + path);
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


}




