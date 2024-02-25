using System.Collections;
using System.IO;
using System;
using UnityEngine;
using TMPro;

public class GetLocation : MonoBehaviour
{
    //chatgpt helped write this script
    private PressSlotFile slotScript;
    private int slot;
    private TextMeshProUGUI textObj;
    private string folderName;
    public bool isPopUp = false;
    public YesOverwrite overwriteScript;

    // Start is called before the first frame update
    void OnEnable()
    {
        ChangeLocation(); 
    }
    public void ChangeLocation()
    {
        if (!isPopUp)
        {
            slotScript = GetComponentInParent<PressSlotFile>();
            slot = slotScript.slotNumber;
        }
        else
        {
            if (overwriteScript != null)
            {
                slotScript = overwriteScript.slotScript;
                slot = slotScript.slotNumber;
            }

        }
        //get the text obj 
        textObj = GetComponent<TextMeshProUGUI>();
        string directoryPath = Application.persistentDataPath;

        if (slot == 0)
        {
            folderName = "AutoSave";
        }
        else
        {
            folderName = $"Slot{slot}";
        }

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
                Debug.Log("file " + file);
                string fileName = Path.GetFileName(file);
                // Check if the file starts with "PlayerData_"
                if (fileName.StartsWith("PlayerData_") && lastWriteTime > mostRecentTime)
                {
                    mostRecentTime = lastWriteTime;
                    mostRecentFile = fileName;
                }
            }
            Debug.Log(mostRecentFile);
            //get the scene name from this file 
            sceneName = mostRecentFile.StartsWith("PlayerData_") ? mostRecentFile.Substring("PlayerData_".Length) : string.Empty;
            sceneName = Path.GetFileNameWithoutExtension(sceneName);
            textObj.text = sceneName;

        }
    }
    
}
