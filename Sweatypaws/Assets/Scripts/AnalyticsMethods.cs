using Unity.Services.Core;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AnalyticsMethods : MonoBehaviour
{
    public void LevelStartButtonPressEvent()
    {
       var player = PlayerManager.Instance;

        LevelStart levelStart = new LevelStart
        {
            level = player.GetSelectedLevel(),
           
        };


        // Send custom event to Unity Analytics
        AnalyticsService.Instance.RecordEvent(levelStart);
        //AnalyticsService.Instance.Flush(); // Immediate send for testing

        // Debug log to confirm
        Debug.Log("Analytics event sent:" + player.GetSelectedLevel());
    }

    public void CheckLevelCompletion(bool completed, float time)
    {
        var player = PlayerManager.Instance;

        LevelCompleted levelCompleted = new LevelCompleted
        {
            level = player.GetSelectedLevel(),
            levelCompleted=completed,
            timeUsedOnManual = time
        };


        // Send custom event to Unity Analytics
        AnalyticsService.Instance.RecordEvent(levelCompleted);
        //AnalyticsService.Instance.Flush(); // Immediate send for testing

        // Debug log to confirm
        Debug.Log("Analytics event sent:" + player.GetSelectedLevel() + " / " + completed + " / " + time);
    }
}
