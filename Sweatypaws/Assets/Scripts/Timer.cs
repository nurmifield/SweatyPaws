using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
    public RectTransform timerUI;
    public UnityEngine.Vector2 vector2;
    public UnityEngine.Vector2 vector2_default;

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

            GameObject monitorScreen = GameObject.Find("ScreenObject");
            if (monitorScreen != null && monitorScreen.activeSelf)
            {
                monitorScreen.SetActive(false);
            }

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
        this.timeMultiplier = timeMultiplier * 5;
    }

    public void DecreaseTime()
    {
        this.remainingTime -= 20;
    }

    public void SetTimer(int time)
    {
        this.remainingTime += time;
    }

    public void UpdateTimerPosition(bool isManualOpen)
    {
        if (isManualOpen)
        {
            timerUI.anchoredPosition = vector2;
        }

        if (!isManualOpen)
        {
            timerUI.anchoredPosition = vector2_default;
        }
    }
}
