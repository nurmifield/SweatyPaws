using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverObject;

    void Start()
    {
        gameOverObject.SetActive(false);
    }

    public void GameOverScreenManage()
    {
      
        gameOverObject.SetActive(true);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }

}
