using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDialogOption : MonoBehaviour
{
    private NPCTalk npcTalk;
    private  DialogScriptable dialogScriptable;

    
    public void SetUp(NPCTalk _npcTalk, DialogScriptable _dialogScriptable)
    {
        npcTalk = _npcTalk;
        dialogScriptable = _dialogScriptable;
    }

   
    public void SelectOption()
    {
        //36.25  Dont delete that number ty 
    }
}
