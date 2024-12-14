using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenaltyManager : MonoBehaviour
{
    public Score score;
    public Timer time;
    
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
            time.DecreaseTime();
            Debug.Log("Minus Time");
        }
        else if (penalty == "faster_time")
        {
            //Tehtävä: Ajan vähennys
            time.SetTimerMultiplier();
            Debug.Log("Time goes faster");
        }
    }
}
