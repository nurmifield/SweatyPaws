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
    public Button ManualButton;
    private Button currentSelectedButton;
    public GameObject manualPanel;
    public bool Highlight = true;

    public AudioClip [] buttonSounds;
    private AudioSource audioSource;

    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        audioSource = gameObject.AddComponent<AudioSource>();

        PliersButton.onClick.AddListener(() => SelectTool(PliersButton, "pihdit", 0));
        ScrewdriverButton.onClick.AddListener(() => SelectTool(ScrewdriverButton, "Screwdriver", 1));
        TweezerButton.onClick.AddListener(() => SelectTool(TweezerButton, "Tweezer", 2));
        HandButton.onClick.AddListener(() => SelectTool(HandButton, "tassu", 3));
        LiquidNitrogenButton.onClick.AddListener(() => SelectTool(LiquidNitrogenButton, "Liquid Nitrogen", 4));
    }   

    void Update()
    {
        if (currentSelectedButton != null && EventSystem.current.currentSelectedGameObject != currentSelectedButton.gameObject)
        {
            EventSystem.current.SetSelectedGameObject(currentSelectedButton.gameObject);
        }
    }

    void SelectTool(Button button, string toolName, int soundIndex)
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
        PlaySound(soundIndex);
    }

    private void PlaySound(int index)
    {
        if (buttonSounds != null && index < buttonSounds.Length && buttonSounds[index] != null)
        {
            audioSource.PlayOneShot(buttonSounds[index]);
        }
        else
        {
            Debug.LogWarning("Sound index " + index + " is out of range or not assigned.");
        }
    }

    private void HighlightButton(Button button)
    {
        currentSelectedButton = button;
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
}
