using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{
    public GameObject loadingScreen;
    public PauseMenu pauseMenu; 
    public void Saveplayer()
    {
        SaveSystem.SavePlayer(this.gameObject.transform);
    }
    public void LoadPlayer()
    {
        pauseMenu.Resume();
        loadingScreen.SetActive(true);
        StartCoroutine(LoadPlayerAsync());
       
    }

    private IEnumerator LoadPlayerAsync()
    {
        
        yield return new WaitForSeconds(0.5f);
        
        loadingScreen.SetActive(false);


        // Code to run during the asynchronous operation (if needed)

        
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            Vector3 newPosition = new Vector3(data.playerX, data.playerY, data.playerZ);
            this.gameObject.transform.position = newPosition;
        }
        else
        {
            Debug.LogError("Failed to load player data!");
        }
    }

}
