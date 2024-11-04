using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameOverObject;
    public GameObject youWinObject;
    public GameObject guiObject;
    public Timer timer;
    public Score score;

    void Start()
    {
        gameOverObject.SetActive(false);
        youWinObject.SetActive(false);
    }

    public void GameOverScreenManage()
    {
        timer.Stoptimer(true);
        guiObject.SetActive(false);
        gameOverObject.SetActive(true);
    }

    public void YouWinScreenManage()
    {
        var player = PlayerManager.Instance;
        player.LevelCompleted(player.GetSelectedLevel(),score.GetScore());
        timer.Stoptimer(true);
        guiObject.SetActive(false);
        youWinObject.SetActive(true);
    }
    public void RestartButton()
    {
        StartCoroutine(Wait(1f));
        SceneManager.LoadScene("Game");
    }
    public void LevelSelectioButton()
    {
        var player = PlayerManager.Instance;
        player.SetSelectedLevel("none");
        StartCoroutine(Wait(1f));
        SceneManager.LoadScene("Map");
    }

    public IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

}
