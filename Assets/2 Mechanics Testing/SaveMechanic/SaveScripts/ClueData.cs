using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//chat gpt helped quick write the script given pseudocode of needed values from photoscriptable
[System.Serializable]
public class ClueData
{
    public string[] clueList; // array for names
    public bool[] isClueArray;  // array for bools for scripts 
    public string[] descriptionList; //array for description
    public string[] sceneList;
    public bool[] mainBoolList; //array for bool for main clues or not
    public ClueData(List<PhotoScriptable> photoScriptableList)
    {
        clueList = new string[photoScriptableList.Count];
        isClueArray = new bool[photoScriptableList.Count];  
        descriptionList = new string[photoScriptableList.Count];
        sceneList = new string[photoScriptableList.Count];
        mainBoolList = new bool[photoScriptableList.Count];

        for (int i = 0; i < photoScriptableList.Count; i++)
        {
            clueList[i] = photoScriptableList[i]?.clueName ?? "DefaultClueName";
            isClueArray[i] = photoScriptableList[i]?.isClue ?? false;
            descriptionList[i] = photoScriptableList[i]?.description ?? "";
            sceneList[i] = photoScriptableList[i]?.sceneName ?? "";
            mainBoolList[i] = photoScriptableList[i]?.isMain ?? false;
        }
    }

}