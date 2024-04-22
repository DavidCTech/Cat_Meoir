using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{

    public string Scene;


    public void EndGame()
    {
        SceneManager.LoadScene(Scene);
    }
}
