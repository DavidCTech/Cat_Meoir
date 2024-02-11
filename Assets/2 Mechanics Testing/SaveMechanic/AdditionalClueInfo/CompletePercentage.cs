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

    private void OnEnable()
    {
        //this should work 
        completeText.text =  $"{GetPercentage().ToString("F0")}% Completed";

    }
    //chat gpt helped write this script 
    public double GetPercentage()
    {
        string mainCutDenom = mainDenom.text.Substring(1);
        int mainDenomInt = int.Parse(mainCutDenom);
        int mainNumerInt = int.Parse(mainNumer.text);
        int optDenomInt = 0;
        int optNumerInt = 0; 

        if(optDenom.text != "")
        {
            string optCutDenom = optDenom.text.Substring(1);
            optDenomInt = int.Parse(optCutDenom);
            optNumerInt = int.Parse(optNumer.text);
        }
        

        double result; 

        if(optDenomInt == 0 && optDenom.text != "")
        {
            result = mainNumerInt / mainDenomInt;
            return result; 
        }

        Fraction mainFraction = new Fraction(mainNumerInt, mainDenomInt);
        Fraction optFraction = new Fraction(optNumerInt, optDenomInt);

        // Combine fractions and get the result as a percentage
       
        result = CombineFractions(mainFraction, optFraction);
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
        int commonDenominator = LCM(fraction1.Denominator, fraction2.Denominator);
        int commonNumerator1 = fraction1.Numerator * (commonDenominator / fraction1.Denominator);
        int commonNumerator2 = fraction2.Numerator * (commonDenominator / fraction2.Denominator);
        int sumNumerators = commonNumerator1 + commonNumerator2;

        Fraction combinedFraction = new Fraction(sumNumerators, commonDenominator);
        double combinedPercentage = combinedFraction.ToDouble() * 100;

        return combinedPercentage;
    }

    // GCD method for greatest common divisor
    private int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    // LCM method for least common multiple
    private int LCM(int a, int b)
    {
        return (a * b) / GCD(a, b);
    }
}
