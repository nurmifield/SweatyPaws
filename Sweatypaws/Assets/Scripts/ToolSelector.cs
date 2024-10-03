using UnityEngine;
using UnityEngine.UI;

public class ToolSelector : MonoBehaviour
{
    public Button ScrewdriverButton;
    public Button PliersButton;
    public Button TweezerButton;
    public Button HandButton;
    public Button LiquidNitrogenButton;

    void Start()
    {
        // Add listeners to detect when a button is pressed
        PliersButton.onClick.AddListener(() => SelectTool("Pliers"));
        ScrewdriverButton.onClick.AddListener(() => SelectTool("Screwdriver"));
        TweezerButton.onClick.AddListener(() => SelectTool("Tweezer"));
        HandButton.onClick.AddListener(() => SelectTool("Paw"));
        LiquidNitrogenButton.onClick.AddListener(() => SelectTool("Liquid Nitrogen"));
    }

    void SelectTool(string toolName)
    {
        string timestamp = System.DateTime.Now.ToString("HH:mm:ss.fff");
        Debug.Log("Selected tool: " + toolName + " at " + timestamp);
    }
}
