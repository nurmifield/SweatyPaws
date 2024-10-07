using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverObject;
    public GameObject youWinObject;

    void Start()
    {
        gameOverObject.SetActive(false);
        youWinObject.SetActive(false);
    }

    public void GameOverScreenManage()
    {
      
        gameOverObject.SetActive(true);
    }

    public void YouWinScreenManage()
    {

        youWinObject.SetActive(true);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
