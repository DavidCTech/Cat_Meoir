using System.Collections;
using System.IO;
using System;
using UnityEngine;
using TMPro;

public class GetTime: MonoBehaviour
{    
    //chatgpt helped write this script
    private PressSlotFile slotScript;
    private int slot;
    private TextMeshProUGUI textObj;
    private string folderName;
    public bool isPopUp = false;
    public YesOverwrite overwriteScript;

    void OnEnable()
    {
        ChangeTime(); 
    }
    public void ChangeTime()
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


            foreach (string file in files)
            {
                DateTime lastWriteTime = File.GetLastWriteTime(file);
                string fileName = Path.GetFileName(file);

                if (fileName.StartsWith("PlayerData_") && lastWriteTime > mostRecentTime)
                {
                    mostRecentTime = lastWriteTime;

                }
            }

            textObj.text = mostRecentTime.ToString();

        }
    }

}

