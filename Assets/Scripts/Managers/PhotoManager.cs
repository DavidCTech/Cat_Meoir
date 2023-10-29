using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

[RequireComponent(typeof(ClueImageManager))]
public class PhotoManager : MonoBehaviour
{
    //put this script on your game Manager
    [Header("This is the text for when the images are full.")]
    public GameObject imagesFullText;
    [Header("You do not need to ref anything, this is just nice info.")]
    public List<PhotoScriptable> snapshots = new List<PhotoScriptable>();


    private bool canContinue;
    private ClueImageManager clueImageManager;

    void Awake()
    {
        clueImageManager = this.GetComponent<ClueImageManager>();
    }


    public void addPictureToList(Sprite pictureSprite, bool isClue, string clueName, Texture2D texture, string description)
    {
        PhotoScriptable snapshot = ScriptableObject.CreateInstance<PhotoScriptable>();
        snapshot.sprite = pictureSprite;
        snapshot.isClue = isClue;
        snapshot.clueName = clueName;
        snapshot.texture = texture;
        snapshot.description = description; 
       // snapshot.renderTexture = renderTexture; 
        // Update in clue image manager here if needed 
        canContinue = checkClueImages(clueName, snapshot);
        if (canContinue)
        {
            snapshots.Add(snapshot);
            checkPictureClue(snapshot);

        }
    }


    public void PictureChecking()
    {
        //this method will go through all the scriptable objects list and then put them in the UI 
        for (int i = 0; i < snapshots.Count; i++)
        {
            if (snapshots[i].isClue)
            {
                //loop through the clue images UI stuff for anything with a general name and put in the snapshot[i].sprite in it 
                //and then name it to snapshot[i].clueName 
                clueImageManager.newImageClue(snapshots[i].sprite, snapshots[i].clueName);
            }
            else
            {
                //loop through the non clue images UI stuff for anything with a general name and put a snapshot[i] sprite in it 
                clueImageManager.newImageFail(snapshots[i].sprite, snapshots[i].clueName);
            }
        
        }
    }


    
    //this script will loop through all the scriptable objects in the list and look for 1. if it is a bool of clue and 2. if it has the same name as another. If so, it wll replace
    public bool checkClueImages(string clueName, PhotoScriptable snapshot)
    {
        for (int i = 0; i < snapshots.Count; i++)
        {

            PhotoScriptable photoObj = snapshots[i];
            if (photoObj.isClue)
            {
                if (photoObj.clueName == clueName)
                {
                    snapshots[i] = snapshot;

                    addPictureClue(snapshot);
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
    public void ClearList()
    {
        snapshots.Clear(); 
    }

    public void StopFullShow()
    {
        if (imagesFullText != null)
        {
            imagesFullText.SetActive(false);

        }
    }
    private void checkPictureClue(PhotoScriptable snapshot)
    {
        if (snapshot.isClue)
        {
            addPictureClue(snapshot);
        }

        else if (!snapshot.isClue)
        {
            addPictureFail(snapshot);
        }

    }
    public void addPictureClue(PhotoScriptable snapshot)
    {

        clueImageManager.newImageClue(snapshot.sprite, snapshot.clueName);
        // clueCount++;
    }
    public void addPictureFail(PhotoScriptable snapshot)
    {
        bool didImage = clueImageManager.newImageFail(snapshot.sprite, snapshot.clueName);
        //if you didnt do the image, then it must be because its full, you need to delete - should be good to have some mechanic or words to the player here
        if (!didImage)
        {
            deletePicture(snapshot);
            if (imagesFullText != null)
            {
                
                imagesFullText.SetActive(true);

            }
        }
    }
}