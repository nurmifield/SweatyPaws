using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualController : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject[] pages;         // Array to store all pages of the manual
    private int currentPage = 0;       // Index to track the current page

    public GameObject manualPanel;     // Reference to the manual panel (containing all pages)
    public GameObject bookCoverPanel;  // Reference to the book cover panel

    // References for UI elements to hide
    public GameObject scoreText;       // Reference to the score text UI
    public GameObject toolsButtons;    // Reference to the tools buttons panel
    public GameObject settingsButton;  // Reference to the settings button
    public GameObject manualButton;    // Reference to the manual button

    void Start()
    {
        // Ensure the game starts on the book cover screen
        Debug.Log("ManualController started. Ensure the book cover shows when opening the manual.");
    }

    // Show the book cover panel and hide everything else
    public void ShowBookCover()
    {
        Debug.Log("Opening book cover panel.");
        bookCoverPanel.SetActive(true);    // Show the book cover panel
        manualPanel.SetActive(false);      // Hide the manual panel
        scoreText.SetActive(false);        // Hide score text
        toolsButtons.SetActive(false);     // Hide tools buttons panel
        settingsButton.SetActive(false);   // Hide settings button
        manualButton.SetActive(false);     // Hide manual button


    }

    // Continue the game (hide the book cover)
    public void ContinueGame()
    {
        Debug.Log("Continuing game from the book cover.");
        bookCoverPanel.SetActive(false);   // Hide the book cover
        scoreText.SetActive(true);         // Show score text
        toolsButtons.SetActive(true);      // Show tools buttons panel
        settingsButton.SetActive(true);    // Show settings button
        manualButton.SetActive(true);      // Show manual button
        Time.timeScale = 1f;               // Resume the game
    }

    public void AudioOpen()
    {
        if (audioSource != null)
        {
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
        scoreText.SetActive(false);        // Hide score text
        toolsButtons.SetActive(false);     // Hide tools buttons panel
        settingsButton.SetActive(false);   // Hide settings button
        manualButton.SetActive(false);     // Hide manual button
    }

    // Go to the first page of the manual from the book cover
    public void OpenFirstPageFromCover()
    {
        Debug.Log("Opening the first manual page.");
        AudioOpen();
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
            Debug.Log("Previous page: " + currentPage);
            UpdatePage();
        }
    }

    // Update the page display
    private void UpdatePage()
    {
        Debug.Log("Updating page: " + currentPage);
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == currentPage);  // Only the current page is active
        }
    }

    // Close the manual and go back to the game
    public void CloseManual()
    {
        Debug.Log("Closing manual and resuming game.");
        manualPanel.SetActive(false);      // Hide the manual panel
        bookCoverPanel.SetActive(false);   // Hide the book cover
        scoreText.SetActive(true);         // Show score text
        toolsButtons.SetActive(true);      // Show tools buttons panel
        settingsButton.SetActive(true);    // Show settings button
        manualButton.SetActive(true);      // Show manual button
        Time.timeScale = 1f;               // Resume the game
    }





}
