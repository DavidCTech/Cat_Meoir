using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour
{

    public void Saveplayer()
    {
        SaveSystem.SavePlayer(this.gameObject.transform);
    }
    public void LoadPlayer()
    {
    
      
        StartCoroutine(LoadPlayerAsync());
       
    }
    public void DeletePlayer()
    {
        SaveSystem.DeletePlayer();
    }

    private IEnumerator LoadPlayerAsync()
    {
        
        yield return new WaitForSeconds(0.5f);
        // Code to run during the asynchronous operation (if needed)
        PlayerData data = SaveSystem.LoadPlayer();
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
