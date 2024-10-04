using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFailure : MonoBehaviour
{
    
    public bool correctOrder = true;
    public int sectionsCleared = 0;
    public int MustBeCleared = 1;


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
            Debug.Log("PENALTYÄ , PELI EI KUITENKAAN LOPPUNUT!");
        }


    }

 
}
