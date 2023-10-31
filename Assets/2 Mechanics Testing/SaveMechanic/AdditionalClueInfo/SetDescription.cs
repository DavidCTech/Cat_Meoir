using UnityEngine;
using TMPro;

public class SetDescription : MonoBehaviour
{
    private TextMeshProUGUI starterText;
    //this script goes on the description object 
    private void Start()
    {
        starterText = this.gameObject.GetComponent<TextMeshProUGUI>(); 
    }

    public void ChangeText(ImageData imageData)
    {
        if(imageData.description != null)
        {
            starterText.text = imageData.description;
        }
        

    }
}
