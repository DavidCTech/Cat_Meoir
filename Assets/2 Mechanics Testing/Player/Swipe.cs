using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public float swipeForce = 3f;
    public float swipeDuration = 0.5f; //How long the swipe will last


    private BoxCollider swipeHitBox; 
    public bool isSwiping = false;

    public Animator anim;


    private void Start()
    {
        swipeHitBox = GetComponent<BoxCollider>();

        if (swipeHitBox != null)
        {
            swipeHitBox.enabled = false;
        }
    }

    public void EnableCollider()
    {
        if (swipeHitBox != null)
        {
            swipeHitBox.enabled = true;
            isSwiping = true;
            StartCoroutine(DisableColliderAfterDuration());
        }
    }

    public void Swiping()
    {
        // Enable the collider
        EnableCollider();
        anim.SetBool("Swipe", true);
    }
    public IEnumerator DisableColliderAfterDuration()
    {
        yield return new WaitForSeconds(swipeDuration);

        // Disable the collider after the swipe duration
        swipeHitBox.enabled = false;
        isSwiping = false;
        anim.SetBool("Swipe", false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (isSwiping && swipeHitBox.enabled && other.attachedRigidbody != null)
        {
            // Calculate the direction based on the relative position of the colliders
            Vector3 forceDirection = other.transform.position - transform.position;

            // Apply force to the collided rigidbody
            other.attachedRigidbody.AddForce(forceDirection.normalized * swipeForce, ForceMode.Impulse);
        }
    }
}
