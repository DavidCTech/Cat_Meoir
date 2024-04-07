
using UnityEngine;

public class TimeStopper : MonoBehaviour
{

    //only make the top go occur when the journal menu is open 
    public GameObject journalMenu; 
    public void Stop()
    {
        if (journalMenu != null) 
        {
            if (!journalMenu.activeInHierarchy)
            {
                Time.timeScale = 0f;
            }
        }
        else
        {
            Time.timeScale = 0f;
        }
      
    }

    // Update is called once per frame
    public void Go()
    {
        if (journalMenu != null)
        {
            if (!journalMenu.activeInHierarchy)
            {
                Debug.Log(this.gameObject + " sets time to go");
                Time.timeScale = 1f;
            }
        }
        else
        {
            Time.timeScale = 1f;

        }
    }
}
