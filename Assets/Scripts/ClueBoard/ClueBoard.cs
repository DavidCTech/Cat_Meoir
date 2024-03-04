using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueBoard : MonoBehaviour
{
    public GameObject clueBoardUI;
    public GameObject playerCamera;
    public PlayerMovement playerMovementScript; // Reference to the player's movement script
    public PlayerManager playerManagerScript; // Reference to the player manager script
    public GameObject clueCamera;
    public GameObject[] clueBoardSlots; // Reference to the slots on the clue board UI

    private bool isClueBoardActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isClueBoardActive)
                ActivateClueBoard();
            else
                DeactivateClueBoard();
        }
    }

    private void ActivateClueBoard()
    {
        isClueBoardActive = true;
        clueBoardUI.SetActive(true);
        playerCamera.SetActive(false);
        clueCamera.SetActive(true);

        // Display images on the clue board slots
        DisplayImagesOnSlots();
    }

    private void DeactivateClueBoard()
    {
        isClueBoardActive = false;
        playerCamera.SetActive(true);
        clueCamera.SetActive(false);

        // Clear images from the clue board slots
        ClearSlots();
    }

    // Method to display images on the clue board slots
    private void DisplayImagesOnSlots()
    {
        for (int i = 0; i < clueBoardSlots.Length; i++)
        {
            // Assuming you have a method to get the image to display on the slot
            Sprite image = GetImageForSlot(i);
            if (image != null)
            {
                Image slotImage = clueBoardSlots[i].GetComponent<Image>();
                if (slotImage != null)
                {
                    slotImage.sprite = image;
                }
            }
        }
    }

    // Method to clear images from the clue board slots
    private void ClearSlots()
    {
        for (int i = 0; i < clueBoardSlots.Length; i++)
        {
            Image slotImage = clueBoardSlots[i].GetComponent<Image>();
            if (slotImage != null)
            {
                slotImage.sprite = null;
            }
        }
    }

    // Method to get the image to display on a slot (replace with your logic)
    private Sprite GetImageForSlot(int slotIndex)
    {
        // Return the image sprite for the specified slot index
        // Replace this with your logic to determine which image to display on each slot
        return null;
    }
}
