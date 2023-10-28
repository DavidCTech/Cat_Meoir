using UnityEngine;
using UnityEngine.Events;

public class ColliderSave : MonoBehaviour
{
    public UnityEvent onTriggerEnterEvent;

    void OnTriggerEnter(Collider other)
    {
        onTriggerEnterEvent.Invoke();

    }
}