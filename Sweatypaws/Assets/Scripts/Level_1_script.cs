using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1_script : MonoBehaviour
{
    public GameObject EpäiltykansioCanvas;

    public void OpenCanvas()
    {
        if (EpäiltykansioCanvas != null)
        {
            EpäiltykansioCanvas.SetActive(true);
        }
        else
        {
            Debug.LogWarning("EpäiltykansioCanvas is not assigned in the inspector.");
        }
    }
}