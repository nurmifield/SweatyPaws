using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonManager : MonoBehaviour
{
    public Button ContinueButton;
    public Button NewGameButton;
    public Button yesButton;
    public Button noButton;
    public MainMenuController mainMenuController;
    public GameObject confirmationPanel;
    private PlayerManager playerManager;

    void Start()
    {
        playerManager = PlayerManager.Instance;

        if (playerManager.GetExistingUser())
        {
            ContinueButton.interactable = true;
            NewGameButton.onClick.AddListener(OpenConfirmationPanel);
        }
        else
        {
            ContinueButton.interactable = false;
            NewGameButton.onClick.AddListener(ContinueGame);
        }

        ContinueButton.onClick.AddListener(ContinueGame);
        
    }

    public void ContinueGame()
    {
        string levelToLoad = playerManager.GetSelectedLevel();

        if (string.IsNullOrEmpty(levelToLoad) || levelToLoad == "none")
        {
            levelToLoad = "Map";
        }
        SceneManager.LoadScene(levelToLoad);
    }

    public void OpenConfirmationPanel()
    {
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();
        
        if (!playerManager.GetExistingUser())
        {
            
            StartNewGame();
            return;
        }

        confirmationPanel.SetActive(true);

        yesButton.onClick.AddListener(() =>
        {
            StartNewGame();
        });

        noButton.onClick.AddListener(() =>
        {
            mainMenuController.BackToMainMenu();
        });
    }

    public void StartNewGame()
    {
        playerManager.NewGame();
    }
}