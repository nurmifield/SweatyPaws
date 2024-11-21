using UnityEngine;
using UnityEngine.UI;

public class BombManualManager : MonoBehaviour
{
    // Panels for book cover, pages, TOC, and map
    public GameObject BookCoverPanel;    // Book cover panel
    public GameObject[] pages;          // Array of page panels
    public GameObject TOCPanel;         // Table of Contents panel
    public GameObject MapPanel;         // Map panel

    // Dropdown for section selection
    public Dropdown sectionSelection;   // Dropdown for selecting sections

    private int currentPageIndex = 0;   // Tracks the current page

    void Start()
    {
        // Show the book cover at the start
        ShowBookCover();
    }

    // Show the book cover
    public void ShowBookCover()
    {
        HideAllPanels();
        BookCoverPanel.SetActive(true);
    }

    // Show the specified page
    public void ShowPage(int pageIndex)
    {
        HideAllPanels();
        if (pageIndex >= 0 && pageIndex < pages.Length)
        {
            pages[pageIndex].SetActive(true);
            currentPageIndex = pageIndex;
        }
        else
        {
            Debug.LogError("Page index out of range: " + pageIndex);
        }
    }

    // Show the TOC panel
    public void ShowTOC()
    {
        HideAllPanels();
        TOCPanel.SetActive(true);
    }

    // Show the map panel
    public void ShowMap()
    {
        HideAllPanels();
        MapPanel.SetActive(true);
    }

    // Go to the next page
    public void NextPage()
    {
        if (currentPageIndex < pages.Length - 1)
        {
            ShowPage(currentPageIndex + 1);
        }
    }

    // Go to the previous page
    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            ShowPage(currentPageIndex - 1);
        }
    }

    // Jump to a specific section from the dropdown
    public void JumpToSection()
    {
        int selectedIndex = sectionSelection.value;
        ShowPage(selectedIndex);
    }

    // Hide all panels
    private void HideAllPanels()
    {
        BookCoverPanel.SetActive(false);
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        TOCPanel.SetActive(false);
        MapPanel.SetActive(false);
    }
}
