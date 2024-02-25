using System.Collections;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI; 

public class GetImage : MonoBehaviour
{
    //chatgpt helped write this script
    private PressSlotFile slotScript;
    private int slot;
    private Image imageObj;
    private string folderName;
    public bool isPopUp = false;
    public YesOverwrite overwriteScript; 
  
    // Start is called before the first frame update
    void OnEnable()
    {
        ChangeImage(); 
        
    }
     public void ChangeImage()
    {
        //get the slot data 
        if (!isPopUp)
        {
            slotScript = GetComponentInParent<PressSlotFile>();
            slot = slotScript.slotNumber;
        }
        else
        {
            if(overwriteScript!= null)
            {
                slotScript = overwriteScript.slotScript; 
                slot = slotScript.slotNumber;
            }
            
        }
       
        //get the image obj 
        imageObj = GetComponent<Image>();
        string directoryPath = Application.persistentDataPath;

        if (slot == 0)
        {
            folderName = "AutoSave";
        }
        else
        {
            folderName = $"Slot{slot}";
        }

        string firstFolderPath = Path.Combine(directoryPath, folderName);
        string imageFolder = "CatMeoirSavedImages";
        string folderPath = Path.Combine(firstFolderPath, imageFolder);

        if (Directory.Exists(folderPath))
        {
            string[] files = Directory.GetFiles(folderPath);

            if (files.Length > 0)
            {

                DateTime mostRecentTime = DateTime.MinValue;
                string mostRecentFile = string.Empty;
                string sceneName = string.Empty;
                foreach (string file in files)
                {
                    DateTime lastWriteTime = File.GetLastWriteTime(file);
                    string fileName = Path.GetFileName(file);
                    // Check if the file starts with "PlayerData_"
                    if (lastWriteTime > mostRecentTime)
                    {
                        mostRecentTime = lastWriteTime;
                        mostRecentFile = fileName;
                    }
                }
                string filePath = Path.Combine(folderPath, mostRecentFile);


                byte[] pngData = File.ReadAllBytes(filePath);
                Texture2D loadedTexture = new Texture2D(1080, 1080);
                loadedTexture.LoadImage(pngData);
                Sprite loadedSprite = Sprite.Create(loadedTexture, new Rect(0, 0, loadedTexture.width, loadedTexture.height), new Vector2(0.5f, 0.5f));
                imageObj.sprite = loadedSprite;
            }
        }
    }
}

