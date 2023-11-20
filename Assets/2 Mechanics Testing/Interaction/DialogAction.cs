using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class DialogAction : MonoBehaviour
{
    //this script goes on your dialog data gameobj. 
    [Header("These events can take 0 or 1 argument, for doors or effects.")]
    public UnityEvent actionEvent;
    [Header("These events take 2 arguments, for clue checking dialog.")]
    public UnityEvent<List<string>, DialogData> clueEvent;
    [Header("This string is for the clue names you check for")]
    public List<string> clueNames;
    [Header("This dialogdata is the GameObj for the next dialog.")]
    public DialogData dialogData; 
    
    //this method calls clue events and any action events, it is called in the NPCSpeak Script. 
    public void Action()
    {
        clueEvent.Invoke(clueNames, dialogData);
        actionEvent.Invoke();

    }
}