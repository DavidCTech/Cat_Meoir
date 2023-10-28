using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ClueImageManager : MonoBehaviour
{
    [Header("You need to put in the image references that have the clues and the fails.")]
    public List<Image> clueSpaces = new List<Image>();
    public List<Image> failSpaces = new List<Image>();

    [Header("You need to reference the parent game object of the Image UI to turn on.")]
    public GameObject imageUI;
    private PhotoManager photoManager;


    void Awake()
    {
        photoManager = this.GetComponent<PhotoManager>();
    }
    public void TurnUIOn()
    {
        imageUI.SetActive(true);
    }
    public void TurnUIOff()
    {
        imageUI.SetActive(false);
    }

    //ChatGpt helped with the logic writing in this method for the foreach loops 
    public void newImageClue(Sprite slotSprite, string slotName)
    {
        foreach (Image clueSpace in clueSpaces)
        {
            if (clueSpace.name == slotName)
            {
                // Case 1: The name matches, update the sprite

                clueSpace.sprite = slotSprite;
                return;
            }
        }

        foreach (Image clueSpace in clueSpaces)
        {
            if (clueSpace.sprite == null)
            {
                // Case 2: No match found, assign to the nearest null sprite
                clueSpace.name = slotName;
                clueSpace.sprite = slotSprite;
                return;
            }
        }
    }

    public bool newImageFail(Sprite slotSprite, string slotName)
    {
        foreach (Image failSpace in failSpaces)
        {
            if (failSpace.sprite == null)
            {
                failSpace.name = slotName;
                failSpace.sprite = slotSprite;
                return true;
            }

        }
        return false;
    }
    public void turnToNull(Image imageToNull)
    {
        foreach (Image failSpace in failSpaces)
        {
           
            if (failSpace.name == imageToNull.name)
            {
                //take the failSpace name and compare it with a bunch of photo manager scriptable list 
                // when there's a match, delete that one in the list and then reorganize the list 
                
                for (int i = 0; i < photoManager.snapshots.Count; i++)
                {
                    if(photoManager.snapshots[i] != null)
                    {
                        if (photoManager.snapshots[i].clueName == imageToNull.name)
                        {

                            photoManager.deletePicture(photoManager.snapshots[i]);
                            break; // Break out of the loop once the item is removed
                        }
                    }
                    else
                    {
                        Debug.Log("there was an issue here Screenshot this debug message ");
                        break; 
                    }
                    
                }
                failSpace.sprite = null;
                failSpace.name = null;
                break; // Break out of the outer loop since the item has been processed
            }

        }

    }
    public void NullAll()
    {
        foreach (Image failSpace in failSpaces)
        {
            failSpace.sprite = null;
            failSpace.name = null;
            

        }
        foreach (Image clueSpace in clueSpaces)
        {
            clueSpace.sprite = null;
            clueSpace.name = null;
            

        }
    }
}
