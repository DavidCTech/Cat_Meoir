using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenShotTool : MonoBehaviour
{
    // Specify the folder path where screenshots will be saved
    public string screenshotFolder = "Screenshots";

    // Specify the key to trigger the screenshot
    public KeyCode screenshotKey = KeyCode.T;

    private void Awake()
    {
        // Create the folder if it doesn't exist
        if (!Directory.Exists(screenshotFolder))
        {
            Directory.CreateDirectory(screenshotFolder);
        }
    }

    private void Update()
    {
        // Check if the specified key is pressed
        if (Input.GetKeyDown(screenshotKey))
        {
            // Capture the screenshot
            CaptureScreenshot();
        }
    }

    void CaptureScreenshot()
    {
        // Create a RenderTexture
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        Camera.main.targetTexture = rt;

        // Render the camera's view to the RenderTexture
        Camera.main.Render();

        // Create a Texture2D and read the RenderTexture content
        Texture2D screenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture.active = rt;
        screenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        Camera.main.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        // Convert the Texture2D to a byte array
        byte[] bytes = screenshotTexture.EncodeToPNG();

        // Set the screenshot file path with a timestamp
        string screenshotPath = Path.Combine(screenshotFolder, "Screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png");

        // Save the byte array to a file
        File.WriteAllBytes(screenshotPath, bytes);

        // Log the path in the console
        Debug.Log("Screenshot saved to: " + screenshotPath);
    }
}
