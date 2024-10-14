using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualController : MonoBehaviour
{
    public GameObject[] pages;         // Array to store all pages of the manual
    private int currentPage = 0;       // Index to track the current page

    public GameObject manualPanel;     // Reference to the manual panel (containing all pages)
    public GameObject bookCoverPanel;  // Reference to the book cover panel

    void Start()
    {
        // Ensure the game starts on the book cover screen
        ShowBookCover();
    }

    // Show the book cover panel and hide everything else
    public void ShowBookCover()
    {
        bookCoverPanel.SetActive(true);   // Show the book cover panel
        manualPanel.SetActive(false);     // Hide the manual
        Time.timeScale = 0f;              // Pause the game (if needed)
    }

    // Continue the game (hide the book cover)
    public void ContinueGame()
    {
        bookCoverPanel.SetActive(false);  // Hide the book cover
        Time.timeScale = 1f;              // Resume the game
    }

    // Open the manual (starting from the first page)
    public void OpenManual()
    {
        currentPage = 0;                  // Reset to the first page
        UpdatePage();                     // Show the first page
        bookCoverPanel.SetActive(false);   // Hide the book cover
        manualPanel.SetActive(true);       // Show the manual
    }

    // Go to the next page
    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            UpdatePage();
        }
    }

    // Go to the previous page
    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdatePage();
        }
    }

    // Update the page display
    private void UpdatePage()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == currentPage);  // Only the current page is active
        }
    }

    // Close the manual and go back to the game
    public void CloseManual()
    {
        manualPanel.SetActive(false);  // Hide the manual panel
        Time.timeScale = 1f;           // Resume the game
    }



    
}
