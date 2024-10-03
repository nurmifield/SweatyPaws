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
            // Pelin lopetusta t�nne
            if(sectionsCleared < MustBeCleared)
            {
                Debug.Log("PELI LOPPUI R�J�HDIT");
            }
            else
            {
                Debug.Log("PENALTY� , PELI EI KUITENKAAN LOPPUNUT!");
            }
            correctOrder = true;
        }
    }
}
