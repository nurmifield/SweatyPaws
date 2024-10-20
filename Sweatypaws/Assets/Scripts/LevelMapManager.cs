using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapManager : MonoBehaviour
{
    public GameObject dialogCanvas;
    private JsonReader jsonReader;
    // Start is called before the first frame update
    void Start()
    {
        GameObject reader = GameObject.Find("Reader");
        if (reader != null)
        {
            jsonReader = reader.GetComponent<JsonReader>();
            if (jsonReader != null )
            {
                for (int i=0;i< jsonReader.player.dialogue_progress.Length;i++)
                {
                    if (jsonReader.player.dialogue_progress[i].level_index== jsonReader.player.level && jsonReader.player.dialogue_progress[i].watched == false )
                    {
                        Instantiate(dialogCanvas, new Vector2(0, 0), Quaternion.identity);
                    }
                }
                
            }

        }
    }
}
