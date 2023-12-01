using UnityEngine;
using TMPro;
//using UnityEngine.UI; 

public class SetDescription : MonoBehaviour
{
    private TextMeshProUGUI starterText;
    private string startText; 
    //this script goes on the description object 
    private void Start()
    {
        starterText = this.gameObject.GetComponent<TextMeshProUGUI>();
        startText = starterText.text; 
    }

    public void ChangeText(ImageData imageData)
    {
        if(imageData.description != null && imageData.description != "")
        {
            starterText.text = imageData.description;
        }
        else
        {
            starterText.text = startText; 
        }

    }
}
