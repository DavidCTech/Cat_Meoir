using UnityEngine;
using UnityEngine.Events;

public class ColliderSave : MonoBehaviour
{
    [Header("Add in events such as save, load, or scene change.")]
    public UnityEvent onTriggerEnterEvent;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onTriggerEnterEvent.Invoke();
        }
       
       

    }
}