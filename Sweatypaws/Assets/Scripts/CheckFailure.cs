using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFailure : MonoBehaviour
{
    
    public bool correctOrder = true;
    public int sectionsCleared = 0;
    public int MustBeCleared = 1;

    // Update is called once per frame
    void Update()
    {
        if (!correctOrder)
        {
            // Pelin lopetusta tänne
            if(sectionsCleared < MustBeCleared)
            {
                GetComponent<GameOverScreen>().GameOverScreenManage();
                Debug.Log("PELI LOPPUI RÄJÄHDIT");
            }
            else
            {
                Debug.Log("PENALTYÄ , PELI EI KUITENKAAN LOPPUNUT!");
            }
            correctOrder = true;
        }
    }

 
}
