using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject optionsPanel;  // Reference to the Options Panel
    public GameObject menuPanel;     // Reference to the Main Menu Panel

    void Start()
    {
        // Ensure the game starts unpaused
        Time.timeScale = 1f;
    }

    // Continue the game (hide the menu)
    public void ContinueGame()
    {
        menuPanel.SetActive(false);  // Hide the main menu
        Time.timeScale = 1f;         // Resume the game
    }

    // Open the options menu
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);  // Show the options menu
     //   menuPanel.SetActive(false);    // Hide the main menu
    }

    // Go back to the main menu from options
    public void BackToMenu()
    {
        optionsPanel.SetActive(false); // Hide the options panel
        menuPanel.SetActive(true);     // Show the main menu
    }

    // Toggle sound (replace with your actual sound management code)
    public void ToggleSound()
    {
        Debug.Log("Sound toggled");
    }

    // Toggle music (replace with your actual music management code)
    public void ToggleMusic()
    {
        Debug.Log("Music toggled");
    }
}
