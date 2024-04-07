using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerSave : MonoBehaviour
{

    public GameObject loadingScreen;
    private string sceneName;
    private bool doneLoad = false; 
    /*
    public GameObject sceneLoader;
    public delegate void OnUnityEventFinished();
    public static event OnUnityEventFinished UnityEventFinished;

    private void OnEnable()
    {
        if (sceneLoader != null)
        {
            SceneLoader loader = sceneLoader.GetComponent<SceneLoader>();
            if (loader != null)
            {
                loader.onEnableEvent.AddListener(OnUnityEventDone);
            }
        }
    }

    private void OnDisable()
    {
        if (sceneLoader != null)
        {
            SceneLoader loader = sceneLoader.GetComponent<SceneLoader>();
            if (loader != null)
            {
                loader.onEnableEvent.RemoveListener(OnUnityEventDone);
            }
        }
    }

    private void OnUnityEventDone()
    {
        Debug.Log("unity event finished");
        if (UnityEventFinished != null)

            UnityEventFinished();
    }
    */
    public void SavePlayer()
    {
        if (doneLoad)
        {// If the Unity event has finished, then save the player
            string sceneName = SceneManager.GetActiveScene().name;
            Debug.Log("Saving player");
            SaveSystem.SavePlayer(this.gameObject.transform, sceneName);

        }
            
        
       
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
        Debug.Log("loading player data in " + sceneName);
        if (data != null)
        {
            Vector3 newPosition = new Vector3(data.playerX, data.playerY, data.playerZ);
            Debug.Log("current position is " + this.gameObject.transform.position);
            Debug.Log("New position is: " + newPosition);
            this.gameObject.transform.position = newPosition;
        }
        doneLoad = true;
    }

}
