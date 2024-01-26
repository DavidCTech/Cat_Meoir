using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events; 

public class NPCSpeak : MonoBehaviour
{
    //modeled from the base in YT Jared Brandjes's tutorial about scriptable object dialog system 

    [Header("This name is used for loading save data. It should be Unique.")]
    public string npcName;
    [Header("These are references to the canvas and UI for Interaction.")]
    public GameObject dialogCanvas;
    public Text dialogText;
    public Transform dialogOptionsParent;
    public GameObject dialogOptionsPrefab;
    public GameObject dialogOptionsContainer;
    [Header("Parent of your dialog GameObjects- used for saving and loading.")]
    public GameObject dialogParent;

    //this is the dialog Data
    [Header("This is your starting dialog. It changes in play.")]
    public DialogData startDialogObject;

    // load and option selected bools for controling UI and Saving 
    private bool npcDataLoaded = false;
    private bool optionSelected = false;

    //dialog children is just a list of the children to loop through and save. 
    private List<GameObject> dialogChildren = new List<GameObject>();
    private NPCVariableChecker npcChecker;
    private AudioSource audioSource;
    //this is for click skipping 
    private bool currentlySpeaking = false;
    private float skipTime; 
    //this is for hold down skipping
    public bool megaSkip; 


    //delegate for the clicking skip
    void OnEnable()
    {
        InteractionSkip.onSkipDel += SkipDialog;
        InteractionSkip.onMegaSkip += MegaSkip;
    }

    void OnDisable()
    {
        InteractionSkip.onSkipDel -= SkipDialog;
        InteractionSkip.onMegaSkip -= MegaSkip;
    }

    void MegaSkip()
    {
        megaSkip = true;
        skipTime = 0f;
    }

    void SkipDialog()
    {
        if(currentlySpeaking == true)
        {
            skipTime = 0f; 
        }
    }
    private void Start()
    {

        // first it will get the npcVariable checker to see if you did the right combination first. 
        npcChecker = this.gameObject.GetComponent<NPCVariableChecker>();
        audioSource = this.gameObject.GetComponent<AudioSource>();


    }

    //this is the general interact method that will be brought up,
    //currently it will always loop through the dialog beginning at start object. 
    public void Interact()
    {
        megaSkip = false;  
        //freeze the player 

        // if the selected object about to be diaplayed is noted to be saved
        //it saves
        if (startDialogObject.isSaved)
        {
            Save(startDialogObject);
        }
        //this is for the npc checker - it will see if you took pictures 
        // of the needed clues or not for the original clue checker.
        if (npcChecker != null)
        {
            // is said is a bool in npc checker 
            // we need to check if it was said or not before it runs checknpcunlock 
            // else it will keep looping 
            if (!npcChecker.isSaid)
            {
                //checknpcunlock is a method that returns a bool 
                //it returns if it actually has clues working or not 
                //it then starts at dialog object which got changed in the method 
                if (this.gameObject.GetComponent<NPCVariableChecker>().CheckNPCUnlock())
                {
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
        else
        {
            StartCoroutine(LoadAndProceed());
        }
        
    }


    //this coroutine is meant to run the loading npc function and 
    // will wait for it to finish. 
    // this is to ensure no race consitions 
    private IEnumerator LoadAndProceed()
    {
        yield return StartCoroutine(LoadNPCAsync(npcName));

        // Wait until NPC data is loaded
        while (!npcDataLoaded)
        {
            yield return null;
        }
        // when it is loaded - now one can put in the starting dialog object 
        // that got loaded into the next dialog check 
        NextDialogCheck(startDialogObject);
    }

    //this coroutine handles loading - loading is in this script as it is 
    //name dependent on npc- same with saving 
    private IEnumerator LoadNPCAsync(string npcName)
    {
        yield return new WaitForSeconds(0.5f);
        //this data is mainly a bool system for what is loaded and what isn't 
        NPCData data = SaveSystem.LoadNPC(npcName);

        if (data != null)
        {
            // dialog children is the list of objects we loop through to figure out which to load. 
            //It is cleared then remade based on what has dialogdata component. 
            dialogChildren.Clear();
            
            // Call the function with the parent transform
            if (dialogParent != null)
            {
                TraverseChildren(dialogParent.transform);
            }
            //iterate through the bool list from the NPCData 
            for (int i = 0; i < data.dialogCheck.Length; i++)
            {
               
                if (data.dialogCheck[i])
                {
                    //if it is true, set that location of the dialog children list to the start object
                    DialogData dialogData = dialogChildren[i].GetComponent<DialogData>();
                    startDialogObject = dialogData;

                }

            }
            //this is for dialog actions- specific to the dialog object it is currently on. 
            // this takes priority over everything 
            DialogAction dialogAction = startDialogObject.gameObject.GetComponent<DialogAction>();

            if (dialogAction != null)
            {
                dialogAction.Action();
            }
            //this true bool ends the load and progress coroutine while loop
            npcDataLoaded = true;
        }

        else
        {
            //this true bool ends the load and progress coroutine while loop
            npcDataLoaded = true;
        }
       

    }

    //traverse children loops through the children of a game object and can add it into a given list 
    //based on if it has the required script or not 
    void TraverseChildren(Transform parentTransform)
    {
        foreach (Transform child in parentTransform)
        {

            DialogData dialogDataComponent = child.GetComponent<DialogData>();

            if (dialogDataComponent != null)
            {
                dialogChildren.Add(child.gameObject);
            }

            // Recursively call the function for the grandchildren
            TraverseChildren(child);
        }
    }

   



    //this is the saving function 
    public void Save(DialogData selectedOption)
    {
        //clears child list then remakes it 
        dialogChildren.Clear();
        if (dialogParent != null)
        {
            TraverseChildren(dialogParent.transform);
        }
        // uses method in save npc to create the bool list in the data

        SaveSystem.SaveNPC(npcName, dialogChildren, selectedOption);
    }

    //Next dialog check is checking if there is a next dialog and 
    // sets the start dialog option to that 
    // this ensures that next time player interacts, it's the next 
    // dialog 
    public void NextDialogCheck(DialogData selectedOption)
    {

        if(selectedOption.nextDialog != null)
        {
            startDialogObject = selectedOption.nextDialog;
        }
        StartCoroutine(displayDialog(selectedOption));


    }

    //Option selected controls - this is referenced in buttons that get spawned 
    // when option is selected, it will check for next dialog then display the new
    // info 
    public void OptionSelected(DialogData selectedOption)
    {
        
        optionSelected = true;
        startDialogObject = selectedOption;
        //this is the dialog action associated with the selected option 
     
        DialogAction dialogAction = startDialogObject.gameObject.GetComponent<DialogAction>();

        if (dialogAction != null)
        {
            dialogAction.Action();
        }

        NextDialogCheck(selectedOption);

        
    }

    //Display dialog handles the actual displaying of the dialog, 
    //length, and choice options
    IEnumerator displayDialog(DialogData selectedOption)
    {

        yield return null;
        currentlySpeaking = true;
        //spawned button list is for object pooling to destroy it all
        //when object is selected 

        List<GameObject> spawnedButtons = new List<GameObject>();
        dialogCanvas.SetActive(true);

        // this is looping through the dialog segments of the dialog object
        foreach (var dialog in selectedOption.dialogSegments)
        {

            if (audioSource != null)
            {
                if (dialog.audio != null)
                {
                    audioSource.clip = dialog.audio;
                    audioSource.Play();
                }

            }
            if (dialog.dialogChoices.Count == 0)
            {
                // if there is no choices - just make the text show up for the display time. 
                dialogText.text = dialog.dialogText;
                float startTime = Time.time;
                float elapsedTime = 0f;

                if(megaSkip == false)
                {
                    skipTime = dialog.dialogDisplayTime;
                }
                else
                {
                    skipTime = 0f; 
                }


                while (elapsedTime < skipTime)
                {
                    
                    elapsedTime = Time.time - startTime;

                    yield return null; 
                }
            }
            else
            {
                //if there are choices - open the options container
                dialogOptionsContainer.SetActive(true);
                //open options panel 
                foreach (var option in dialog.dialogChoices)
                {
                    //for each option, set the dialog test and make new buttons 
                    // new buttons are added to the button game object list to delete later
                    dialogText.text = dialog.dialogText;
                    GameObject newButton = Instantiate(dialogOptionsPrefab, dialogOptionsParent);
                    spawnedButtons.Add(newButton);

                    // new buttons are then set up to have the option press ability
                    Debug.Log("New Button made " + dialog.dialogText); 
                    newButton.GetComponent<UIDialogOption>().SetUp(this, option.followingDialog, option.choiceText);
                    //make a list of these button objects and feed it into UIDialogOption
                 
                }
                //for controller support 

                for (int i = 0; i < spawnedButtons.Count; i++)
                {
                    //choose the first to set the event system to it 
                    if(i == 0)
                    {
                        EventSystem.current.SetSelectedGameObject(spawnedButtons[i]);
                    }
                    // If spawned button I isn't first (has a previous button)
                    if (i != 0)
                    {
                        // Get the current button and its Navigation
                        Button currentButton = spawnedButtons[i].GetComponent<Button>();
                        Navigation nav = currentButton.navigation;

                        // Set the up navigation target
                        Button upButton = spawnedButtons[i - 1].GetComponent<Button>();
                        nav.selectOnUp = upButton;

                        // Assign the modified Navigation back to the button
                        currentButton.navigation = nav;
                    }

                    // If spawned button I isn't the last one (has a next button)
                    if (i != (spawnedButtons.Count - 1))
                    {
                        // Get the current button and its Navigation
                        Button currentButton = spawnedButtons[i].GetComponent<Button>();
                        Navigation nav = currentButton.navigation;

                        // Set the down navigation target
                        Button downButton = spawnedButtons[i + 1].GetComponent<Button>();
                        nav.selectOnDown = downButton;

                        // Assign the modified Navigation back to the button
                        currentButton.navigation = nav;
                    }
                }
                



                // wait for the option to be selected before going on 
                while (!optionSelected)
                {
                    yield return null;
                }
                break;
            }
        }
        //when finished with segments - look to see if the dialog container has a next segment option 
        if (selectedOption.nextSegment)
        {
            NextDialogCheck(selectedOption.nextDialog);
        }

        //when the dialog segments and choices are done, the container and canvas are turned off. 
        dialogOptionsContainer.SetActive(false);
        dialogCanvas.SetActive(false);
        optionSelected = false;
        // the created buttons for choices are destoryed. 
        foreach (GameObject button in spawnedButtons)
        {
            Destroy(button);
        }

        currentlySpeaking = false;
    }

}
