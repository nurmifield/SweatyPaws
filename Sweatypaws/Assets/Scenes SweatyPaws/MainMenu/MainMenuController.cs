using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;     // Reference to the Main Menu Panel
    public GameObject settingsPanel;     // Reference to the Settings Panel
    public GameObject collectionPanel;   // Reference to the Collection Panel
    public GameObject creditsPanel;      // Reference to the Credits Panel
    public GameObject confirmationPanel;
    public GameObject achievementsPanel;
    public GameObject fredInfoCanvas;
    public GameObject controlsPanel;
    public GameObject fredPage1;
    public GameObject fredPage2;
     
    public Text soundButtonText;         // Reference to the Text component of the Sound Button

    private bool isSoundOn = true;       // Variable to track the sound state

    void Start()
    {
        // Start with only the main menu panel active
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
        collectionPanel.SetActive(false);
        creditsPanel.SetActive(false);
        
        var player = PlayerManager.Instance;
        player.GetExistingUser();

        // Initialize the sound button text based on the sound state
        UpdateSoundButtonText();
    }

    // Load the game scene to start the game
    public void PlayGame()
    {
        SceneManager.LoadScene("Map");  // Replace with your actual game scene name
    }

    // Show the Settings Panel
    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OpenControls()
    {
        settingsPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void CloseControls()
    {
        controlsPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // Show the Collection Panel
    public void OpenCollection()
    {
        mainMenuPanel.SetActive(false);
        collectionPanel.SetActive(true);
        achievementsPanel.SetActive(false);
    }

    // Show the Credits Panel
    public void OpenCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void OpenAchievements()
    {
        collectionPanel.SetActive(false);
        achievementsPanel.SetActive(true);
    }

    public void OpenFredPanel()
    {
        collectionPanel.SetActive(false);
        fredInfoCanvas.SetActive(true);
        fredPage2.SetActive(false);
    }

    public void NextPage()
    {
        fredPage1.SetActive(false);
        fredPage2.SetActive(true);
    }

    public void PreviousPage()
    {
        fredPage2.SetActive(false);
        fredPage1.SetActive(true);
    }

    public void CloseFredPanel()
    {
        collectionPanel.SetActive(true);
        fredInfoCanvas.SetActive(false);
    }

    // Go back to the Main Menu from any other panel
    public void BackToMainMenu()
    {
        confirmationPanel.SetActive(false);
        settingsPanel.SetActive(false);
        collectionPanel.SetActive(false);
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // Toggle sound on or off and update the button text
    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        AudioListener.volume = isSoundOn ? 1 : 0;  // Set volume based on the sound state
         Debug.Log("Sound Toggled: " + (isSoundOn ? "On" : "Off"));  // Debug message
        UpdateSoundButtonText();  // Update the button text
    }

    // Update the text on the Sound Toggle Button
    private void UpdateSoundButtonText()
    {
        soundButtonText.text = isSoundOn ? "X" : "";  // "X" for sound on, blank for sound off
    }

// Method to exit the game
    public void ExitGame()
    {
        Debug.Log("Game is exiting...");  // This will appear in the console for testing in the Unity Editor

        Application.Quit();  // Quit the game (this works in the build, not in the editor)
    }



}
