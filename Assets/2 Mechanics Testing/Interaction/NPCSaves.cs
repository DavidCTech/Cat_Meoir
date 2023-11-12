using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NPCSaves : MonoBehaviour
{
    public string npcName;
    
    /*
    public void SaveNPC(DialogScriptable dialogObject)
    {
        npcPath = Application.persistentDataPath + "/NPCData_" + npcName;
        dialogObject.SaveToFile(npcPath);
    }

    public DialogScriptable LoadNPC()
    {
        npcPath = Application.persistentDataPath + "/NPCData_" + npcName;
        if (File.Exists(npcPath))
        {
            string jsonData = File.ReadAllText(npcPath);
            return JsonUtility.FromJson<DialogScriptable>(jsonData);
        }
        else
        {
            Debug.Log("File not found: " + npcPath);
            return null;
        }
    }
    */
    public void SaveNPC()
    {
        SaveSystem.SaveNPC(npcName);
    }


}
 