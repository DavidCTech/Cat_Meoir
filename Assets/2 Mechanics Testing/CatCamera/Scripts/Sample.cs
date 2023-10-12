using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    public PhotoManager photoManager; 
    public List<string> clueNamesThatTheLevelDesignerPutInTheInspector = new List<string>();
    public List<string> cluesThatThePlayerFoundNames = new List<string>(); 
    public bool allCluesFound = false;

    // clueName "Dirt" , "Bloodstain" 
    // 1 clue of dirt to open door 
    // 2 clue of dirt and blood stain open door 
    // if islocked true - functioning door 
    // if isunlocked false - delete door 

    //where to put script - put it on door ( different doors) 


    //reference photomanager 
    //for each snapshot in photoManager.snapshots do the following: 
    // read if its a clue 
    // if snapshot.isClue = true 
    // read name of clue 
    // if it's dirt now unlock door 

    //scalable 
    // public list for inspector of clue names 
    //   public List<String> clueNames = new List<String>();
    // "dirt" "bloodstain"  in a list 
    // photo manager 
    //for each snapshot in photoManager.snapshots do the following: 
    // read if its a clue 
    // if snapshot.isClue = true 
    //       read name of clue 
    //       search for level designer clues 
    //       for each cluename in cluenames do the following: 
    //              if(snapshot.clueName = clueName) 
    //                      good 
    //              if not 
    //                      bad 


    // List.Contains() 
    // list.count() 




    // if it's dirt now unlock door 





    // 1. comment at the top where to put the code 
    // 2. Use headers 
    // 3. make a video 


    // read only 
    // 1,2,3 list is named numbers
    // for each in list 1,2,3
    // add 1 
    // no writing 
    //(int i = 0; i < snapshots.Count; i++)
    // writing 


    // drag and drop public variable 
    //tags or names or layers 
    // look through every gameobjecft and find the needed thing 
    // 


    public void CheckDoorUnlock()
    {
        // Check if all level designer clues are present in player's clues
        allCluesFound = true;
        foreach (string clueName in clueNamesThatTheLevelDesignerPutInTheInspector)
        {
            if (!cluesThatThePlayerFoundNames.Contains(clueName))
            {
                allCluesFound = false;
                break; // No need to check further if one clue is missing
            }
        }


    }
}
