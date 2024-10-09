using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{   
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    public GameObject menupanel;
    // Update is called once per frame
    void Update()
    {
        if (menupanel.activeSelf)
        {
            return;
        }

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            // GameOver();
            timerText.color = Color.red;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
