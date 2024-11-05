using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level_1_script : MonoBehaviour
{
    public GameObject EpäiltykansioCanvas;
    public GameObject[] pages;
    public AudioClip pageTurnClip;
    private int currentPage = 0;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == 0);
        }
    }

    public void OpenCanvas()
    {
        if (EpäiltykansioCanvas != null)
        {
            EpäiltykansioCanvas.SetActive(true);
            Transform panelGroup = EpäiltykansioCanvas.transform.Find("LevelGroup1");
            panelGroup.gameObject.SetActive(true);
            GameObject buttonObject = EventSystem.current.currentSelectedGameObject;
            var player = PlayerManager.Instance;
            player.SetSelectedLevel(buttonObject.name);
        }
        else
        {
            Debug.LogWarning("EpäiltykansioCanvas is not assigned in the inspector.");
        }
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            pages[currentPage].SetActive(false);
            currentPage++;
            pages[currentPage].SetActive(true);
            Debug.Log("Next page: " + currentPage);
            PlayAudio(pageTurnClip);
        }
        else
        {
            Debug.Log("Already at the last page.");
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            pages[currentPage].SetActive(false);  // Hide current page
            currentPage--;
            pages[currentPage].SetActive(true);   // Show previous page
            Debug.Log("Previous page: " + currentPage);
            PlayAudio(pageTurnClip);
        }
        else
        {
            Debug.Log("Already at the first page.");
        }
    }

    public void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}