using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogData : MonoBehaviour
{

    public List<DialogSegments> dialogSegments = new List<DialogSegments>();
    public DialogData nextDialog;
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
        public DialogData followingDialog;
    }


}
