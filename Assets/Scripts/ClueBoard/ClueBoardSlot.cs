using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ClueBoardSlot : MonoBehaviour
{
    public string clueName; // The name of the clue associated with this slot
    public Image image; // The UI image component to display the clue image

    public void SetImage(Sprite sprite)
    {
        // Set the sprite directly to the Image component
        image.sprite = sprite;
    }
}
