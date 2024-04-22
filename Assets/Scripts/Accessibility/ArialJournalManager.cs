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

    [Header("Journal Menus Stuff")]
    public Toggle menuArialToggle;
    public List<ArialMenu> menuArial = new List<ArialMenu>();
    [HideInInspector] public bool isUsingArialInMenus = false;
    private int menuArialInt;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        journalArialInt = PlayerPrefs.GetInt("ArialJournalState");
        menuArialInt = PlayerPrefs.GetInt("ArialMenusState");
    }

    void Start()
    {
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

        if (PlayerPrefs.HasKey("ArialMenusState"))
        {
            if (menuArialInt == 1)
            {
                menuArialToggle.isOn = true;
                isUsingArialInMenus = true;
                SwitchAllTextInMenusToArial();
            }
            else
            {
                menuArialToggle.isOn = false;
                isUsingArialInMenus = false;
                SwitchAllTextToTypeWriterInMenus();
            }
        }
    }

    public void UpdateList(ArialJournal journal)
    {
        journalArial.Add(journal);
    }

    public void UpdateMenuList(ArialMenu menu)
    {
        menuArial.Add(menu);
    }

    public void SetJournalArialMode(bool isUsingArialInJournal)
    {
        journalArialToggle.isOn = isUsingArialInJournal;
    }

    public void SetMenuArialMode(bool isUsingArialInMenus)
    {
        menuArialToggle.isOn = isUsingArialInMenus;
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

        if (!menuArialToggle.isOn)
        {
            PlayerPrefs.SetInt("ArialMenusState", 0);
            isUsingArialInMenus = false;
            SwitchAllTextToTypeWriterInMenus();
        }
        else
        {
            PlayerPrefs.SetInt("ArialMenusState", 1);
            isUsingArialInMenus = true;
            SwitchAllTextInMenusToArial();
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

    public void SwitchAllTextInMenusToArial()
    {
        foreach (ArialMenu menu in menuArial)
        {
            menu.ChangeToArialFont();
        }
    }

    public void SwitchAllTextToTypeWriterInMenus()
    {
        foreach (ArialMenu menu in menuArial)
        {
            menu.ChangeBacktoTypeWriterFont();
        }
    }
}
