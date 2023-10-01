using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class ClueImageManager : MonoBehaviour
{
    [Header("You need to put in the image references that have the clues and the fails.")]
    public List<Image> clueSpaces = new List<Image>();
    public List<Image> failSpaces = new List<Image>();




    public bool newImageClue (Sprite slotSprite)
    {
        foreach (Image clueSpace in clueSpaces)
        {
            
            if (clueSpace.sprite == null)
            {
                clueSpace.sprite = slotSprite;
                return true; 
            }
            
        }
        return false; 
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
  

