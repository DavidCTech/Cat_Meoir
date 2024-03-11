using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClueBoard : MonoBehaviour
{
    public GameObject clueBoardUI;
    public GameObject clueCamera;
    public GameObject[] clueBoardSlots; // Array of clue board slot objects
    public GameObject[] pickWhoArray; // Array of pick who objects
    public GameObject nextButton;
    public GameObject backButton;

    private Camera mainCamera;
    private bool isClueBoardActive = false;
    private bool isPickWhoActive = false;

    // Unity Events for button clicks
    public UnityEvent activatePickWhoEvent;
    public UnityEvent deactivatePickWhoEvent;

    private void Start()
    {
        mainCamera = Camera.main;
    }

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
        mainCamera.gameObject.SetActive(false);
        clueCamera.SetActive(true);
        nextButton.SetActive(true);
        backButton.SetActive(false);

        // Activate clue board slot objects
        foreach (GameObject slotObject in clueBoardSlots)
        {
            slotObject.SetActive(true);
        }

        // Deactivate pick who objects
        foreach (GameObject pickWhoObject in pickWhoArray)
        {
            pickWhoObject.SetActive(false);
        }
    }

    public void DeactivateClueBoard()
    {
        isClueBoardActive = false;

        // Deactivate clue board slot objects
        foreach (GameObject slotObject in clueBoardSlots)
        {
            slotObject.SetActive(false);
        }

        // Deactivate pick who array
        if (!isPickWhoActive)
        {
            clueCamera.SetActive(false);
            mainCamera.gameObject.SetActive(true);
        }

        // Deactivate next button
        nextButton.SetActive(false);
    }

    // Method to activate the Pick Who array and back button
    public void ActivatePickWhoArray()
    {
        isPickWhoActive = true;

        foreach (GameObject pickWhoObject in pickWhoArray)
        {
            pickWhoObject.SetActive(true);
        }

        backButton.SetActive(true);
        clueBoardUI.SetActive(false);

        // Invoke Unity Event for activating pick who
        activatePickWhoEvent.Invoke();
    }

    // Method to deactivate the Pick Who array and back button
    public void DeactivatePickWhoArray()
    {
        isPickWhoActive = false;

        foreach (GameObject pickWhoObject in pickWhoArray)
        {
            pickWhoObject.SetActive(false);
        }

        backButton.SetActive(false);
        clueBoardUI.SetActive(true);

        // Invoke Unity Event for deactivating pick who
        deactivatePickWhoEvent.Invoke();
    }
}
