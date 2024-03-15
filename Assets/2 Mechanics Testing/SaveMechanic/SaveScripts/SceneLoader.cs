using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

//made by chat gpt
public class SceneLoader : MonoBehaviour
{

    [Header("Put in load events to load in variables on scene enter")]
    public UnityEvent onEnableEvent;

    private void OnEnable()
    {
        Debug.Log("loading");
        onEnableEvent.Invoke();
    }

    public void LoadScene(string sceneName)
    {
      

        SceneManager.LoadScene(sceneName);
    }
}
