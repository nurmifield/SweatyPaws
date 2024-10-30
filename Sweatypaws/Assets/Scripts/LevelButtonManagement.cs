using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;


public class LevelButtonManagement : MonoBehaviour
{
    public UnityEngine.UI.Button[] buttons;
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
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (jsonReader.player.level >= i)
                    {
                        buttons[i].gameObject.SetActive(true);
                        Debug.Log("leveli: " + i);
                    }
                }
            }
        }

        
    }

    
}
