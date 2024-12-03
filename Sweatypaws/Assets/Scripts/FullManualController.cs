using UnityEngine;

public class BombManualManager : MonoBehaviour
{
    // Panels for full manual, pages, TOC, and map
    public GameObject FullManualPanel;   // Parent panel for the full bomb manual
    public GameObject BookCoverPanel;    // Book cover panel (child of FullManualPanel)
    public GameObject[] pages;          // Array of page panels (children of FullManualPanel)
    public GameObject TOCPanel;         // Table of Contents panel (child of FullManualPanel)
    public GameObject MapPanel;         // Map panel
    public AudioClip pageTurnClip;
    public AudioClip closeManualClip;
    private AudioSource audioSource;

    private int currentPageIndex = 0;   // Tracks the current page

    // Show the full manual starting at the book cover
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OpenFullManual()
    {
        Debug.Log("Opening Full Manual...");
        HideAllPanels();                  // Hide all other panels
        FullManualPanel.SetActive(true);  // Activate the full manual panel
        ShowBookCover();                  // Start with the book cover
        Debug.Log("FullManualPanel activated with BookCoverPanel!");
    }

    // Close the manual and return to the map
    public void CloseManual()
    {
        Debug.Log("Closing Full Manual...");
        HideAllPanels();               // Hide all panels
        MapPanel.SetActive(true);      // Show the map panel
        Debug.Log("MapPanel activated!");
    }

    // Show the book cover panel
    public void ShowBookCover()
    {
        HideAllPanels();
        FullManualPanel.SetActive(true); // Ensure the parent panel is active
        BookCoverPanel.SetActive(true); // Activate the book cover panel
        if (currentPageIndex>0)
        {
            currentPageIndex = 0;
        }
        Debug.Log("BookCoverPanel activated!");
    }

    // Show the specified page
    public void ShowPage(int pageIndex)
    {
        HideAllPanels();
        if (pageIndex >= 0 && pageIndex < pages.Length)
        {
            FullManualPanel.SetActive(true); // Ensure the parent panel is active
            pages[pageIndex].SetActive(true);
            currentPageIndex = pageIndex;
            Debug.Log($"Page {pageIndex} activated!");
        }
        else
        {
            Debug.LogError("Page index out of range: " + pageIndex);
        }
    }

    // Show the TOC panel
    public void ShowTOC()
    {
        PlayAudio(pageTurnClip);
        HideAllPanels();
        FullManualPanel.SetActive(true); // Ensure the parent panel is active
        TOCPanel.SetActive(true);
        currentPageIndex = 0;
        Debug.Log("TOCPanel activated!");
    }

    // Show the map panel
    public void ShowMap()
    {
        PlayAudio(closeManualClip);
        HideAllPanels();
        MapPanel.SetActive(true); // Activate the map panel
        Debug.Log("MapPanel activated!");
    }

    // Go to the next page
    public void NextPage()
    {
        PlayAudio(pageTurnClip);
        if (currentPageIndex < pages.Length - 1)
        {
            ShowPage(currentPageIndex + 1);
        }
    }

    // Go to the previous page
    public void PreviousPage()
    {
        PlayAudio(pageTurnClip);
        if (currentPageIndex > 0)
        {
            ShowPage(currentPageIndex - 1);
        }
    }

    // Hide all panels
    private void HideAllPanels()
    {
        Debug.Log("Hiding all panels...");
        FullManualPanel.SetActive(false);
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        BookCoverPanel.SetActive(false);
        TOCPanel.SetActive(false);
        MapPanel.SetActive(false);
        Debug.Log("All panels hidden.");
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
