using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // For loading scenes
using UnityEngine.UI; // For working with UI components

public class MenuController : MonoBehaviour
{
    public GameObject optionsPanel; // Drag your options panel here in the Inspector

    // Function to load the main menu
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Replace with the name of your main menu scene
    }

    // Function to open the options panel
    public void OpenOptions()
    {
        optionsPanel.SetActive(true); // Show the options panel
    }

    // Function to close the options panel or continue the game
    public void CloseOptions()
    {
        optionsPanel.SetActive(false); // Hide the options panel
    }
}
