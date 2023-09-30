using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public bool _canHide;
    public bool _isHidden;


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
        if(_canHide == true)
        {
            Hide();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
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
        if(_canHide == true && _isHidden == false)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            _isHidden = true;
        }
    }
}
