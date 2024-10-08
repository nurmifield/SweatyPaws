using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverObject;
    public GameObject youWinObject;
    public GameObject guiObject;

    void Start()
    {
        gameOverObject.SetActive(false);
        youWinObject.SetActive(false);
    }

    public void GameOverScreenManage()
    {
        
        guiObject.SetActive(false);
        gameOverObject.SetActive(true);
    }

    public void YouWinScreenManage()
    {
        guiObject.SetActive(false);
        youWinObject.SetActive(true);
    }
    public void RestartButton()
    {
        StartCoroutine(Wait(1f));
        SceneManager.LoadScene("Game");
    }
    public void MainMenuButton()
    {
        StartCoroutine(Wait(1f));
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

}
