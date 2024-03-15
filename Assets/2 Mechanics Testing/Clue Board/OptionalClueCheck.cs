using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class OptionalClueCheck : MonoBehaviour
{
    public string clueName;

    public TMP_Text clueNameText;
    public TMP_Text clueDescriptionText;

    public ClueBoardManager clueBoardManager;
    public bool noMatchFound = true;

    void Start()
    {
        // Find ClueBoardManager reference once at the start
        clueBoardManager = FindObjectOfType<ClueBoardManager>();

        // Call InformationCheck when the script starts
        InformationCheck();
    }

    public void InformationCheck()
    {
        // Reset noMatchFound to true at the start of each check
        bool noMatchFound = true;

        Debug.Log("Clues Found: " + string.Join(", ", clueBoardManager.cluesFound));

        foreach (string foundClue in clueBoardManager.cluesFound)
        {
            Debug.Log("Found Clue: " + foundClue);
            Debug.Log("Clue Name: " + clueName);

            if (foundClue == clueName)
            {
                // If a match is found, set the flag to false and exit the loop
                noMatchFound = false;
                Debug.Log("False ");
                break;
            }
        }

        // If no match is found, set the text
        if (noMatchFound)
        {
            clueNameText.text = "Bro";
            clueDescriptionText.text = "???";
        }
    }
}
