using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClueEventTrigger : MonoBehaviour
{

    [Header("Write the name of the Game Object of the clue your looking for")]
    public List<string> clueNames = new List<string>();
    public List<string> cluesFound = new List<string>();
    private bool isAllCluesFound = false;
    [Header("Get reference to Game Manager")]
    public GameObject gameManagerObject;
    public UnityEvent clueRight;





    public bool Check(PhotoManager photoManager)
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
            
            clueRight.Invoke();
            return true;
        }
        else
        {
            return false; 
        }
    }
}
