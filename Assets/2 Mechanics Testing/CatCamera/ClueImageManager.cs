using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ClueImageManager : MonoBehaviour
{
    [Header("You need to put in the image references that have the clues and the fails.")]
    public List<Image> clueSpaces = new List<Image>();
    public List<Image> failSpaces = new List<Image>();



    //ChatGpt helped with the logic writing in this method for the foreach loops 
    public void newImageClue(Sprite slotSprite, string slotName)
    {
        foreach (Image clueSpace in clueSpaces)
        {
            if (clueSpace.name == slotName)
            {
                // Case 1: The name matches, update the sprite
                Debug.Log("EEEE");
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

    public bool newImageFail(Sprite slotSprite)
    {
        foreach (Image failSpace in failSpaces)
        {
            if (failSpace.sprite == null)
            {
                failSpace.sprite = slotSprite;
                return true;
            }

        }
        return false;
    }
}
  

