using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogData", menuName = "Dialog info")]
public class DialogScriptable : ScriptableObject
{
    public List<DialogSegments> dialogSegments = new List<DialogSegments>();


    public DialogScriptable nextDialog; 


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

}
