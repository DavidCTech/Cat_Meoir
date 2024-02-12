using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickingMinigame : MonoBehaviour
{
    [SerializeField] float pickSpeed = 3f;
    float pickPosition;

    public float PickPosition
    {
        get 
        { 
            return pickPosition; 
        }
        set
        {
            pickPosition = value;
            pickPosition = Mathf.Clamp(pickPosition, 0f, 1f);
        }
    }

    float slotPosition;

    public float SlotPosition
    {
        get 
        { 
            return slotPosition; 
        }
        set
        {
            slotPosition = value;
            slotPosition = Mathf.Clamp(slotPosition, 0f, 1f);
        }
    }

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Pick();
        UpdateAnimator(); 
    }

    private void Pick()
    {
        PickPosition += Input.GetAxisRaw("Horizontal") * Time.deltaTime * pickSpeed;
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("PickPosition", PickPosition);
    }
}
