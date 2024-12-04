using UnityEngine;
using UnityEngine.UI;  // For handling UI components
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
//using UnityEditor.Timeline.Actions;
using UnityEditor;

public class MenuController : MonoBehaviour
{
    public GameObject ToolButtons;
    public GameObject ManualButton;
    public GameObject optionsPanel;  // Reference to the Options Panel
    public GameObject menuPanel;
    public GameObject MainMenuButton;     // Reference to the Main Menu Panel
    public Text soundButtonText;     // Reference to the Text component of the sound button
    private bool isSoundOn = true;
    private ToolSelector toolSelector;   // Variable to track the sound state

    void Start()
    {
        toolSelector = FindObjectOfType<ToolSelector>();
        
        // Ensure the game starts unpaused and only the menu panel is active
        Time.timeScale = 1f;
        if (menuPanel != null && optionsPanel != null)
        {
            menuPanel.SetActive(false);   // Show the main menu at the start
            optionsPanel.SetActive(false); // Hide the options panel at the start
        }
        soundButtonText.text = PlayerManager.Instance.IsSoundOn ? "X" : "";
    }

    // Continue the game (hide the menu)
    public void ContinueGame()
    {
        if (toolSelector != null && toolSelector.currentSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            toolSelector.currentSelectedButton = null;
        }

        menuPanel.SetActive(false);
        optionsPanel.SetActive(false);    // Hide the main menu
        ToolButtons.SetActive(true);   // Show the tool buttons
        ManualButton.SetActive(true);  // Show the manual button 
        Time.timeScale = 1f;         // Resume the game
    }

    // Open the options menu
    public void OpenOptions()
    {
        menuPanel.SetActive(false);
        optionsPanel.SetActive(true);

        if (optionsPanel.activeSelf)
        {
            MainMenuButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            MainMenuButton.GetComponent<Button>().interactable = true;
        }
    }
    public void OpenSettings()
    {
        menuPanel.SetActive(true);
        ToolButtons.SetActive(false);
        ManualButton.SetActive(false);
        optionsPanel.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Game")
        {
            GetComponent<Player>().EquipTool("none");
        }
    }
    // Go back to the main menu from options
    public void BackToMenu()
    {
        optionsPanel.SetActive(false); // Hide the options panel
        menuPanel.SetActive(true);
        MainMenuButton.GetComponent<Button>().interactable = true;
    }

    // Toggle sound (this toggles sound on/off and updates the button text)
    public void ToggleSound()
    {
        PlayerManager.Instance.ToggleSound();
        soundButtonText.text = PlayerManager.Instance.IsSoundOn ? "X" : "";
    }



    // Toggle music (replace with your actual music management code)
    public void ToggleMusic()
    {
        Debug.Log("Music toggled");
    }


    public void MainMenuScene()
    {
        var player = PlayerManager.Instance;
        player.SetSelectedLevel("none");
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenSettingsMap()
    {
        menuPanel.SetActive(true);
    }

    public void BackToMap()
    {
        menuPanel.SetActive(false);
    }

}
