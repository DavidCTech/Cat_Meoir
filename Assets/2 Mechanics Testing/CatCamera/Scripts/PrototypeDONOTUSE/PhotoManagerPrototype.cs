using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

/*
[RequireComponent(typeof(ClueImageManager))]
public class PhotoManagerPrototype : MonoBehaviour
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


    public void addPictureToList(Sprite pictureSprite, bool isClue, string clueName)
    {
        PhotoScriptable snapshot = ScriptableObject.CreateInstance<PhotoScriptable>();
        snapshot.sprite = pictureSprite;
        snapshot.isClue = isClue;
        snapshot.clueName = clueName;
        // Update in clue image manager here if needed 
        canContinue = checkClueImages(clueName, snapshot);
        if (canContinue)
        {
            snapshots.Add(snapshot);
            checkPictureClue(snapshot);

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
    //delete from list is back up method currently for an idea of making the information dynamic 





    public void deleteFromList(GameObject imagePlaceholder)
    {
        string imageName = imagePlaceholder.name;
        if (int.TryParse(imageName, out int listNum))
        {
            //setting it and passing it to another script in order to update mappings between the image and the scriptable object name 
            clueImageManager.SetSnapshotList(snapshots);
            Debug.Log("list num: " + listNum);
            clueImageManager.DeleteAtIndex(listNum);




            if (!clueImageManager.CheckListForNull())
            {
                snapshots = clueImageManager.GetSnapshotList();
            }

        }


        /*
        string imageName = imagePlaceholder.name; 
        if (int.TryParse(imageName, out int listNum))
        {
            Debug.Log("snapshot count: " + snapshots.Count);
            if (listNum >= 0 && listNum < snapshots.Count)
            {
                Debug.Log("RemoveAt "+ listNum);
                Debug.Log("CurrentList:  " );
                for (int i = 0; i < snapshots.Count; i++)
                {
                    Debug.Log("Name: " + snapshots[i].clueName);
                    
                }
                clueImageManager.turnToNull(imagePlaceholder.GetComponent<Image>());
                snapshots.RemoveAt(listNum);
                for (int i = 0; i < snapshots.Count; i++)
                {

                    if(i == listNum + 1)
                    {
                        snapshots[i].name = (i - 1).ToString();
                    }

                }
            }
            if (imagesFullText != null)
            {
                Debug.Log("set active should be true");
                imagesFullText.SetActive(false);

            }
        }
        else
        {
            Debug.Log("Image name is not parseable " + imageName);

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
            Debug.Log("The Images are all Filled up! Deleting your newest Image taken from Memory!");
            deletePicture(snapshot);
            if (imagesFullText != null)
            {

                imagesFullText.SetActive(true);

            }
        }
    }
}


*/
