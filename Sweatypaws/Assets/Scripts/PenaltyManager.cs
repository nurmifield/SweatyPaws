using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaltyManager : MonoBehaviour
{
    public Score score;
    
    public void CheckPenalty(string penalty)
    {
        if (penalty == "fail")
        {
            GetComponent<GameOverScreen>().GameOverScreenManage();
            Debug.Log("Failure");
        }
        else if (penalty == "points")
        {
            score.MinusScore();
            Debug.Log("Minus points");
        }
        else if (penalty == "time")
        {
            //Teht�v�: Aikaan liittyv� nopeennus
            Debug.Log("Minus Time");
        }
        else if (penalty == "faster_time")
        {
            //Teht�v�: Ajan v�hennys
            Debug.Log("Time goes faster");
        }
    }
}
