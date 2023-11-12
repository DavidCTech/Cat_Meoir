using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTalk : MonoBehaviour
{
    //used tutorial from YT Jared Brandjes 
   
    public GameObject dialogCanvas;
    public Text dialogText;
    public Transform dialogOptionsParent;
    public GameObject dialogOptionsPrefab;
    public GameObject dialogOptionsContainer;
    public DialogScriptable startDialogObject;

    public List<DialogScriptable> allDialog; 

    //for saving
    private NPCSaves npcSaves; 

    private bool optionSelected = false;
    private bool finishedDialog = false; 

    private void Start()
    {
        npcSaves = this.gameObject.GetComponent<NPCSaves>(); 
    }
    //this is the general interact method that will be brought up, currently it will always loop through the dialog beginning at start object. 
    /*public void Interact()
    {
        NextDialogCheck(startDialogObject);
    }
    */

    //overloaded method takes in the selected option could be good for manually putting in specific objects 
    //will want to 1. check for saved dialog in future 
    // 2. check a bool for saving 
    //3. bring up that information and loop through the scriptable objects for the right reference and pass it into the interact before this script is called
    // specifically, in the player interact section - 

    /*
    public void Interact(DialogScriptable selectedOption)
    {
        NextDialogCheck(selectedOption);
    }

    public void NextDialogCheck(DialogScriptable selectedOption)
    {
        if (selectedOption.nextDialog == null)
        {
            Debug.Log("next null");
            StartCoroutine(displayDialog(selectedOption));
        }

        //if there is a next dialog
        else
        {
            Debug.Log("Next Not Null");
            //if you havent said your stuff yet: 
            if (!finishedDialog)
            {
                Debug.Log("Not finished dialog");
                StartCoroutine(displayDialog(selectedOption));
            }
            else
            {
                Debug.Log("Finished dialog");
                StartCoroutine(displayDialog(selectedOption.nextDialog));
            }

        }
    }

    public void OptionSelected(DialogScriptable selectedOption)
    {
        Debug.Log("OptionSelected");
        optionSelected = true;
        Interact(selectedOption);

       
    }
    IEnumerator displayDialog(DialogScriptable selectedOption)
    {
        yield return null;
        //saving
        if (selectedOption.isSaved)
        {
            npcSaves.SaveNPC(selectedOption);
        }
        
        finishedDialog = false;
        //object pooling 
        List<GameObject> spawnedButtons = new List<GameObject>(); 
        dialogCanvas.SetActive(true);
        foreach(var dialog in selectedOption.dialogSegments)
        { 
            if (dialog.dialogChoices.Count == 0)
            {
                dialogText.text = dialog.dialogText;
                yield return new WaitForSeconds(dialog.dialogDisplayTime);
            }
            else
            {
                dialogOptionsContainer.SetActive(true);
                //open options panel 
                foreach (var option in dialog.dialogChoices)
                {
                    dialogText.text = dialog.dialogText;
                    GameObject newButton = Instantiate(dialogOptionsPrefab, dialogOptionsParent);
                    spawnedButtons.Add(newButton);
                    newButton.GetComponent<UIDialogOption>().SetUp(this, option.followingDialog, option.choiceText);
                }
                while (!optionSelected)
                {
                    yield return null;
                }
                break;
            }
        }
        dialogOptionsContainer.SetActive(false);
        dialogCanvas.SetActive(false);
        optionSelected = false; 
        finishedDialog = true; 
        foreach (GameObject button in spawnedButtons)
        {
            Destroy(button);
        }
        if (selectedOption.nextDialog != null)
        {
            NextDialogCheck(selectedOption);
        }

    }
    */
}
