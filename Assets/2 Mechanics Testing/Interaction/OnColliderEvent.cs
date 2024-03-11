using UnityEngine;
using UnityEngine.Events;

public class OnColliderEvent : MonoBehaviour
{
    public UnityEvent onTriggerEnterEvent;

    public UnityEvent onTriggerExitEvent;

    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnterEvent.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        onTriggerExitEvent.Invoke();
    }

  

   
}