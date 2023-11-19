using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSpeak : MonoBehaviour
{
    //used tutorial from YT Jared Brandjes 
    //UI Junk
    public string npcName; 
    public GameObject dialogCanvas;
    public Text dialogText;
    public Transform dialogOptionsParent;
    public GameObject dialogOptionsPrefab;
    public GameObject dialogOptionsContainer;
    public GameObject dialogParent; 

    //this is the dialog Data
    public DialogData startDialogObject;

    private bool npcDataLoaded = false;
    private bool optionSelected = false;
    private bool finishedDialog = false;

    private List<GameObject> dialogChildren = new List<GameObject>();


    //this is the general interact method that will be brought up, currently it will always loop through the dialog beginning at start object. 
    public void Interact()
    {
        //finishedDialog = false;
        NPCVariableChecker npcChecker = this.gameObject.GetComponent<NPCVariableChecker>();
        if(npcChecker != null)
        {
            if (this.gameObject.GetComponent<NPCVariableChecker>().CheckNPCUnlock())
            {
                Debug.Log("---");
                NextDialogCheck(startDialogObject);
               

            }
            else
            {
                StartCoroutine(LoadAndProceed());
            }
        }
        else
        {
            StartCoroutine(LoadAndProceed());
        }

    }

    public void Interact(DialogData selectedOption)
    {
        finishedDialog = false;
        NextDialogCheck(selectedOption);
    }

    private IEnumerator LoadAndProceed()
    {
        yield return StartCoroutine(LoadNPCAsync(npcName));

        // Wait until NPC data is loaded
        while (!npcDataLoaded)
        {
            yield return null;
        }

        NextDialogCheck(startDialogObject);
    }


    public void NextDialogCheck(DialogData selectedOption)
    {
        if (selectedOption.nextDialog == null)
        {
            StartCoroutine(displayDialog(selectedOption));
            Debug.Log("this");
        }

        //if there is a next dialog
        else
        {
            if (finishedDialog)
            {
                Debug.Log("Intect again?" );
                Interact(startDialogObject);
            }
            else
            {
                startDialogObject = selectedOption.nextDialog;
                Debug.Log("next dialog option " + startDialogObject);
                Debug.Log("Current dialog Option:  " + selectedOption);
                StartCoroutine(displayDialog(selectedOption));
            }
            
            
            
           
            
           
        }
    }

    public void OptionSelected(DialogData selectedOption)
    {
        optionSelected = true;
        Interact(selectedOption);


    }
    public void Save(DialogData selectedOption)
    {
        dialogChildren.Clear();
        if (dialogParent != null)
        {
            foreach (Transform child in dialogParent.transform)
            {
                DialogData dialogDataComponent = child.GetComponent<DialogData>();

                if (dialogDataComponent != null)
                {
                    dialogChildren.Add(child.gameObject);
                }
            }
        }
        SaveSystem.SaveNPC(npcName, dialogChildren, selectedOption);
    }


    
    private IEnumerator LoadNPCAsync(string npcName)
    {
        yield return new WaitForSeconds(0.5f);
        NPCData data = SaveSystem.LoadNPC(npcName);

        if (data != null)
        {
            //clear the children list
            dialogChildren.Clear();
            // get a list of the children of the dialog object again 
            if (dialogParent != null)
            {
                foreach (Transform child in dialogParent.transform)
                {
                    DialogData dialogDataComponent = child.GetComponent<DialogData>();

                    if (dialogDataComponent != null)
                    {
                        dialogChildren.Add(child.gameObject);
                    }
                }
            }

            //iterate through the bool list from the data 
            for (int i = 0; i < data.dialogCheck.Length; i++)
            {
                //check if it is true 
                if (data.dialogCheck[i])
                {
                    //if it is true, set that location of the dialog children list to the start object 
                    DialogData dialogData = dialogChildren[i].GetComponent<DialogData>();
                    startDialogObject = dialogData;

                }
               
            }
            npcDataLoaded = true;
        }
        
        else
        {
            npcDataLoaded = true;
        }
    }



    IEnumerator displayDialog(DialogData selectedOption)
    {
        yield return null;
        if(selectedOption.isSaved)
        {
            Save(selectedOption);
        }


        GameObject optionObj = selectedOption.gameObject;
        DialogAction dialogAction = optionObj.GetComponent<DialogAction>(); 
        if(dialogAction != null)
        {
            dialogAction.Action(); 
        }

        //finishedDialog = false;
        Debug.Log("Last option: " + selectedOption);
        //object pooling 
        List<GameObject> spawnedButtons = new List<GameObject>();
        dialogCanvas.SetActive(true);
        foreach (var dialog in selectedOption.dialogSegments)
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
        Debug.Log("finished dialog true "+ selectedOption);
        finishedDialog = true;
        foreach (GameObject button in spawnedButtons)
        {
            Destroy(button);
        }
       
        /*
        if (selectedOption.nextDialog != null)
        {
            NextDialogCheck(selectedOption);
        }
        */

    }

}
