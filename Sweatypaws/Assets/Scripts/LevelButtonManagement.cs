using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;



public class LevelButtonManagement : MonoBehaviour
{
    public UnityEngine.UI.Button[] buttons;
    public List<GameObject> groupedObjects;
    public LevelMapManager mapManager;
    private JsonReader jsonReader;


    // Start is called before the first frame update
    void Start()
    {
        GameObject reader = GameObject.Find("Reader");
        if (reader != null)
        {
            jsonReader = reader.GetComponent<JsonReader>();

            if (jsonReader != null)
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

    public void StartLevel()
    {
        jsonReader.UpdateNewDialogueList();
        if (mapManager.CheckDialogue())
        {
            mapManager.StartDialogChangeScene();
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
        
        //SceneManager.LoadScene("Game");
    }
}

        
    

    

