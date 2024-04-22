using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArialJournalManager : MonoBehaviour
{
    public static ArialJournalManager instance;

    [Header("Journal Arial Stuff")]
    public Toggle journalArialToggle;
    public List<ArialJournal> journalArial = new List<ArialJournal>();
    [HideInInspector] public bool isUsingArialInJournal = false;
    private int journalArialInt;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        journalArialInt = PlayerPrefs.GetInt("ArialJournalState");

        if (PlayerPrefs.HasKey("ArialJournalState"))
        {
            if (journalArialInt == 1)
            {
                journalArialToggle.isOn = true;
                isUsingArialInJournal = true;
                SwitchAllTextToArial();
            }
            else
            {
                journalArialToggle.isOn = false;
                isUsingArialInJournal = false;
                SwitchAllTextToTypeWriter();
            }
        }
    }

    void Start()
    {

    }

    public void UpdateList(ArialJournal journal)
    {
        journalArial.Add(journal);
    }

    public void SetJournalArialMode(bool isUsingArialInJournal)
    {
        journalArialToggle.isOn = isUsingArialInJournal;
    }

    public void ApplyArialFontSettings()
    {
        if (!journalArialToggle.isOn)
        {
            PlayerPrefs.SetInt("ArialJournalState", 0);
            isUsingArialInJournal = false;
            SwitchAllTextToTypeWriter();
        }
        else
        {
            PlayerPrefs.SetInt("ArialJournalState", 1);
            isUsingArialInJournal = true;
            SwitchAllTextToArial();
        }
    }

    public void SwitchAllTextToArial()
    {
        foreach (ArialJournal journal in journalArial)
        {
            journal.ChangeToArialFont();
        }
    }

    public void SwitchAllTextToTypeWriter()
    {
        foreach (ArialJournal journal in journalArial)
        {
            journal.ChangeBacktoTypeWriterFont();
        }
    }
}
