using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

//made by chat gpt
public class SceneLoader : MonoBehaviour
{
    public UnityEvent onEnableEvent;

    private void OnEnable()
    {
        onEnableEvent.Invoke();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
