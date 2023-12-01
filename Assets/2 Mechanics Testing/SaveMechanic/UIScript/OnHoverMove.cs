using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//chatgpt helped write this script 
public class OnHoverMove : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float hoverTranslation = 1f; // Adjust this value to control the amount of translation
    public float exitTranslation = -1f;    // You can set this to 0 for no translation on exit

    private RectTransform buttonRectTransform;
    private Vector2 initialPosition;
    public Vector2 outPosition;
    public Vector2 inPosition; 
    public bool goUp;
    public bool isOn; 

    private void Start()
    {
        buttonRectTransform = GetComponent<RectTransform>();
        initialPosition = buttonRectTransform.anchoredPosition;
        
    }
    void OnDisable()
    {
        
        buttonRectTransform.anchoredPosition = initialPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Translate the button to the left
        if (isOn)
        {
            if (!goUp)
            {
                buttonRectTransform.anchoredPosition = initialPosition + new Vector2(hoverTranslation, 0);
            }
            else
            {
                buttonRectTransform.anchoredPosition = initialPosition + new Vector2(0, hoverTranslation);
            }
        }
       
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Translate the button to the right
        if (isOn)
        {
            if (!goUp)
            {
                buttonRectTransform.anchoredPosition = initialPosition + new Vector2(exitTranslation, 0);
            }
            else
            {
                buttonRectTransform.anchoredPosition = initialPosition + new Vector2(0, exitTranslation);
            }
        }
        
    }
    public void OutTab()
    {
        buttonRectTransform.anchoredPosition = outPosition;
        
        isOn = false; 
    }
  
    public void InTab()
    {
        buttonRectTransform.anchoredPosition = inPosition;
        initialPosition = inPosition; 
        isOn = true; 
    }

}
