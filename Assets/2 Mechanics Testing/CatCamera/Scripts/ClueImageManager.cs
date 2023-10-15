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
                failSpace.sprite = null;
                failSpace.name = null;
            }

        }

    }
}
