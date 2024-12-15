using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ManualController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip closeManualClip;
    public AudioClip pageTurnClip;
    public GameObject[] pages;         
    private int currentPage = 0;       

    public GameObject manualPanel;     
    public GameObject bookCoverPanel;  

    
    public GameObject scoreText_A;
    public GameObject scoreText_B;       
    public GameObject toolsButtons;    
    public GameObject settingsButton;  
    public GameObject manualButton;    
    public float startTime;
    public float endTime;
    public ManualTimeUsed manualTimeUsed;
    public Timer timer;
    private ToolSelector toolSelector;

    void Start()
    {
        toolSelector = FindObjectOfType<ToolSelector>();
        
        audioSource = GetComponent<AudioSource>();
        //Debug.Log("ManualController started. Ensure the book cover shows when opening the manual.");
    }

    
    public void ShowBookCover()
    {
        //Debug.Log("Opening book cover panel.");
        bookCoverPanel.SetActive(true);    
        manualPanel.SetActive(false);              
        toolsButtons.SetActive(false);     
        settingsButton.SetActive(false);   
        manualButton.SetActive(false);
        timer.UpdateTimerPosition(true);     
        startTime = Time.time;
        //Debug.Log(Time.time);

        if (SceneManager.GetActiveScene().name == "Game")
        {
            GetComponent<Player>().EquipTool("none");
        }
    }

    
    public void ContinueGame()
    {
        //Debug.Log("Continuing game from the book cover.");
        bookCoverPanel.SetActive(false);   
        toolsButtons.SetActive(true);      
        settingsButton.SetActive(true);    
        manualButton.SetActive(true);      
        Time.timeScale = 1f;
        
        timer.UpdateTimerPosition(false);               
    }

    public void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    
    public void OpenManual()
    {
        //Debug.Log("Opening manual (book cover panel first).");
        currentPage = 0;                  
        bookCoverPanel.SetActive(false);    
        manualPanel.SetActive(true);     
        toolsButtons.SetActive(false);     
        settingsButton.SetActive(false);   
        manualButton.SetActive(false);
        timer.UpdateTimerPosition(true);     
    }

    
    public void OpenFirstPageFromCover()
    {
        //Debug.Log("Opening the first manual page.");

        PlayAudio(pageTurnClip);
        currentPage = 0;                   
        bookCoverPanel.SetActive(false);   
        manualPanel.SetActive(true);       
        UpdatePage();                      
    }

    
    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            PlayAudio(pageTurnClip);
            //Debug.Log("Next page: " + currentPage);
            UpdatePage();
        }
    }

    
    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            PlayAudio(pageTurnClip);
            //Debug.Log("Previous page: " + currentPage);
            UpdatePage();
        }
    }

    
    private void UpdatePage()
    { var player=PlayerManager.Instance;
        //Debug.Log("Updating page: " + currentPage);
        if (currentPage == 0) {
            pages[0].SetActive(true);
            for (int i = 0; i < pages.Length;i++)
            {
                if (i > 0  )
                {
                    pages[i].SetActive(false);
                }
            }
        }
        else if (currentPage==1)
        {
            pages[0].SetActive(false);
            for (int i = 0; i< pages.Length; i++)
            {
                if (player.GetSelectedLevel() == pages[i].name)
                {
                    pages[i].SetActive(true);
                }
                else
                {
                    pages[i].SetActive(false);
                }
            }
        }
        else
        {
            pages[0].SetActive(false);
        }
      
    }

    
    public void CloseManual()
    {
        if (toolSelector != null && toolSelector.currentSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            toolSelector.currentSelectedButton = null;
        }

        //Debug.Log("Closing manual and resuming game.");
        PlayAudio(closeManualClip);
        manualPanel.SetActive(false);      
        bookCoverPanel.SetActive(false);   
        toolsButtons.SetActive(true);      
        settingsButton.SetActive(true);    
        manualButton.SetActive(true);
        timer.UpdateTimerPosition(false);      
        Time.timeScale = 1f;               
        endTime = Time.time;
        manualTimeUsed.CalculateTime(startTime,endTime);
        //Debug.Log(Time.time);

        
    }
}
