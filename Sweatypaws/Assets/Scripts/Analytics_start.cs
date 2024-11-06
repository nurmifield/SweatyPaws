using Unity.Services.Core;
using Unity.Services.Analytics;
using UnityEngine;

public class UnityServicesInitializer : MonoBehaviour
{
    async void Start()
    {
        try
        {
            // Initialize Unity Services
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services Initialized Successfully");

            // Start Analytics data collection
            AnalyticsService.Instance.StartDataCollection();
            Debug.Log("Analytics data collection started");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to initialize Unity Services: {e.Message}");
        }
    }
}
