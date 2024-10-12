using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;
    public int minusScore = 5;
    public int addScore = 10;

    public Text scoreText;
    public Text youWinScoreText;
    public GameObject floatingScore;
    // Start is called before the first frame update
    public void AddScore()
    {
        score += addScore;
        scoreText.text = "Score: " + score.ToString();
        youWinScoreText.text = "Score: " + score.ToString();
        GameObject popUpScoreText = Instantiate(floatingScore, new Vector2(720, 2041), Quaternion.identity,GetComponent<Canvas>().transform);
        popUpScoreText.GetComponent<TextMeshProUGUI>().text = "+" + addScore.ToString();
        Destroy(popUpScoreText, 1f);
    }

    public void MinusScore()
    {

        score -= minusScore;

        if (score < 0)
        {
            score = 0;
        }
        scoreText.text ="Score: " +  score.ToString();
        youWinScoreText.text = "Score: " + score.ToString();
        GameObject popUpScoreText = Instantiate(floatingScore, new Vector2(720, 2041), Quaternion.identity, GetComponent<Canvas>().transform);
        Text popUpScore = popUpScoreText.GetComponent<Text>();
        popUpScoreText.GetComponent<TextMeshProUGUI>().text = "-"+minusScore.ToString();
        Destroy(popUpScoreText, 1f);
    }
    public int GetScore()
    {
        return score;
    }
}
