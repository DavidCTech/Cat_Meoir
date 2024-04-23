using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClueBoardOpener : MonoBehaviour
{
    public GameObject enableBoard;
    public bool isInClueBoard = false;

    private Coroutine clueBoardUp;
    public InputActionReference exitButtonRY; // Reference to the North button action


    public void EnterOrExitClueBoard()
    {
        if (!isInClueBoard)
        {
            EnterClueBoard();
        }
        /*else
        {
            ExitClueBoard();
            //ClueBoardExit();
        }*/
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

    /*public void ExitClueBoard()
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
    }*/

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

    public void ExitClueBoard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ExitClueBoard();
        }
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

    private void OnEnable()
    {
        // Enable the input action callback when the script is enabled
        exitButtonRY.action.Enable();
        // Add ExitClueBoard as a callback to the North button action
        exitButtonRY.action.performed += ExitClueBoard;
    }

    private void OnDisable()
    {
        // Disable the input action callback when the script is disabled
        exitButtonRY.action.Disable();
        // Remove ExitClueBoard as a callback from the North button action
        exitButtonRY.action.performed -= ExitClueBoard;
    }
}
