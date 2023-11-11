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

        if (currentScene.name == "Mechanics")
        {
            GameObject firstObject = sceneMaps[0];
            firstObject.SetActive(true);
        }
        if (currentScene.name == "MapScene2")
        {
            GameObject firstObject = sceneMaps[1];
            firstObject.SetActive(true);
        }
    }
}
