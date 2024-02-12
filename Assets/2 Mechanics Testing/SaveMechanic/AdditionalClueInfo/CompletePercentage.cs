using UnityEngine;
using TMPro;
using System;

public class CompletePercentage : MonoBehaviour
{
    public TextMeshProUGUI mainDenom;
    public TextMeshProUGUI mainNumer;
    public TextMeshProUGUI optDenom;
    public TextMeshProUGUI optNumer;

    public TextMeshProUGUI completeText;
    public GameObject flashBackButton;

    private int mainNumerInt;
    private int mainDenomInt; 

    private void OnEnable()
    {
        //this should work 
        this.gameObject.GetComponent<TextNumer>().GetNumer();  
        completeText.text =  $"{GetPercentage().ToString("F0")}% Completed";
        if( mainNumerInt == mainDenomInt)
        {
            flashBackButton.SetActive(true);
        }


    }
    //chat gpt helped write this script 
    public double GetPercentage()
    {
        string mainCutDenom = mainDenom.text.Substring(1);
        mainDenomInt = int.Parse(mainCutDenom);
        mainNumerInt = int.Parse(mainNumer.text);
        int optDenomInt = 0;
        int optNumerInt = 0; 

        if(optDenom.text != "")
        {
            string optCutDenom = optDenom.text.Substring(1);
            optDenomInt = int.Parse(optCutDenom);
            optNumerInt = int.Parse(optNumer.text);
        }
        

        double result; 

        if(optDenomInt == 0 || optDenom.text == "")
        {
            result = mainNumerInt / mainDenomInt;
            return result; 
        }
        Fraction mainFraction = new Fraction(mainNumerInt, mainDenomInt);
        Fraction optFraction = new Fraction(optNumerInt, optDenomInt);

        // Combine fractions and get the result as a percentage
       
        result = CombineFractions(mainFraction, optFraction);
        Debug.Log(optFraction);
        return result; 

    }

    // Fraction class representing a fraction with numerator and denominator
    private class Fraction
    {
        public int Numerator { get; }
        public int Denominator { get; }

        public Fraction(int numerator, int denominator)
        {
           

            Numerator = numerator;
            Denominator = denominator;
        }

        public double ToDouble()
        {
            return (double)Numerator / Denominator;
        }
    }

    // CombineFractions method to calculate combined percentage
    private double CombineFractions(Fraction fraction1, Fraction fraction2)
    {
        int sumNumerators = fraction1.Numerator + fraction2.Numerator; 
        int sumDenominators =fraction1.Denominator + fraction2.Denominator; 

        Fraction combinedFraction = new Fraction(sumNumerators, sumDenominators);
        double combinedPercentage = combinedFraction.ToDouble() * 100;

        return combinedPercentage;
    }

}
