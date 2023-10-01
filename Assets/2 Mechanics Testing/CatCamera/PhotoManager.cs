using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

[RequireComponent(typeof(ClueImageManager))]
public class PhotoManager : MonoBehaviour
{
    //put this script on your game Manager
    [Header("You do not need to ref anything, this is just nice info.")]
    public List<PhotoScriptable> snapshots = new List<PhotoScriptable>();


    private int clueCount = 0;
    /*
    private bool isClue;
    private string clueName;
    private Sprite pictureSprite;
    */
    private bool canContinue; 
    private ClueImageManager clueImageManager;

    void Awake()
    {
        clueImageManager = this.GetComponent<ClueImageManager>();
    }
    

    public void addPictureToList(Sprite pictureSprite, bool isClue, string clueName)
    {
        PhotoScriptable snapshot = ScriptableObject.CreateInstance<PhotoScriptable>();
        snapshot.sprite = pictureSprite;
        snapshot.isClue = isClue;
        snapshot.clueName = clueName;
        Debug.Log("snapshot info- sprite: " + snapshot.sprite + " isclue: " + snapshot.isClue + "clue name: " + snapshot.clueName);
        // Update in clue image manager here if needed
        canContinue = checkClueImages(clueName, snapshot);
        if (canContinue)
        {
            snapshots.Add(snapshot);
            checkPictureClue(snapshot);

        }
        else
        {
            //this is where we can tell it to go look through the clue manager stuff for a duplicate ? 

        }
        
       

    }
    //this script will loop through all the scriptable objects in the list and look for 1. if it is a bool of clue and 2. if it has the same name as another. If so, it wll replace
    public bool checkClueImages(string clueName, PhotoScriptable snapshot)
    {
        for (int i = 0; i < snapshots.Count; i++)
        {
            
            PhotoScriptable photoObj = snapshots[i];
            if(photoObj.isClue)
            {
                if (photoObj.clueName == clueName)
                {
                    snapshots[i] = snapshot;
                    Debug.Log("this Happened, the clue name:" + snapshots[i].clueName + " and then the other clue name: " + snapshot.clueName);
                    // Update in clue image manager here if needed

                    return false;
                }
            }
            
        }
        return true;

    }
    
    public void deletePicture(PhotoScriptable snapshot)
    {
        snapshots.Remove(snapshot);
    }
    private void checkPictureClue(PhotoScriptable snapshot)
    {
        if (snapshot.isClue)
        {
            addPictureClue(snapshot);
        }

        else if (!snapshot.isClue)
        {
            addPictureFail();
        }
       
    }
    public void addPictureClue(PhotoScriptable snapshot)
    {

        clueImageManager.newImageClue(snapshot.sprite, snapshot.clueName);
       // clueCount++;
    }
    public void addPictureFail()
    {
        bool didImage = clueImageManager.newImageFail(snapshots[clueCount].sprite);
        clueCount++;
    }
}
