using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextNumer : MonoBehaviour
{
    public TextMeshProUGUI mainNumer;
    public TextMeshProUGUI optNumer;
    public GameObject[] imageList;
    int mainNumerInt;
    int optNumerInt; 

    public void AddOneMain()
    {
        mainNumerInt = mainNumerInt + 1;
       // mainNumer.text = newInt.ToString(); 
    }
    public void AddOneOpt()
    {
        
        optNumerInt = optNumerInt + 1;
    }

    public void GetNumer()
    {
        mainNumerInt = 0;
        optNumerInt = 0; 

        foreach( GameObject image in imageList)
        {

            if (image.GetComponent<ImageData>() != null)
            {
                string mainString = image.GetComponent<ImageData>().mainString;
                if(mainString == "Main Clue")
                {
                    AddOneMain(); 
                }
                if(mainString == "Optional Clue")
                {
                    AddOneOpt(); 
                }
            } 
        }
        mainNumer.text = mainNumerInt.ToString();
        optNumer.text = optNumerInt.ToString(); 
    }
}
