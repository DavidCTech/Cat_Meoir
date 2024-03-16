using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ClueBoardManager : MonoBehaviour
{
    public PhotoManager photoManager;

    public List<string> cluesFound = new List<string>();




    private void OnEnable()
    {
        ClueBoardCheck();
    }


    public void ClueBoardCheck()
    {

        // Populate cluesFound list
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
    }
}
