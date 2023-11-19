using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCVariableChecker : MonoBehaviour
{
    // Place on your npc
    private PhotoManager photoManager;
    [Header("Write the name of the Game Object of the clue your looking for")]
    public List<string> clueNames = new List<string>();
    private List<string> cluesFound = new List<string>();
    private bool isAllCluesFound = false;
    [Header("Get reference to Photo Manager")]
    public GameObject gameManagerObject;
    [Header("Special Dialog")]
    public DialogData dialogData;


    void Awake()
    {
        photoManager = gameManagerObject.GetComponent<PhotoManager>();
    }

    public bool CheckNPCUnlock()
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

        foreach (PhotoScriptable snapshot in photoManager.snapshots)
        {
            if (snapshot.isClue)
            {
                string snapshotClueName = snapshot.clueName;
                isAllCluesFound = false;
            }
        }

        // Check if all level designer clues are present in player's clues
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
        if (isAllCluesFound)
        {
            this.gameObject.GetComponent<NPCSpeak>().startDialogObject = dialogData;
            return true; 

        }
        else
        {
            return false; 
        }
        
    }

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

        foreach (PhotoScriptable snapshot in photoManager.snapshots)
        {
            if (snapshot.isClue)
            {
                string snapshotClueName = snapshot.clueName;
                isAllCluesFound = false;
            }
        }

        // Check if all level designer clues are present in player's clues
        isAllCluesFound = true;
        foreach (string clueName in inputClueNames)
        {
            if (!cluesFound.Contains(clueName))
            {
                isAllCluesFound = false;
                break;

                // No need to check further if one clue is missing
            }


        }
        if (isAllCluesFound)
        {
            this.gameObject.GetComponent<NPCSpeak>().startDialogObject = _dialogData;

        }
       
    }


}
