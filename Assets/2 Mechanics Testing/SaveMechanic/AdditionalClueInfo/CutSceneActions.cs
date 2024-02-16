using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneActions : MonoBehaviour
{
    public List<ClueEventTrigger> triggers = new List<ClueEventTrigger>();
    public PhotoManager photoManager;



    public void TriggerActions()
    {
       
        for (int i = triggers.Count - 1; i >= 0; i--)
        {
            triggers[i].Check(photoManager);

        }
        
    }

}
