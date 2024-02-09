using UnityEngine;
//chatgpt wrote this given an ask for a controller checker 
public class ControllerManager : MonoBehaviour
{
    public delegate void Connected ();
    public static event Connected OnControllerConnected;
    public static event Connected OnControllerDisconnected;

    private void Start()
    {
        InvokeRepeating("CheckConnection", 0f, 1f); // Check every second (you can adjust the interval)
    }

    private void CheckConnection()
    {
        string[] joystickNames = Input.GetJoystickNames();

        if (joystickNames.Length > 0)
        {
            // At least one controller is connected
            if (OnControllerConnected != null)
            {
                if(!string.IsNullOrEmpty(joystickNames[0]))
                    OnControllerConnected.Invoke();
                else
                    OnControllerDisconnected.Invoke();
            }
        }
        else
        {
            // No controllers are connected
            if (OnControllerDisconnected != null)
            {
               
               
                OnControllerDisconnected.Invoke();
            }
        }
    }
}