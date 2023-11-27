using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    //I just added a header to your code and moved it to recognize the script not the gameobject - so it's faster. Sorry if you have to redrag it in. 
    [Header("Drag in gameObject with FadeScreen Script.")]
    public GameObject Fade;
    private FadeScreen fadeScreen;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        fadeScreen = Fade.GetComponent<FadeScreen>();
        rb = GetComponent<Rigidbody>();
        this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            rb.isKinematic = true;
            //rb.gameObject.SetActive(false);

            //I also Added a null check to it so it doesnt show errors. 
            if (fadeScreen != null)
            {
                fadeScreen.StartFade();
            }
        }

        if (other.gameObject.CompareTag("Killer"))
        {
            if (fadeScreen != null)
            {
                fadeScreen.StartFade();
                StartCoroutine(ReloadScene());
            }
        }
    }

    IEnumerator ReloadScene() //Delete Later
    {
        yield return new WaitForSeconds(2.0f);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
