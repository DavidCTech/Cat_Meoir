using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VS_CutsceneTimer : MonoBehaviour
{
    [Header("Cutscene Variables")]
    public float timer;
    public string levelToLoad;
    public float lengthOfVideo;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        if (timer >= lengthOfVideo)
        {
            SceneManager.LoadScene(levelToLoad);
        }
        else if (timer < lengthOfVideo)
        {
            timer += Time.deltaTime;
        }
    }
}
