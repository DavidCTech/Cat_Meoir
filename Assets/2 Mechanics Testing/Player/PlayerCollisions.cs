using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    //I just added a header to your code and moved it to recognize the script not the gameobject - so it's faster. Sorry if you have to redrag it in. 
    [Header("Drag in gameObject with FadeScreen Script.")]
    public FadeScreen Fade;
    private FadeScreen fadeScreen;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }

}
