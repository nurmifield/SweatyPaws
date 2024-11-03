using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level_1_script : MonoBehaviour
{
    public GameObject EpäiltykansioCanvas;

    public void OpenCanvas()
    {
        if (EpäiltykansioCanvas != null)
        {
            EpäiltykansioCanvas.SetActive(true);
            Transform panelGroup = EpäiltykansioCanvas.transform.Find("LevelGroup1");
            panelGroup.gameObject.SetActive(true);
            GameObject buttonObject = EventSystem.current.currentSelectedGameObject;
            var player = PlayerManager.Instance;
            player.SetSelectedLevel(buttonObject.name);
        }
        else
        {
            Debug.LogWarning("EpäiltykansioCanvas is not assigned in the inspector.");
        }
    }
}