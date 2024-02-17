using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerSave : MonoBehaviour
{

    public GameObject loadingScreen;
    private string sceneName; 
    

    public void Saveplayer()
    {
        sceneName = SceneManager.GetActiveScene().name;
       
        SaveSystem.SavePlayer(this.gameObject.transform, sceneName);
       
    }
    public void LoadPlayer()
    {
        if(loadingScreen != null)
        {
            loadingScreen.SetActive(true);
        }
        StartCoroutine(LoadPlayerAsync());
       
    }
    public void DeletePlayer(string slotName)
    {
        SaveSystem.DeletePlayer(slotName);
    }

    private IEnumerator LoadPlayerAsync()
    {
        
        yield return new WaitForSeconds(0.5f);
        if(loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }
        // Code to run during the asynchronous operation (if needed)
        sceneName = SceneManager.GetActiveScene().name;
        PlayerData data = SaveSystem.LoadPlayer(sceneName);
        if (data != null)
        {
            Vector3 newPosition = new Vector3(data.playerX, data.playerY, data.playerZ);
            this.gameObject.transform.position = newPosition;
        }
        else
        {
            Debug.Log("No Player data.");
            // put the failed to load gemaobject UI On 
        }
    }

}
