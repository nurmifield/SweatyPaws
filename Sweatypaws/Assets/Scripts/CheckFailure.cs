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
            // T�ST� MENN��N GAME OVER N�KYM��N
            GetComponent<GameOverScreen>().GameOverScreenManage();
            Debug.Log("PELI LOPPUI R�J�HDIT");
        }
        else
        {
            // PISTEEN V�HENNYST� T�NNE
            Debug.Log("PENALTY� , PELI EI KUITENKAAN LOPPUNUT!");
        }


    }

 
}
