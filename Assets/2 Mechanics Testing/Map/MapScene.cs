using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapScene : MonoBehaviour
{
    public GameObject[] sceneMaps;

    private void Start()
    {
        SceneCheck();
    }

    void SceneCheck()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "NewHubWorld")
        {
            GameObject firstObject = sceneMaps[0];
            firstObject.SetActive(true);
        }
        if (currentScene.name == "Apartment")
        {
            GameObject firstObject = sceneMaps[1];
            firstObject.SetActive(true);
        }
        if (currentScene.name == "PoliceDepartment")
        {
            GameObject firstObject = sceneMaps[2];
            firstObject.SetActive(true);
        }
        if (currentScene.name == "BookStore")
        {
            GameObject firstObject = sceneMaps[3];
            firstObject.SetActive(true);
        }
        if (currentScene.name == "coffeeshop")
        {
            GameObject firstObject = sceneMaps[4];
            firstObject.SetActive(true);
        }
        if (currentScene.name == "ShoeStore")
        {
            GameObject firstObject = sceneMaps[5];
            firstObject.SetActive(true);
        }
        if (currentScene.name == "NightClub")
        {
            GameObject firstObject = sceneMaps[6];
            firstObject.SetActive(true);
        }
    }
}