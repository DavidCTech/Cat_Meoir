using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{

    public GameObject tipBox;
    public float timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tipBox.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //tipBox.SetActive(false);
            StartCoroutine(DelayedDeactivate());

        }
    }

    private IEnumerator DelayedDeactivate()
    {
        yield return new WaitForSeconds(timer);
        tipBox.SetActive(false);
    }

}
