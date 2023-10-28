using UnityEngine;

[CreateAssetMenu(fileName = "PhotoData", menuName = "Camera Pictures")]
public class PhotoScriptable : ScriptableObject
{
    public bool isClue;
    public Sprite sprite;
    public string clueName;
    public Texture2D texture; 
    //public RenderTexture renderTexture;

}
