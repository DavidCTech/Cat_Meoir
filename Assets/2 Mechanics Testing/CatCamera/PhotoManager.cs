using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ClueImageManager))]
public class PhotoManager : MonoBehaviour
{
    [Header("You do not need to ref anything, this is just nice info.")]
    public List<PhotoScriptable> snapshots = new List<PhotoScriptable>();


    private int clueCount = 0;
    private bool isClue;
    private string clueName;
    private Sprite pictureSprite;
    private ClueImageManager clueImageManager;

    void Awake()
    {
        clueImageManager = this.GetComponent<ClueImageManager>();
    }
    
    public void addPictureClue()
    {

        bool didImage = clueImageManager.newImageClue(snapshots[clueCount].sprite);
        clueCount++;
    }
    public void addPictureFail()
    {
        bool didImage = clueImageManager.newImageFail(snapshots[clueCount].sprite);
        clueCount++;
    }
    public void addPictureToList(Sprite pictureSprite, bool isClue, string clueName)
    {
        PhotoScriptable snapshot = ScriptableObject.CreateInstance<PhotoScriptable>();
        snapshot.sprite = pictureSprite;
        snapshot.isClue = isClue;
        snapshot.clueName = clueName;
        for (int i = 0; i < snapshots.Count; i++)
        {
            PhotoScriptable photoObj = snapshots[i];

            if (photoObj.clueName == clueName)
            {
                snapshots[i] = snapshot;
                // Update in clue image manager here if needed
                return;
            }
        }

        snapshots.Add(snapshot);
        checkPictureClue(snapshot);
        return; 

    }
    
    public void deletePicture(PhotoScriptable snapshot)
    {
        snapshots.Remove(snapshot);
    }
    private void checkPictureClue(PhotoScriptable snapshot)
    {
        if (snapshot.isClue)
        {
            addPictureClue();
        }

        else if (!snapshot.isClue)
        {
            addPictureFail();
        }
       
    }
}
