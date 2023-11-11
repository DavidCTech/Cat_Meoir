using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTalk : MonoBehaviour
{
    //used tutorial from YT Jared Brandjes 
    public DialogScriptable dialogScriptable;
    public Canvas dialogCanvas;
    public Text dialogText;
    public Transform dialogOptionsParent;
    public GameObject dialogOptionsPrefab;
    public GameObject dialogOptionsContainer;
    private bool optionSelected = false; 

    public void Interact()
    {
        StartCoroutine(displayDialog());
    }

    public void OptionSelected(DialogScriptable selectedOption)
    {
        optionSelected = true; 
    }
    IEnumerator displayDialog()
    {
        dialogCanvas.enabled = true;
        foreach(var dialog in dialogScriptable.dialogSegments)
        {

            if(dialog.dialogChoices.Count == 0 )
            {
                dialogText.text = dialog.dialogText;
                yield return new WaitForSeconds(dialog.dialogDisplayTime);
            }
            else
            {
                dialogOptionsContainer.SetActive(true);
                //open options panel 
                foreach(var option in dialog.dialogChoices)
                {
                    GameObject newButton = Instantiate (dialogOptionsPrefab, dialogOptionsParent);
                    newButton.transform.GetChild(0).GetComponent<Text>().text = option.choiceText; 
                }
                while (!optionSelected)
                {
                    yield return null; 
                }
                
            }
            
        }
        dialogOptionsContainer.SetActive(false);
        dialogCanvas.enabled = false;
    }
  
}
