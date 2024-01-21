using UnityEngine;
using UnityEngine.UI;

public class UICutSceneToggle : MonoBehaviour
{
    //chat gpt helped write the script based on pseudocode
    public RawImage rawImage;
    public RenderTexture renderTexture;
    public bool isOn; 

    public void RenderTextureAdd()
    {

        if (rawImage != null && renderTexture != null)
        {
            isOn = true; 
            rawImage.texture = renderTexture;
        }
        else
        {
            Debug.LogError("Raw Image or Render Texture is not assigned.");
        }
    }
}
