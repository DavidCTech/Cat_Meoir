using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueBoard : MonoBehaviour
{
    public GameObject clueBoardUI;
    public GameObject playerCamera;
    public GameObject clueCamera;
    public GameObject[] clueBoardSlots; // Array of clue board slot objects

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

    public void ActivateClueBoard()
    {
        isClueBoardActive = true;
        clueBoardUI.SetActive(true);
        playerCamera.SetActive(false);
        clueCamera.SetActive(true);

        // Activate clue board slot objects
        foreach (GameObject slotObject in clueBoardSlots)
        {
            slotObject.SetActive(true);
        }
    }

    public void DeactivateClueBoard()
    {
        isClueBoardActive = false;
        playerCamera.SetActive(true);
        clueCamera.SetActive(false);

        // Deactivate clue board slot objects
        foreach (GameObject slotObject in clueBoardSlots)
        {
            slotObject.SetActive(false);
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
