using UnityEngine;
using UnityEngine.UI;

public class ToolDescriptionManager : MonoBehaviour
{
    // Array to hold the speech bubble images (which include text)
    public GameObject[] speechBubbles;  // Speech bubble images with Text as children

    // The button that will trigger showing the next text bubble
    public Button nextButton;  // The next button to progress through bubbles

    private int currentBubbleIndex = 0;  // To track which bubble to show next

    // Reference to the first bubble you want to hide after the first click
    public GameObject firstBubbleToHide;

    void Start()
    {
        // Initially hide all speech bubbles except the first one
        foreach (GameObject bubble in speechBubbles)
        {
            bubble.SetActive(false);  // Hide all bubbles initially
        }

        if (firstBubbleToHide != null)
        {
            firstBubbleToHide.SetActive(true);  // Show the first extra bubble (if assigned)
        }

        // Set up the button to trigger showing the next text bubble
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(ShowNextBubble);  // Register button click event
        }
        else
        {
            Debug.LogError("NextButton is not assigned in the Inspector.");
        }
    }

    // Show the next speech bubble and its text
    void ShowNextBubble()
    {
        // First, hide the extra bubble (only once, after the first click)
        if (firstBubbleToHide != null)
        {
            firstBubbleToHide.SetActive(false);  // Hide the extra bubble after first click
            firstBubbleToHide = null;  // Prevent further changes (only hide once)
        }

        // Check if there are more bubbles to show
        if (currentBubbleIndex < speechBubbles.Length)
        {
            // Show the next bubble
            speechBubbles[currentBubbleIndex].SetActive(true);  // Show the current speech bubble

            // Increment the index to show the next bubble on the next click
            currentBubbleIndex++;
        }
        else
        {
            Debug.Log("All tool descriptions are shown!");
            // Optionally reset or close the description panel here if needed
            // ResetText();  // If you want to hide all bubbles after they’ve been shown
        }
    }

    // (Optional) Reset all speech bubbles after all have been shown
    void ResetText()
    {
        foreach (GameObject bubble in speechBubbles)
        {
            bubble.SetActive(false);  // Hide all speech bubbles
        }

        currentBubbleIndex = 0;  // Reset the index so you can show again from the start
    }
}
