using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVariableChecker : MonoBehaviour
{
    // Place on your npc
   
    [Header("Write the name of the Game Object of the clue your looking for")]
    public List<string> clueNames = new List<string>();
    [Header("Get reference to Photo Manager")]
    public GameObject gameManagerObject;
    [Header("Special Dialog that appears when you complete the task")]
    public DialogData dialogData;
    [HideInInspector]
    public bool isSaid = false;

    private List<string> cluesFound = new List<string>();
    private bool isAllCluesFound;
    private PhotoManager photoManager;
    private NPCSpeak npcSpeak; 

    void Awake()
    {
        if(gameManagerObject != null)
        {
            photoManager = gameManagerObject.GetComponent<PhotoManager>();
        }
        
        npcSpeak = this.gameObject.GetComponent<NPCSpeak>();
    }

    public bool CheckNPCUnlock()
    {
        //look through the photos for things that have a clue name related to 
        // what you are looking for - add those clues to the clues found here
       
        foreach (PhotoScriptable snapshot in photoManager.snapshots)
        {
            if (snapshot.isClue)
            {
                if (!cluesFound.Contains(snapshot.clueName))
                {
                    cluesFound.Add(snapshot.clueName);
                }
            }
        }

        // loop through the clue names found
        // and check if the ones require have it 
        isAllCluesFound = true;
        foreach (string clueName in clueNames)
        {
            if (!cluesFound.Contains(clueName))
            {
                isAllCluesFound = false;
                break;

                // No need to check further if one clue is missing
            }
        }

        // clues were found- npc speak start dialog is set to the clue solve information 
        if (isAllCluesFound)
        {
            isSaid = true; 
            npcSpeak.startDialogObject = dialogData;
            return true; 
        }

        else
        {
            return false; 
        }
        
    }

    //this is a public method one can use in actions-
    //it does the same as the one above but takes in arguments 
    public void UnlockDialog(List<string> inputClueNames, DialogData _dialogData)
    {
        foreach (PhotoScriptable snapshot in photoManager.snapshots)
        {
            if (snapshot.isClue)
            {
                if (!cluesFound.Contains(snapshot.clueName))
                {
                    cluesFound.Add(snapshot.clueName);
                }
            }
        }

        isAllCluesFound = true;
        foreach (string clueName in inputClueNames)
        {
            if (!cluesFound.Contains(clueName))
            {
                isAllCluesFound = false;
                break;
            }

        }

        if (isAllCluesFound)
        {
            npcSpeak.startDialogObject = _dialogData;
        }
    }
}
