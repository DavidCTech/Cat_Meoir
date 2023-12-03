using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionCheck : MonoBehaviour
{
    // Place on locked Game Object!
    private PhotoManager photoManager;
    [Header("Write the name of the Game Object of the clue your looking for")]
    public List<string> clueNames = new List<string>();
    private List<string> cluesFound = new List<string>();
    private bool isAllCluesFound = false;
    [Header("Get reference to Game Manager")]
    public GameObject gameManagerObject;
    public List<GameObject> spawnObjects;
    public GameObject uiPopUp; 


    void Awake()
    {
        photoManager = gameManagerObject.GetComponent<PhotoManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CheckDoorUnlock(); 
        }
    }
    public void CheckDoorUnlock()
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
            foreach (GameObject obj in spawnObjects)
            {
                obj.SetActive(true);
            }
            this.gameObject.SetActive(false);
        }

        if (!isAllCluesFound)
        {
            uiPopUp.SetActive(true);
        }
    }


}
