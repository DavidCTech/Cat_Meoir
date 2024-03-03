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
        
        InvokeRepeating("AutoSave", 10f, 20f);
        
        

    }

    public void AutoSave()
    {
        Debug.Log("Save");
        Save.Invoke(); 

    }
    // Update is called once per frame

}
