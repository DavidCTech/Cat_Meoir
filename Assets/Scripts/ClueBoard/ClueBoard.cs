using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBoard : MonoBehaviour
{
    public GameObject clueBoardUI;
    public GameObject playerCamera;
    public GameObject clueCamera; // Reference to the clue camera
    public GameObject mainCluesDisplay; // Reference to the spots where main clues photos will be displayed

    private bool isClueBoardActive = false;

    private void Update()
    {
        // Check for player interaction with the clue board
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
        clueCamera.SetActive(true); // Enable the clue camera
    }

    private void DeactivateClueBoard()
    {
        isClueBoardActive = false;
        playerCamera.SetActive(true);
        clueCamera.SetActive(false); // Disable the clue camera
    }

    public void DisplayMainCluePhoto(Sprite mainCluePhoto)
    {
        // This method should be called when a main clue photo is collected
        // Add logic to display the photo on the clue board UI at designated spots
    }
}
