using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class DialogAction : MonoBehaviour
{
    [Header("These are Events that can call methods when the dialog comes up- can be expanded on")]
    public UnityEvent<List<string>, DialogData> clueEvent;
    public UnityEvent actionEvent;
    public List<string> clueNames;
    public DialogData dialogData; 
    // This method will be called when the button is clicked
    public void Action()
    {
        clueEvent.Invoke(clueNames, dialogData);
        actionEvent.Invoke();

    }
}