using Unity.Services.Core;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AnalyticsMethods : MonoBehaviour
{
    public void LevelStartButtonPressEvent()
    {
        var eventParameters = new Dictionary<string, object>
        {
            { "button_used", "example_button"},
            { "timestamp2", Time.time }
        };

        LevelStart levelStart = new LevelStart
        {
            Level = "level1"
        };


        // Send custom event to Unity Analytics
        AnalyticsService.Instance.RecordEvent(levelStart);
        //AnalyticsService.Instance.Flush(); // Immediate send for testing

        // Debug log to confirm
        Debug.Log("Analytics event sent: button_used");
    }
}
