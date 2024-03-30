
using UnityEngine;

public class TimeStopper : MonoBehaviour
{
   
    public void Stop()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    public void Go()
    {
        Time.timeScale = 1f;

    }
}
