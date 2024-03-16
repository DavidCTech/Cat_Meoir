using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBoardOpener : MonoBehaviour
{
    public GameObject enableBoard;
    public bool isInClueBoard = false;

    private Coroutine clueBoardUp;

    public void EnterOrExitClueBoard()
    {
        if (!isInClueBoard)
        {
            EnterClueBoard();
        }
        else
        {
            ExitClueBoard();
        }
    }

    public void EnterClueBoard()
    {
        if (enableBoard != null)
        {
            enableBoard.SetActive(true);
        }
        isInClueBoard = true;
        clueBoardUp = StartCoroutine(ClueBoardUp());
    }

    public void ExitClueBoard()
    {
        if (enableBoard != null)
        {
            enableBoard.SetActive(false);
        }
        isInClueBoard = false;

        if (clueBoardUp != null)
        {
            StopCoroutine(clueBoardUp);
        }

        // Reset time scale and lock cursor
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public IEnumerator ClueBoardUp()
    {
        // Keep the time scale 0 and unlock the cursor while the screen is up
        while (isInClueBoard)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            yield return null;
        }
    }
}
