using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    
    //player data
    public static void SavePlayer(Transform playerTransform)
    {
        BinaryFormatter formatter = new BinaryFormatter();


        string path = Application.persistentDataPath + "/PlayerData.CatMeoir";
        FileStream stream = new FileStream(path, FileMode.Create);


        PlayerData data = new PlayerData(playerTransform);

        formatter.Serialize(stream, data);

        stream.Close();

    }
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/PlayerData.CatMeoir";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            Debug.Log("data load in the static save system ");
            return data;

        }

        else
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }

    }

    public static void DeletePlayer()
    {
        string path = Application.persistentDataPath + "/PlayerData.CatMeoir";
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        else
        {
            Debug.LogError("Save File not found in " + path);

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
            Debug.Log("data load in the static save system ");
            return data;

        }

        else
        {
            Debug.LogError("Save File not found in " + path);
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
            Debug.LogError("Save File not found in " + path);

        }

    }


}

