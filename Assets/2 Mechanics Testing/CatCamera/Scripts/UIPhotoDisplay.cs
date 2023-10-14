using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPhotoDisplay : MonoBehaviour
{
    public Canvas lastPhotoCanvas;

    public void TurnUIOn()
    {
        lastPhotoCanvas.gameObject.SetActive(true);
    }
    public void TurnUIOff()
    {
        lastPhotoCanvas.gameObject.SetActive(false);
    }

}
