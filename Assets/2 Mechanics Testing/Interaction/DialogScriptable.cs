using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "DialogData", menuName = "Dialog info")]

public class DialogScriptable : ScriptableObject
{
   // public bool saving;// this bool will be used to call the method for saving and loading after each dialog change to make sure memory is retained. 
    public List<DialogSegments> dialogSegments = new List<DialogSegments>();
    public DialogScriptable nextDialog;
    public bool isSaved; 
     


    [System.Serializable]
    public struct DialogSegments
    {
        public string dialogText; 
        public float dialogDisplayTime;
        public List<DialogChoices> dialogChoices; 

    }

    [System.Serializable]
    public struct DialogChoices
    {
        public string choiceText; 
        public DialogScriptable followingDialog;
    }

    // Default constructor
    public DialogScriptable()
    {
        string dialogText = "";
        float dialogDisplayTime = 0 ;
        List<DialogChoices> dialogChoices;
    }


    // Save the DialogScriptable to a file using JSON
    public void SaveToFile(string filePath)
    {
        string jsonData = JsonUtility.ToJson(this, true);
        File.WriteAllText(filePath, jsonData);
    }

   
    

}
