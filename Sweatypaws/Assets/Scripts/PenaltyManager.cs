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
            //Tehtävä: Aikaan liittyvä nopeennus
            Debug.Log("Minus Time");
        }
        else if (penalty == "faster_time")
        {
            //Tehtävä: Ajan vähennys
            Debug.Log("Time goes faster");
        }
    }
}
