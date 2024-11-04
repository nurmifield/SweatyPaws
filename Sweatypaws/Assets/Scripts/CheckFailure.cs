using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFailure : MonoBehaviour
{
    
    public bool correctOrder = true;
    public int sectionsCleared = 0;
    public int MustBeCleared;
    public Score score;
    public JsonReader jsonReader;

    void Start()
    {
        {
            GameObject reader = GameObject.Find("Reader");
            if (reader != null)
            {
                jsonReader = reader.GetComponent<JsonReader>();
                if (jsonReader != null)
                {
                    MustBeCleared = jsonReader.bombData.level.must_be_cleared;
                }
            }
        }
    }


    public void SetSections(int newSectionsCleared)
    {
        sectionsCleared = newSectionsCleared;

        if (sectionsCleared < MustBeCleared)
        {
            // TÄSTÄ MENNÄÄN GAME OVER NÄKYMÄÄN
            GetComponent<GameOverScreen>().GameOverScreenManage();
            Debug.Log("PELI LOPPUI RÄJÄHDIT");
        }
        else
        {
            // PISTEEN VÄHENNYSTÄ TÄNNE
            score.MinusScore();
            Debug.Log("PENALTYÄ , PELI EI KUITENKAAN LOPPUNUT!");
        }


    }

 
}
