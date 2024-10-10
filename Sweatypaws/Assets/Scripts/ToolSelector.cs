using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolSelector : MonoBehaviour
{
    public Button ScrewdriverButton;
    public Button PliersButton;
    public Button TweezerButton;
    public Button HandButton;
    public Button LiquidNitrogenButton;
    private Button currentSelectedButton;
    public bool Highlight = true;

    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();

        PliersButton.onClick.AddListener(() => SelectTool(PliersButton, "pihdit"));
        ScrewdriverButton.onClick.AddListener(() => SelectTool(ScrewdriverButton, "Screwdriver"));
        TweezerButton.onClick.AddListener(() => SelectTool(TweezerButton, "Tweezer"));
        HandButton.onClick.AddListener(() => SelectTool(HandButton, "tassu"));
        LiquidNitrogenButton.onClick.AddListener(() => SelectTool(LiquidNitrogenButton, "Liquid Nitrogen"));
    }

    void Update()
    {
        if (currentSelectedButton != null && EventSystem.current.currentSelectedGameObject != currentSelectedButton.gameObject)
        {
            EventSystem.current.SetSelectedGameObject(currentSelectedButton.gameObject);
        }
    }

    void SelectTool(Button button, string toolName)
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

        HighlightButton(button);
    }

    private void HighlightButton(Button button)
    {
        currentSelectedButton = button;
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
}
