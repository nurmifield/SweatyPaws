using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public GameObject loadingScreen;
    public Animator animator;
    public void LoadScene(string sceneName)
    {

        SceneManager.LoadSceneAsync(sceneName);
        
    }
    public void  PlayTimeLine()
    {
        loadingScreen.SetActive(true);
        Transform loading = loadingScreen.transform.Find("LoadingScreenImages");
        GameObject timeLineObject = loading.gameObject;
        timeLineObject.GetComponent<PlayableDirector>().Play();
        animator.SetBool("Play", true);
    }

}
