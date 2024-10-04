using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ToolSelector : MonoBehaviour
{
    public Button ScrewdriverButton;
    public Button PliersButton;
    public Button TweezerButton;
    public Button HandButton;
    public Button LiquidNitrogenButton;

    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();

        // Add listeners to detect when a button is pressed
        PliersButton.onClick.AddListener(() => SelectTool("pihdit"));
        ScrewdriverButton.onClick.AddListener(() => SelectTool("Screwdriver"));
        TweezerButton.onClick.AddListener(() => SelectTool("Tweezer"));
        HandButton.onClick.AddListener(() => SelectTool("tassu"));
        LiquidNitrogenButton.onClick.AddListener(() => SelectTool("Liquid Nitrogen"));
    }

    void SelectTool(string toolName)
    {
        string timestamp = System.DateTime.Now.ToString("HH:mm:ss.fff");
        Debug.Log("Selected tool: " + toolName + " at " + timestamp);

        if (player != null)
        {
            player.EquipTool(toolName);
        }
        else
        {
            Debug.LogError("Player Not Found!");
        }
    }
}
