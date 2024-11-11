using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonManager : MonoBehaviour
{
    public Button ContinueButton;
    public Button NewGameButton;
    private PlayerManager playerManager;

    void Start()
    {
        playerManager = PlayerManager.Instance;

        if (playerManager.GetExistingUser())
        {
            ContinueButton.interactable = true;
        }
        else
        {
            ContinueButton.interactable = false;
        }
        ContinueButton.onClick.AddListener(ContinueGame);
        NewGameButton.onClick.AddListener(StartNewGame);
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

    public void StartNewGame()
    {
        playerManager.NewGame();
    }
}