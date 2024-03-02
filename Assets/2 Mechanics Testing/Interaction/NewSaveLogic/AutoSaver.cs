using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class AutoSaver : MonoBehaviour
{

    public UnityEvent Save;
    // Start is called before the first frame update
    void OnEnable()
    {
        //invoke the autosave featre every 10 minutes 
        
        InvokeRepeating("AutoSave", 0f, 10f);
        
        

    }

    public void AutoSave()
    {
        Save.Invoke(); 

    }
    // Update is called once per frame

}
