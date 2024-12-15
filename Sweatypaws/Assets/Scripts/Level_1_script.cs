using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Level_1_script : MonoBehaviour
{
    public GameObject EpäiltykansioCanvas;
    public GameObject[] pages;
    public GameObject NextPageButton;
    public GameObject PreviousPageButton;
    public AudioClip pageTurnClip;
    private int currentPage = 0;
    private AudioSource audioSource;
    public UnityEngine.UI.Image firstPageImage;
    public UnityEngine.UI.Image secondPageImage;
    public bool onePageOnly = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == 0);
        }

        if  (EpäiltykansioCanvas != null)
        {
            EpäiltykansioCanvas.SetActive(false);
        }
            UpdateButtonVisibility();
    }
    public void SetCurrentPageZero() 
    {
        this.currentPage = 0;
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
            Sprite newFirstPageSprite = Resources.Load<Sprite>(buttonObject.name + "_FirstPage");
            Sprite newSecondPageSprite = Resources.Load<Sprite>(buttonObject.name + "_SecondPage");
            //Debug.Log(newSecondPageSprite);
            if (newSecondPageSprite != null)
            {
                secondPageImage.sprite = newSecondPageSprite;
                
                onePageOnly = false;
            }
            else
            {
                NextPageButton.SetActive(false);
                onePageOnly = true;
               
            }
            firstPageImage.sprite=newFirstPageSprite;
            pages[0].SetActive(true);
            pages[1].SetActive(false);
            UpdateButtonVisibility();

        }
        else
        {
            //Debug.LogWarning("EpäiltykansioCanvas is not assigned in the inspector.");
        }
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            pages[currentPage].SetActive(false);
            currentPage++;
            pages[currentPage].SetActive(true);
            //Debug.Log("Next page: " + currentPage);
            PlayAudio(pageTurnClip);
            UpdateButtonVisibility();
        }
        else
        {
            //Debug.Log("Already at the last page.");
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            pages[currentPage].SetActive(false);  // Hide current page
            currentPage--;
            pages[currentPage].SetActive(true);   // Show previous page
            //Debug.Log("Previous page: " + currentPage);
            PlayAudio(pageTurnClip);
            UpdateButtonVisibility();
        }
        else
        {
            //Debug.Log("Already at the first page.");
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

    private void UpdateButtonVisibility()
    {
        if (PreviousPageButton != null)
        {
            PreviousPageButton.SetActive(currentPage > 0);
        }

        if (NextPageButton != null && !onePageOnly)
        {
            NextPageButton.SetActive(currentPage < pages.Length - 1);
        }
    }
}