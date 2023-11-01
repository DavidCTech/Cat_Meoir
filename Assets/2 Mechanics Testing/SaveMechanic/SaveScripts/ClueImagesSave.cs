using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

//made with help from chat gpt and stack overflow
public class ClueImagesSave : MonoBehaviour
{
    private PhotoManager photoManager;
    private string saveFolder = "CatMeoirSavedImages"; // Folder name where the images will be saved

    [Header("Grab from Asset Folder >scripts>mechanic Texture. ")]
    public RenderTexture renderTexture; 


    private void Start()
    {
        photoManager = this.GetComponent<PhotoManager>(); 
    }

    public void SaveClueImages()
    {
        
        // Create the folder if it doesn't exist
        string folderPath = Path.Combine(Application.persistentDataPath, saveFolder);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        for (int i = 0; i < photoManager.snapshots.Count; i++)
        {

            
            Texture2D texture2D = photoManager.snapshots[i].texture;
            

            byte[] pngData = texture2D.EncodeToPNG();
            string fileName = $"savedSprite_{i}.png";
            string filePath = Path.Combine(folderPath, fileName);
            File.WriteAllBytes(filePath, pngData);
        }
    }

    public Sprite LoadClueImages(int i)
    {
        string folderPath = Path.Combine(Application.persistentDataPath, saveFolder);
        string fileName = $"savedSprite_{i}.png";
        string filePath = Path.Combine(folderPath, fileName);
        byte[] pngData = File.ReadAllBytes(filePath);
        Texture2D loadedTexture = new Texture2D(1080, 1080);  
        loadedTexture.LoadImage(pngData);
        Sprite loadedSprite = Sprite.Create(loadedTexture, new Rect(0, 0, loadedTexture.width, loadedTexture.height), new Vector2(0.5f, 0.5f));
        return (loadedSprite);
    }
    public Texture2D LoadClueTexture(int i)
    {
        string folderPath = Path.Combine(Application.persistentDataPath, saveFolder);
        string fileName = $"savedSprite_{i}.png";
        string filePath = Path.Combine(folderPath, fileName);
        byte[] pngData = File.ReadAllBytes(filePath);
        Texture2D loadedTexture = new Texture2D(1080, 1080);
        loadedTexture.LoadImage(pngData);
        return (loadedTexture);
    }

}
