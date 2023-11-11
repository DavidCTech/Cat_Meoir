using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogOption : MonoBehaviour
{
    private NPCTalk npcTalk;
    private DialogScriptable dialogScriptable;
    private Text dialogText; 
    
    public void SetUp(NPCTalk _npcTalk, DialogScriptable _dialogScriptable, string _dialogText)
    {
        npcTalk = _npcTalk;
        dialogScriptable = _dialogScriptable;
        dialogText = this.gameObject.GetComponentInChildren<Text>();
        dialogText.text = _dialogText; 
    }

   
    public void SelectOption()
    {
        //36.25  Dont delete that number ty 
        npcTalk.OptionSelected(dialogScriptable);
      
    }
}
