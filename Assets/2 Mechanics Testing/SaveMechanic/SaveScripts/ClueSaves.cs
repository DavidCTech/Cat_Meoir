using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueSaves : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public GameObject loadingScreen;
    // game object photo manager 
    public PhotoManager photoManager; 

    public void SaveClues()
    {
        this.gameObject.GetComponent<ClueImagesSave>().SaveClueImages();
        SaveSystem.SaveClues(photoManager.snapshots);
    }
    public void LoadClues()
    {
        pauseMenu.Resume();
        loadingScreen.SetActive(true);
        StartCoroutine(LoadClueAsync());

    }

    // logic made with chat gpt help 
    private IEnumerator LoadClueAsync()
    {
        yield return new WaitForSeconds(0.5f);
        loadingScreen.SetActive(false);

        ClueData data = SaveSystem.LoadClues();
        // Clear the existing snapshots
        photoManager.snapshots.Clear();

        if (data != null)
        {
            // Iterate through the loaded clue names and create new PhotoScriptable objects
            for (int i = 0; i < data.clueList.Length; i++)
            {
                // Create a new PhotoScriptable object
                PhotoScriptable newPhoto = ScriptableObject.CreateInstance<PhotoScriptable>();
                newPhoto.sprite = this.gameObject.GetComponent<ClueImagesSave>().LoadClueImages(i);
                // Set the clue name
                newPhoto.clueName = data.clueList[i];

                // Set the isClue property
                newPhoto.isClue = data.isClueArray[i];

                // Add the new PhotoScriptable to the snapshots list
                photoManager.snapshots.Add(newPhoto);
            }
        }
        else
        {
            Debug.LogError("Failed to load Clue data!");
        }
    }
}
