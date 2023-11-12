using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogOption : MonoBehaviour
{
  
    private DialogData dialogData;
    private Text dialogText;
    private NPCSpeak npcSpeak;

    //scriptable
    /*
     private NPCTalk npcTalk;
    public void SetUp(NPCTalk _npcTalk, DialogScriptable _dialogScriptable, string _dialogText)
    {
        npcTalk = _npcTalk;
        dialogScriptable = _dialogScriptable;
        dialogText = this.gameObject.GetComponentInChildren<Text>();
        dialogText.text = _dialogText; 
    }
    public void SelectOption()
    {
        npcTalk.OptionSelected(dialogScriptable);
      
    }
    */

    public void SetUp(NPCSpeak _npcSpeak, DialogData _dialogData, string _dialogText)
    {
        npcSpeak = _npcSpeak;
        dialogData = _dialogData;
        dialogText = this.gameObject.GetComponentInChildren<Text>();
        dialogText.text = _dialogText;
    }
    public void SelectOption()
    {
        npcSpeak.OptionSelected(dialogData);

    }

}
