using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ClueImageManager : MonoBehaviour
{
    [Header("You need to put in the image references that have the clues and the fails.")]
    //public List<GameObject> clueLists = new List<GameObject>;
    public List<GameObject> clueSpaces = new List<GameObject>();
    //public List<clueSpaces> ClueList = new List<clueSpaces>();
    //public List<List<Image>> clueSpaces = new List<Image>();
    public List<Image> failSpaces = new List<Image>();

    [Header("You need to reference the parent game object of the Image UI to turn on.")]
    public GameObject imageUI;
    private PhotoManager photoManager;
    private Color initialColor;


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
    public void newImageClue(Sprite slotSprite, string slotName, string sceneName, string description, bool isMain)
    {
        
        foreach (GameObject clueSpace in clueSpaces)
        {
            Image clueSpaceImage = clueSpace.GetComponent<Image>();
            ImageData imageData = clueSpace.GetComponent<ImageData>(); 
            
            if(imageData.sceneName == sceneName)
            {
                //get the clueSpace component of photodata
                if (clueSpaceImage.name == slotName)
                {
                    // Case 1: The name matches, update the sprite

                    clueSpaceImage.sprite = slotSprite;
                    imageData.description = description; 
                    return;
                }
            }
           
        }


        foreach (GameObject clueSpace in clueSpaces)
        {
            Image clueSpaceImage = clueSpace.GetComponent<Image>();
            ImageData imageData = clueSpace.GetComponent<ImageData>();
            if (imageData.sceneName == sceneName)
            {
                if (clueSpaceImage.sprite == null)
                {
                    // Case 2: No match found, assign to the nearest null sprite
                    clueSpaceImage.color = Color.white;
                    clueSpaceImage.name = slotName;
                    clueSpaceImage.sprite = slotSprite;
                    imageData.description = description;
                    if (isMain)
                    {
                        imageData.mainString = "Main Clue";
                    }
                    else
                    {
                        imageData.mainString = "Optional Clue";
                    }
                    return;
                }
            }
        }
    }

    public bool newImageFail(Sprite slotSprite, string slotName)
    {
        foreach (Image failSpace in failSpaces)
        {
            if (failSpace.sprite == null)
            {
                initialColor = failSpace.color; 
                failSpace.color = Color.white;
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
                if (!failSpace.name.Contains("Image"))
                {
                    //take the failSpace name and compare it with a bunch of photo manager scriptable list 
                    // when there's a match, delete that one in the list and then reorganize the list 

                    for (int i = 0; i < photoManager.snapshots.Count; i++)
                    {
                        if (photoManager.snapshots[i] != null)
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
                    if (initialColor != null)
                    {
                        failSpace.color = initialColor;
                    }

                    failSpace.name = null;
                    break; // Break out of the outer loop since the item has been processed
                }
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
        foreach (GameObject clueSpace in clueSpaces)
        {
            Image clueSpaceImage = clueSpace.GetComponent<Image>();
            clueSpaceImage.sprite = null;
            clueSpaceImage.name = null;
            

        }
    }
    public void UpdateDescription(string clueName, string description)
    {
        
        foreach (GameObject clueSpace in clueSpaces)
        {
            Image clueSpaceImage = clueSpace.GetComponent<Image>();
            ImageData imageData = clueSpace.GetComponent<ImageData>();
            
            if (clueSpaceImage.name == clueName)
            {

                imageData.description = description;
                clueSpace.GetComponent<Button>().onClick.Invoke();
             
                return;
            }
            
        }
    }
}
