using UnityEngine;
using TMPro;
public class ClueType : MonoBehaviour
{
    private TextMeshProUGUI starterText;
    private string startText;
    //this script goes on the description object 

    public void ChangeText(ImageData imageData)
    {
        starterText = this.gameObject.GetComponent<TextMeshProUGUI>();
        startText = starterText.text;
        if (imageData.mainString != null && imageData.mainString != "")
        {


            starterText.text = imageData.mainString;
        }
     
        else
        {
            starterText.text = startText;
        }

    }
}
