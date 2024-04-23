using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogOption : MonoBehaviour
{

    //this goes on the dialogbutton prefab 
    private DialogData dialogData;
    private Text dialogText;
    private NPCSpeak npcSpeak;

    //set up creates info for the button  - this is for a button with no other choices 
    public void SetUp(NPCSpeak _npcSpeak, DialogData _dialogData, string _dialogText)
    {
        npcSpeak = _npcSpeak;
        dialogData = _dialogData;
        dialogText = this.gameObject.GetComponentInChildren<Text>();

        if (PlayerPrefs.HasKey("ArialDialogueState"))
        {
            if (PlayerPrefs.GetInt("ArialDialogueState") == 1)
            {
                if (ArialJournalManager.instance != null)
                {
                    dialogText.text = _dialogText;
                    dialogText.font = ArialJournalManager.instance.arialFont;
                }
            }
            else
            {
                dialogText.text = _dialogText;
                dialogText.font = ArialJournalManager.instance.typeWriterFont;
                Debug.Log("Making Text Typwriter");
            }
        }

        // Get the Button component
        Button button = this.gameObject.GetComponent<Button>();


    }



    //on select option it will do option selected in the npcspeak - which changes the dialog
    public void SelectOption()
    {
        npcSpeak.OptionSelected(dialogData);

    }

    public void SwapText()
    {
        if (PlayerPrefs.GetInt("ArialDialogueState") == 1)
        {
            dialogText.font = ArialJournalManager.instance.arialFont;
        }
        else
        {
            dialogText.font = ArialJournalManager.instance.typeWriterFont;
        }
    }

}
