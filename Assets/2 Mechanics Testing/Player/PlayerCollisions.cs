using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{

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
            fadeScreen.StartFade();
        }
    }

}
