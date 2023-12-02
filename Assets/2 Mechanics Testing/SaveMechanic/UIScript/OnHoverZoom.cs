using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverZoom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector2 initialPosition;
    private Vector2 initialSize;
    public float downValue = -10f;
    public Transform newParent;
    private Transform initialParent;

    public void OnPointerEnter(PointerEventData eventData)
    {
        initialPosition = this.gameObject.GetComponent<RectTransform>().anchoredPosition;
        initialSize = this.gameObject.GetComponent<RectTransform>().sizeDelta;
        initialParent = this.gameObject.transform.parent; 


        this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, downValue);
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2((float)(960/1.5), (float)(540/1.5));
        this.gameObject.transform.parent = newParent; 

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.gameObject.transform.parent = initialParent;
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = initialPosition;
        this.gameObject.GetComponent<RectTransform>().sizeDelta = initialSize; 
    }
}
