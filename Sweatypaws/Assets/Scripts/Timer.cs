using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{   
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI timerText_1;
    [SerializeField] float remainingTime;
    public GameObject menupanel;
    public GameObject manualPanel;
    public GameOverScreen gameOverObject;
    public GameObject youWinObject;
    public GameObject GameOverScreen;
    public bool gameEnds = false;
    public GameObject tutorialPanel;
    public float timeMultiplier = 1f;

    // Update is called once per frame
    void Update()
    {
        if (menupanel.activeSelf || gameEnds)
        {
            return;
        }
        if (tutorialPanel.activeSelf) return;
    
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime * timeMultiplier;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            // GameOver();
            timerText.color = Color.red;
            timerText_1.color = Color.red;
            gameOverObject.GameOverScreenManage();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        timerText_1.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void Stoptimer(bool newgameEnds)
    {
        gameEnds = newgameEnds;
    }
    public void SetTimerMultiplier()
    {
        this.timeMultiplier = timeMultiplier * 2;
    }

    public void DecreaseTime()
    {
        this.remainingTime -= 20;
    }

}
