using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;
    public int minusScore = 5;
    public int addScore = 10;

    public Text scoreText;
    // Start is called before the first frame update
    public void AddScore()
    {
        score += addScore;
        scoreText.text = "Score: " + score.ToString();
    }

    public void MinusScore()
    {

        score -= minusScore;

        if (score < 0)
        {
            score = 0;
        }
        scoreText.text ="Score: " +  score.ToString();
    }
    public int GetScore()
    {
        return score;
    }
}
