using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;



public class LevelButtonManagement : MonoBehaviour
{
    public UnityEngine.UI.Button[] buttons;
    public List<GameObject> groupedObjects;
 

    // Start is called before the first frame update
    void Start()
    {
                var player = PlayerManager.Instance;
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (player.playerData.level >= i)
                    {
                        buttons[i].gameObject.SetActive(true);
                        Debug.Log("leveli: " + i);
                    }
                }
     }

    public void CloseButton()
    {
  
        GameObject canvas = GameObject.Find("EpäiltykansioCanvas");

        foreach (GameObject group in groupedObjects)
        {
            if (group != null)
            {
                group.SetActive(false);
            }
        }
    

        if (canvas != null)
        {
            canvas.SetActive(false);
        }
       

        
    }
}

        
    

    

