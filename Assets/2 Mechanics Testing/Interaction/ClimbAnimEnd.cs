using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbAnimEnd : MonoBehaviour
{
    // this is a method called on the animation jump itself
    public void EndAnim()
    {
        this.gameObject.GetComponent<Animator>().SetBool("Jumping", false); 

    }

}
