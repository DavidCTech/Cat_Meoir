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
    [Header("You need to reference a defualt image for the clues.")]
    public Sprite defaultSprite;


    private Dictionary<int, int> indexMapping = new Dictionary<int, int>();
    private List<PhotoScriptable> snapshotsPass; 


    public void SetSnapshotList(List<PhotoScriptable> snapshotsList)
    {
        snapshotsPass = snapshotsList;
    }
    public List<PhotoScriptable> GetSnapshotList()
    {
        return snapshotsPass;
    }

    //deletes both the info from scriptable object and in the image list dictionary made with chatGPT help
    public void DeleteAtIndex(int index)
    {
        if (index >= 0 && index < snapshotsPass.Count)
        {
        
            snapshotsPass.RemoveAt(index);

            UpdateMapping(index);

            UpdateImageNames();
        }
    }

    private void UpdateMapping(int removedIndex)
    {
        
        indexMapping.Clear();
        Debug.Log("snapshotspass count " + snapshotsPass.Count);
        for (int i = 0; i < snapshotsPass.Count; i++)
        {
            indexMapping[i] = i;
        }
       
        indexMapping.Remove(removedIndex);
        Debug.Log("snapshotspass count after removal: " + snapshotsPass.Count);
    }

    private void UpdateImageNames()
    {
        Debug.Log("Failspaces count: "+ failSpaces.Count);
        for (int i = 0; i < failSpaces.Count; i++)
        {
            if (indexMapping.TryGetValue(i, out int newIndex))
            {
                Debug.Log("try get value: " + i);
                Debug.Log("new Index" + newIndex);

                Image image = failSpaces[i];
                image.name = newIndex.ToString();

            }
        }
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

    /*
    public void fixFailList()
    {
        for(int i = 0; i < failSpaces.Count; i++)
        {
            if (failSpaces[1].name) ;
        }
    }
    */


    public void turnToNull(Image imageToNull)
    {
        imageToNull.sprite = defaultSprite;
        imageToNull.name = null;
        /*
        foreach (Image failSpace in failSpaces)
        {
            if (failSpace.name == imageToNull.name)
            {
                failSpace.sprite = defaultSprite;
                failSpace.name = "Default"; 
            }

        }
        */
        
    }
}
  

