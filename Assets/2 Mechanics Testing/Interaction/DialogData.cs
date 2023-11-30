using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogData : MonoBehaviour
{
    //this script goes on your dialog data gameobj. 
    [Header("This is the dialog you say - it consists of string and time on the UI")]
    public List<DialogSegments> dialogSegments = new List<DialogSegments>();
    //next dialog will be like the next dialog that gets turned on when you talk again 
    
    [Header("This is for the next interaction dialog.")]
    public DialogData nextDialog;
    [Header("This bool should be checked if its a next interaction dialog.")]
    public bool isNext;
    [Header("This bool should be checked if it's a checkpoint of saving.")]
    public bool isSaved;
    [Header("This bool should be checked if the next dialog is a next segment dialog instead.")]
    public bool nextSegment; 


    [System.Serializable]
    public struct DialogSegments
    {
        [TextArea] public string dialogText;
        public float dialogDisplayTime;
        [Header("This is the audio clip you will want. Optional")]
        public AudioClip audio;
        public List<DialogChoices> dialogChoices;
        

    }
    [System.Serializable]
    public struct DialogChoices
    {
        [TextArea] public string choiceText;
       
        public DialogData followingDialog;
    }


}
