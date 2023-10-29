using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//chat gpt helped quick write the script given pseudocode 
[System.Serializable]
public class ClueData
{
    public string[] clueList;// array for names
    public bool[] isClueArray;  // array for bools for scripts 
    public string[] descriptionList; //array for description

    public ClueData(List<PhotoScriptable> photoScriptableList)
    {
        // Initialize the arrays with the count of photoScriptableList
        clueList = new string[photoScriptableList.Count];
        isClueArray = new bool[photoScriptableList.Count];  // Corrected the type to bool
        descriptionList = new string[photoScriptableList.Count];

        for (int i = 0; i < photoScriptableList.Count; i++)
        {
            // Make sure photoScriptableList[i].clueName is not null before assigning
            clueList[i] = photoScriptableList[i]?.clueName ?? "DefaultClueName";

            // Set the isClueArray[i] based on the isClue property of PhotoScriptable
            isClueArray[i] = photoScriptableList[i]?.isClue ?? false;
            descriptionList[i] = photoScriptableList[i]?.description ?? "";
        }
    }

}