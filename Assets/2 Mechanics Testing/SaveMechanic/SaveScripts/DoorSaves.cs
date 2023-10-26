using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSaves : MonoBehaviour
{
    //This script goes on the game manager or an object in the scene to make sure anything that shouldnt appear after unlocking doesn't appear. ( aka doors) 

    public GameObject[] doors;
    private bool[] boolList;
    public PauseMenu pauseMenu;
    public GameObject loadingScreen; 

    public void SaveDoors()
    {
        SaveSystem.SaveDoors(doors);
    }
    public void LoadDoors()
    {
        pauseMenu.Resume();
        loadingScreen.SetActive(true);
        StartCoroutine(LoadDoorAsync());

    }

    private IEnumerator LoadDoorAsync()
    {
        
        yield return new WaitForSeconds(0.5f);
        loadingScreen.SetActive(false);


        DoorData data = SaveSystem.LoadDoors();
        if (data != null)
        {
            //get The data and apply it to the door arrays 
            
            boolList = data.boolList;
            for (int i = 0; i < doors.Length; i++)
            {
                // Check if the corresponding bool value is true (active)
                if (boolList[i])
                {
                    // Set the GameObject to be active
                    if (doors[i] != null)
                    {
                        doors[i].SetActive(true);
                    }
                    else
                    {
                        Debug.LogWarning($"doors[{i}] is null.");
                    }
                }
                else
                {
                    // Set the GameObject to be inactive
                    if (doors[i] != null)
                    {
                        doors[i].SetActive(false);
                    }
                    else
                    {
                        Debug.LogWarning($"doors[{i}] is null.");
                    }
                }
            }

        }
        else
        {
            Debug.LogError("Failed to load Door data!");
        }
    }



}
