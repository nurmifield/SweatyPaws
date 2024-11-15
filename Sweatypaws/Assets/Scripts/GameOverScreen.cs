using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public AudioClip explosionClip;
    public AudioClip WonkyWinClip;
    public ManualController manualController;
    public GameObject gameOverObject;
    public GameObject youWinObject;
    public GameObject guiObject;
    public Timer timer;
    public Score score;
    public GameObject analyticManager;
    public ManualTimeUsed manualTimeUsed;

    void Start()
    {
        gameOverObject.SetActive(false);
        youWinObject.SetActive(false);
    }

    public void GameOverScreenManage()
    {
        timer.Stoptimer(true);
        manualController.PlayAudio(explosionClip);
        guiObject.SetActive(false);
        gameOverObject.SetActive(true);
        analyticManager.GetComponent<AnalyticsMethods>().CheckLevelCompletion(false, manualTimeUsed.overAllTimeUsed);
        Debug.Log(manualTimeUsed.overAllTimeUsed);
    }

    public void YouWinScreenManage()
    {
        manualController.PlayAudio(WonkyWinClip);
        var player = PlayerManager.Instance;
        player.LevelCompleted(player.GetSelectedLevel(),score.GetScore());
        timer.Stoptimer(true);
        guiObject.SetActive(false);
        youWinObject.SetActive(true);
        analyticManager.GetComponent<AnalyticsMethods>().CheckLevelCompletion(true,manualTimeUsed.overAllTimeUsed);
        Debug.Log(manualTimeUsed.overAllTimeUsed);
    }
    public void RestartButton()
    {
        var player = PlayerManager.Instance;
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
