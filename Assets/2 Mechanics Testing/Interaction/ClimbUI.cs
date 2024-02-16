using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbUI : MonoBehaviour
{
    // Start is called before the first frame update
    //this script will be improved upon later - with added ui stuff but for rn it's what is possible 
    public GameObject climbUI;
    public float offTime;
    public float up; 
    public void TurnOn(Vector3 closest)
    {
        if(climbUI != null)
        {
            climbUI.transform.position = new Vector3(closest.x, closest.y + up, closest.z);
            climbUI.SetActive(true);
        }
        else
        {
            Debug.Log(this.gameObject + " Has no reference for a climbUI");
        }
        

        Invoke("TurnOff", offTime);
    }
    public void TurnOff()
    {
        if(climbUI != null)
        {
            climbUI.SetActive(false);
        }
        
    }
}
