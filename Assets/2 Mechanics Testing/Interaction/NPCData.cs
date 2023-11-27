using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCData
{
    public bool[] dialogCheck;
    //basically ill want a npc save area with a drag and drop of all the game objects to save 
    // then it will set a list bool of them 
    // the list will be used to cross ref which one needs to be loaded into the npc 

    public NPCData(List<GameObject> dialogList, DialogData selectedOption)
    {
        dialogCheck = new bool[dialogList.Count];
        for (int i = 0; i < dialogList.Count; i++)
        {
            
            if (dialogList[i].GetComponent<DialogData>() == selectedOption)
            {
                dialogCheck[i] = true;
            }
            else
            {
                dialogCheck[i] = false;
            }
            
            
        
        }


     }
}
