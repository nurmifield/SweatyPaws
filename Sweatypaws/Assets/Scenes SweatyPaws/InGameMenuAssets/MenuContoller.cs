using UnityEngine;
using UnityEngine.UI;  // For handling UI components
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject ToolButtons;
    public GameObject ManualButton;
    public GameObject optionsPanel;  // Reference to the Options Panel
    public GameObject menuPanel;     // Reference to the Main Menu Panel
    public Text soundButtonText;     // Reference to the Text component of the sound button
    private bool isSoundOn = true;   // Variable to track the sound state

    void Start()
    {
        // Ensure the game starts unpaused and only the menu panel is active
        Time.timeScale = 1f;
        menuPanel.SetActive(false);   // Show the main menu at the start
        optionsPanel.SetActive(false); // Hide the options panel at the start
    }

    // Continue the game (hide the menu)
    public void ContinueGame()
    {
        menuPanel.SetActive(false);    // Hide the main menu
        ToolButtons.SetActive(true);   // Show the tool buttons
        ManualButton.SetActive(true);  // Show the manual button 
        Time.timeScale = 1f;         // Resume the game
    }

    // Open the options menu
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);  // Show the options menu
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
        menuPanel.SetActive(true);     // Show the main menu
    }

    // Toggle sound (this toggles sound on/off and updates the button text)
    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;  // Toggle the sound state

        // Enable or disable sound based on the current state
        AudioListener.volume = isSoundOn ? 1 : 0;

        // Update the button text based on the sound state
        soundButtonText.text = isSoundOn ? "X" : "";  // "X" for sound on, blank for sound off
    }



    // Toggle music (replace with your actual music management code)
    public void ToggleMusic()
    {
        Debug.Log("Music toggled");
    }


    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
