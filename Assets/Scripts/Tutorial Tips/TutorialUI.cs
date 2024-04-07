using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{

    public GameObject tipBox;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tipBox.SetActive(true); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Assuming the object exiting the trigger is tagged as "Player"
        {
            //tipBox.SetActive(false); // Deactivate the UI element
            StartCoroutine(DelayedDeactivate());

        }
    }

    private IEnumerator DelayedDeactivate()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        tipBox.SetActive(false); // Deactivate the UI element
    }

}
