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
    public GameObject[] pages;         // Array to store all pages of the manual
    private int currentPage = 0;       // Index to track the current page

    public GameObject manualPanel;     // Reference to the manual panel (containing all pages)
    public GameObject bookCoverPanel;  // Reference to the book cover panel

    // References for UI elements to hide
    public GameObject scoreText_A;
    public GameObject scoreText_B;       // Reference to the score text UI
    public GameObject toolsButtons;    // Reference to the tools buttons panel
    public GameObject settingsButton;  // Reference to the settings button
    public GameObject manualButton;    // Reference to the manual button
    public float startTime;
    public float endTime;
    public ManualTimeUsed manualTimeUsed;
    public Timer timer;
    private ToolSelector toolSelector;

    void Start()
    {
        toolSelector = FindObjectOfType<ToolSelector>();
        // Ensure the game starts on the book cover screen
        audioSource = GetComponent<AudioSource>();
        Debug.Log("ManualController started. Ensure the book cover shows when opening the manual.");
    }

    // Show the book cover panel and hide everything else
    public void ShowBookCover()
    {
        Debug.Log("Opening book cover panel.");
        bookCoverPanel.SetActive(true);    // Show the book cover panel
        manualPanel.SetActive(false);      // Hide the manual panel        
        toolsButtons.SetActive(false);     // Hide tools buttons panel
        settingsButton.SetActive(false);   // Hide settings button
        manualButton.SetActive(false);
        timer.UpdateTimerPosition(true);     // Hide manual button
        startTime = Time.time;
        Debug.Log(Time.time);

        if (SceneManager.GetActiveScene().name == "Game")
        {
            GetComponent<Player>().EquipTool("none");
        }
    }

    // Continue the game (hide the book cover)
    public void ContinueGame()
    {
        Debug.Log("Continuing game from the book cover.");
        bookCoverPanel.SetActive(false);   // Hide the book cover          // Show score text
        toolsButtons.SetActive(true);      // Show tools buttons panel
        settingsButton.SetActive(true);    // Show settings button
        manualButton.SetActive(true);      // Show manual button
        Time.timeScale = 1f;
        
        timer.UpdateTimerPosition(false);               // Resume the game
    }

    public void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    // Open the manual (starting from the first page)
    public void OpenManual()
    {
        Debug.Log("Opening manual (book cover panel first).");
        currentPage = 0;                  // Reset to the first page
        bookCoverPanel.SetActive(false);    // Show the book cover panel
        manualPanel.SetActive(true);      // Hide the manual panel until book cover is closed
        toolsButtons.SetActive(false);     // Hide tools buttons panel
        settingsButton.SetActive(false);   // Hide settings button
        manualButton.SetActive(false);
        timer.UpdateTimerPosition(true);     // Hide manual button
    }

    // Go to the first page of the manual from the book cover
    public void OpenFirstPageFromCover()
    {
        Debug.Log("Opening the first manual page.");

        PlayAudio(pageTurnClip);
        currentPage = 0;                   // Reset to the first page
        bookCoverPanel.SetActive(false);   // Hide the book cover
        manualPanel.SetActive(true);       // Show the manual panel with pages
        UpdatePage();                      // Show the first page
    }

    // Go to the next page
    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            PlayAudio(pageTurnClip);
            Debug.Log("Next page: " + currentPage);
            UpdatePage();
        }
    }

    // Go to the previous page
    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            PlayAudio(pageTurnClip);
            Debug.Log("Previous page: " + currentPage);
            UpdatePage();
        }
    }

    // Update the page display
    private void UpdatePage()
    { var player=PlayerManager.Instance;
        Debug.Log("Updating page: " + currentPage);
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

    // Close the manual and go back to the game
    public void CloseManual()
    {
        if (toolSelector != null && toolSelector.currentSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            toolSelector.currentSelectedButton = null;
        }

        Debug.Log("Closing manual and resuming game.");
        PlayAudio(closeManualClip);
        manualPanel.SetActive(false);      // Hide the manual panel
        bookCoverPanel.SetActive(false);   // Hide the book cover
        toolsButtons.SetActive(true);      // Show tools buttons panel
        settingsButton.SetActive(true);    // Show settings button
        manualButton.SetActive(true);
        timer.UpdateTimerPosition(false);      // Show manual button
        Time.timeScale = 1f;               // Resume the game
        endTime = Time.time;
        manualTimeUsed.CalculateTime(startTime,endTime);
        Debug.Log(Time.time);

        
    }
}
