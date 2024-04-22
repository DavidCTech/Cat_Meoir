using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ArialJournal : MonoBehaviour
{
    public TMP_Text[] allTexts;
    public TMP_FontAsset[] originalFonts;
    public TMP_FontAsset arialFont;
    public TMP_FontAsset typeWriterFont;

    void Start()
    {
        if (allTexts != null)
        {
            allTexts = GetComponentsInChildren<TMP_Text>();

            originalFonts = new TMP_FontAsset[allTexts.Length];
            for (int i = 0; i < allTexts.Length; i++)
            {
                originalFonts[i] = allTexts[i].font;
            }

            ArialJournalManager.instance.UpdateList(this);
        }

        if (PlayerPrefs.GetInt("ArialJournalState") == 1)
        {
            ChangeToArialFont();
        }
        else
        {
            ChangeBacktoTypeWriterFont();
        }
    }

    public void ChangeToArialFont()
    {
        for (int i = 0; i < originalFonts.Length; i++)
        {
            allTexts[i].font = arialFont;
        }

        Debug.Log("Changing to Arial");
    }

    public void ChangeBacktoTypeWriterFont()
    {
        for (int i = 0; i < originalFonts.Length; i++)
        {
            allTexts[i].font = typeWriterFont;
        }

        Debug.Log("Changing to TypeWriter");
    }
}
