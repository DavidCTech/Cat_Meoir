using UnityEngine;

public class DialogClueDescription : MonoBehaviour
{
    //chatgpt fixed syntax errors
    public string clueName;
    public string clueDescription;
    public PhotoManager photoManager;
    private ClueImagesSave clueSaves;
    private ClueImageManager imageManager; 



    public void ChangeClueDescription()
    {
        imageManager = photoManager.GetComponent<ClueImageManager>(); 
        clueSaves = photoManager.GetComponent<ClueImagesSave>(); 

        foreach (var clue in photoManager.snapshots)
        {
            if (clue.clueName == clueName)
            {
               
                clue.description = clueDescription;

            }
        }

        imageManager.UpdateDescription(clueName, clueDescription);

        SaveSystem.SaveClues(photoManager.snapshots);
        
    }
}

