using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

//made with help from chat gpt 
public class ClueImagesSave : MonoBehaviour
{
    public PhotoManager photoManager;
    private string saveFolder = "CatMeoirSavedImages"; // Folder name where the images will be saved

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
            // Get the sprite
            Sprite snapshotSprite = photoManager.snapshots[i].sprite;

            // Convert Sprite to Texture2D
            Texture2D texture = snapshotSprite.texture;

            // Encode Texture2D to PNG byte array
            byte[] pngData = texture.EncodeToPNG();

            // Construct a unique file name based on some criteria (e.g., index, timestamp, etc.)
            string fileName = $"savedSprite_{i}.png";

            // Construct the full file path
            string filePath = Path.Combine(folderPath, fileName);

            // Save PNG data to file
            File.WriteAllBytes(filePath, pngData);
        }
    }

    public Sprite LoadClueImages(int i)
    {
        string folderPath = Path.Combine(Application.persistentDataPath, saveFolder);
        string fileName = $"savedSprite_{i}.png";
        string filePath = Path.Combine(folderPath, fileName);
        byte[] pngData = File.ReadAllBytes(filePath);
        Texture2D loadedTexture = new Texture2D(1080, 1080);  // Provide dimensions as needed
        loadedTexture.LoadImage(pngData);
        Sprite loadedSprite = Sprite.Create(loadedTexture, new Rect(0, 0, loadedTexture.width, loadedTexture.height), new Vector2(0.5f, 0.5f));
        return (loadedSprite);
    }

}
