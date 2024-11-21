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
    public GameObject[] buttons;
    public GameObject[] perfectImage;
    public List<GameObject> groupedObjects;
    public LevelMapManager mapManager;
    public LoadingScene loadingScreen;
    private JsonReader jsonReader;
    public Sprite levelImage;
    public Sprite youWinImage;


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
                        if (!buttons[i].activeSelf)
                        {
                            buttons[i].SetActive(true);
                        }
                        UnityEngine.UI.Image buttonsImage = buttons[i].GetComponent<UnityEngine.UI.Image>();
                        Animator buttonAnimator = buttons[i].GetComponent<Animator>();
                        if (player.playerData.level_progress[i].complete == true)
                        {
                            buttonsImage.sprite = youWinImage;
                            buttonAnimator.enabled = false;
                            if (player.playerData.level_progress[i].score == player.playerData.level_progress[i].max_score)
                            {

                                perfectImage[i].SetActive(true);
                            }
                           
                        }
                        else
                        {
                            buttonsImage.sprite = levelImage;
                            
                        }
                     
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
            loadingScreen.PlayTimeLine();
            //SceneManager.LoadScene("Game");
        }
        
        //SceneManager.LoadScene("Game");
    }


}

        
    

    

