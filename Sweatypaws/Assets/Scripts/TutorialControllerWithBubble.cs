using UnityEngine;
using UnityEngine.UI;

public class HowToPlayManager : MonoBehaviour
{
    // The two panels: Introduction and Tools
    public GameObject introductionPanel;  // The introduction panel
    public GameObject toolsPanel;         // The tools panel

    // Array to hold the speech bubble images (which include text)
    public GameObject[] speechBubbles;  // Speech bubble images with Text as children

    // The button to go to the next page (Introduction to Tools)
    public Button nextPageButton;  // The next button to go from Introduction to Tools

    // The button that will trigger showing the next text bubble
    public Button nextButton;  // The next button to progress through bubbles

    private int currentBubbleIndex = 0;  // To track which bubble to show next

    // Reference to Mr. Snuggles' bubble
    public GameObject mrSnugglesBubble;  // Mr. Snuggles' speech bubble (should hide after first click)

    void Start()
    {
        // Initially hide all speech bubbles except Mr. Snuggles' text
        foreach (GameObject bubble in speechBubbles)
        {
            bubble.SetActive(false);  // Hide all bubbles initially
        }

        // Ensure only the Introduction panel is active initially
        if (introductionPanel != null)
        {
            introductionPanel.SetActive(true);  // Show Introduction
        }

        if (toolsPanel != null)
        {
            toolsPanel.SetActive(false);  // Hide Tools panel initially
        }

        // Set up the button to transition from Introduction to Tools
        if (nextPageButton != null)
        {
            nextPageButton.onClick.AddListener(ShowToolsPanel);  // Register button click event for transition
        }
        else
        {
            Debug.LogError("nextPageButton is not assigned in the Inspector.");
        }

        // Set up the button to trigger speech bubble progression
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(ShowNextBubble);  // Register button click event for speech bubbles
        }
        else
        {
            Debug.LogError("nextButton is not assigned in the Inspector.");
        }

        // Ensure Mr. Snuggles' bubble is visible at the start
        if (mrSnugglesBubble != null)
        {
            mrSnugglesBubble.SetActive(true);  // Show Mr. Snuggles' text at the start
        }
    }

    // This method is called when the 'Next' button is clicked to show speech bubbles
    void ShowNextBubble()
    {
        // If it's the first click (after Mr. Snuggles' bubble), hide Mr. Snuggles' bubble
        if (mrSnugglesBubble != null)
        {
            mrSnugglesBubble.SetActive(false);  // Hide Mr. Snuggles' bubble
        }

        // Show the next speech bubble if any are left
        if (currentBubbleIndex < speechBubbles.Length)
        {
            speechBubbles[currentBubbleIndex].SetActive(true);  // Show the current speech bubble
            currentBubbleIndex++;  // Increment the index to show the next bubble
        }
        else
        {
            Debug.Log("All tool descriptions are shown!");
            // Hide all speech bubbles after the last one
            foreach (GameObject bubble in speechBubbles)
            {
                bubble.SetActive(false);  // Hide all speech bubbles
            }
        }
    }

    // This method is called when the 'NextPage' button is clicked to transition to the Tools panel
    void ShowToolsPanel()
    {
        // Hide the Introduction panel
        if (introductionPanel != null)
        {
            introductionPanel.SetActive(false);
        }

        // Show the Tools panel
        if (toolsPanel != null)
        {
            toolsPanel.SetActive(true);

            // Ensure all speech bubbles are hidden when transitioning to the Tools panel
            foreach (GameObject bubble in speechBubbles)
            {
                bubble.SetActive(false);  // Hide all speech bubbles initially
            }
        }
    }
}
