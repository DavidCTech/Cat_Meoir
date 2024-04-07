using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClueSaves : MonoBehaviour
{
    // game object photo manager 
    private PhotoManager photoManager;
    public GameObject loadingScreen;
    [Header("Put in events you want after load.")]
    public UnityEvent afterLoadEvent;

    private bool isLoading= false; 

    private void Start()
    {
        photoManager = this.gameObject.GetComponent<PhotoManager>();
    }
    public void SaveClues()
    {
        if (!isLoading)
        {
            photoManager = this.gameObject.GetComponent<PhotoManager>();
            this.gameObject.GetComponent<ClueImagesSave>().SaveClueImages();
            SaveSystem.SaveClues(photoManager.snapshots);
            Debug.Log("saved Clues");
        }
    }
    public void loadEvents()
    {
        if (!isLoading)
        {
            afterLoadEvent.Invoke();
        }
    }
    public void DeleteClues()
    {
        SaveSystem.DeleteClues();
        SaveSystem.DeleteClueImages(); 
    }
    public void LoadClues()
    {
        isLoading = true; 
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }
        StartCoroutine(LoadClueAsync());

    }

    // logic made with chat gpt help 
    private IEnumerator LoadClueAsync()
    {
        yield return new WaitForSeconds(0.2f);

        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }
        ClueData data = SaveSystem.LoadClues();
        // Clear the existing snapshots
        photoManager.snapshots.Clear();
        if (data == null)
        {
            Debug.Log("clue data null");
        }

        if (data != null)
        {
            // Iterate through the loaded clue names and create new PhotoScriptable objects
            for (int i = 0; i < data.clueList.Length; i++)
            {
                // Create a new PhotoScriptable object
                PhotoScriptable newPhoto = ScriptableObject.CreateInstance<PhotoScriptable>();
                newPhoto.sprite = this.gameObject.GetComponent<ClueImagesSave>().LoadClueImages(i);
                newPhoto.texture = this.gameObject.GetComponent<ClueImagesSave>().LoadClueTexture(i);
                // Set the clue name
                newPhoto.clueName = data.clueList[i];

                // Set the isClue property
                newPhoto.isClue = data.isClueArray[i];

                //set the description property 
                newPhoto.description = data.descriptionList[i];

                //set the location property 
                newPhoto.sceneName = data.sceneList[i];

                //set main bool property 
                newPhoto.isMain = data.mainBoolList[i];

                // Add the new PhotoScriptable to the snapshots list
                photoManager.snapshots.Add(newPhoto);
            }
            photoManager.PictureChecking();
        }
       
        else
        {
            // put the failed to load gemaobject UI On 
            
            
        }

        isLoading = false;
        
        SaveClues();
        loadEvents();

    }
}
