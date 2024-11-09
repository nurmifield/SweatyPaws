using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLevelManager : MonoBehaviour
{
    public JsonReader jsonReader;
    // Start is called before the first frame update
    void Start()
    {
        GameObject reader = GameObject.Find("Reader");
        if (reader != null)
        {
            jsonReader = reader.GetComponent<JsonReader>();

            if (jsonReader != null)
            {
                //GameObject bomb = Resources.Load<GameObject>(jsonReader.bombData.level.bomb_name);
                GameObject bomb = Resources.Load<GameObject>(jsonReader.bombLogicData.level.bomb_name);
                GameObject instance=Instantiate(bomb);
                Vector2 originalPostion=bomb.transform.position;

                instance.transform.position=originalPostion;
                
                
            }
        }
    }

}
