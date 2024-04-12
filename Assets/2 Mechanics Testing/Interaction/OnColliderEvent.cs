using UnityEngine;
using UnityEngine.Events;

public class OnColliderEvent : MonoBehaviour
{
    public UnityEvent onTriggerEnterEvent;

    public UnityEvent onTriggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onTriggerEnterEvent.Invoke();
        }
    }

        private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onTriggerExitEvent.Invoke();
        }
    }




    }