using UnityEngine;
using TMPro;
using System;

//using UnityEngine.UI; 

public class SetName : MonoBehaviour
{
    private TextMeshProUGUI starterText;
    private string startText;
    //this script goes on the description object 

    public void ChangeText(ImageData imageData)
    {
        string name = imageData.gameObject.name; 
        starterText = this.gameObject.GetComponent<TextMeshProUGUI>();
        startText = starterText.text;
        if (name!= null && !name.StartsWith("image", StringComparison.OrdinalIgnoreCase))

        {


            starterText.text = name;
        }
        else
        {
            starterText.text = startText;
        }

    }
}