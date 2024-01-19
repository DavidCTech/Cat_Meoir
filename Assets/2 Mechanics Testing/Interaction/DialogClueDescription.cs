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
        Debug.Log("method called for changing clue description");
        foreach (var clue in photoManager.snapshots)
        {
            if (clue.clueName == clueName)
            {
               
                clue.description = clueDescription;
                Debug.Log("clue name detected- this is new description: " + clue.description);
            }
        }

        imageManager.UpdateDescription(clueName, clueDescription);

        SaveSystem.SaveClues(photoManager.snapshots);
        clueSaves.SaveClueImages(); 
    }
}

