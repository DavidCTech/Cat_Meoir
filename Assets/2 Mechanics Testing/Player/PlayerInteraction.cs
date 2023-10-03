using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public bool _canHide;
    public bool _isHidden;
    public bool _hidePressed;


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnInteraction()
    {
        if(_canHide == true && _isHidden == false)
        {
            Hide();
        }
        else if(_canHide == true && _isHidden == true)
        {
            unHide();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        //Checks for Hiding Spot
        if (collision.gameObject.CompareTag("Hide"))
        {
            _canHide = true;
        }
        else
        {
            _canHide = false;
        }
    }
    
    void Hide()
    {
        //gameObject.layer = LayerMask.NameToLayer("Invisibility");
        _isHidden = true;
    }
    void unHide()
    {
        //gameObject.layer = LayerMask.NameToLayer("Invisibility");
        _isHidden = false;
    }
}
