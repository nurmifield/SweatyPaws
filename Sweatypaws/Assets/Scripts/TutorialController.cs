using UnityEngine;
using UnityEngine.UI;

public class HowToPlayManager : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public GameObject introductionPanel;
    public GameObject toolsPanel;
    public GameObject gameFlowPanel;

    public GameObject[] speechBubbles; // For toolsPanel

    public GameObject mrSnugglesImage;  // Mr. Snuggles' image
    public GameObject doctorImage;      // Doctor's image
    public GameObject speechBubble1;    // First speech bubble
    public GameObject speechBubble2;    // Second speech bubble
    public GameObject speechBubble3;    // Third speech bubble

    public GameObject introBubble; // Introduction bubble
    public Button nextPageButton;
    public Button nextButton;      // Button to progress through bubbles in toolsPanel
    public Button gameFlowNextButton;  // Button to progress through bubbles in gameFlowPanel
    public Button skipButton;
    public Button startOverButton;      // Button to skip the tutorial

    private int currentBubbleIndex = 0;  // Track which bubble to show next

    void Start()
    {
        var player = PlayerManager.Instance;
        if (!player.playerData.tutorial_done)
        {
            howToPlayPanel.SetActive(true);
        }
        else
        {
            return;
        }

        // Ensure all speech bubbles and text are hidden at start
        foreach (GameObject bubble in speechBubbles)
        {
            bubble.SetActive(false);
        }

        mrSnugglesImage.SetActive(false);
        doctorImage.SetActive(false);
        speechBubble1.SetActive(false);
        speechBubble2.SetActive(false);
        speechBubble3.SetActive(false);

        // Show only the Introduction panel at the start
        if (introductionPanel != null) introductionPanel.SetActive(true);
        if (toolsPanel != null) toolsPanel.SetActive(false);
        if (gameFlowPanel != null) gameFlowPanel.SetActive(false);

        // Set up button listeners
        if (nextPageButton != null) nextPageButton.onClick.AddListener(ShowToolsPanel);
        if (nextButton != null) nextButton.onClick.AddListener(ShowNextBubbleInToolsPanel);
        if (gameFlowNextButton != null) gameFlowNextButton.onClick.AddListener(ShowNextBubbleInGameFlowPanel);
        if (skipButton != null) skipButton.onClick.AddListener(StartGame); // Start game if skipped
        if (startOverButton != null) startOverButton.onClick.AddListener(RestartTutorial);
    }

    // Show the next speech bubble in toolsPanel
    void ShowNextBubbleInToolsPanel()
    {
        if (introBubble.activeSelf)
        {
            introBubble.SetActive(false);
        }

        // Show the next bubble in the speechBubbles array for toolsPanel
        if (currentBubbleIndex < speechBubbles.Length)
        {
            speechBubbles[currentBubbleIndex].SetActive(true);
            currentBubbleIndex++;
        }
        else if (currentBubbleIndex == speechBubbles.Length) // After the last bubble
        {
            // Switch to gameFlowPanel
            toolsPanel.SetActive(false);
            gameFlowPanel.SetActive(true);

            // Reset currentBubbleIndex for gameFlowPanel usage
            currentBubbleIndex = 0;

            // Show Mr. Snuggles with SpeechBubble1
            mrSnugglesImage.SetActive(true);
            speechBubble1.SetActive(true);

            // Ensure the gameFlowNextButton is visible
            gameFlowNextButton.gameObject.SetActive(true);
        }
    }

    // Show the next speech bubble in gameFlowPanel
    void ShowNextBubbleInGameFlowPanel()
    {
        // Step 1: Show Mr. Snuggles with SpeechBubble2
        if (currentBubbleIndex == 0)
        {
            speechBubble1.SetActive(false);
            speechBubble2.SetActive(true);

            // Keep Mr. Snuggles visible, hide others
            mrSnugglesImage.SetActive(true);
            doctorImage.SetActive(false);
            speechBubble3.SetActive(false);

            currentBubbleIndex++;
        }
        // Step 2: Show Doctor with SpeechBubble3
        else if (currentBubbleIndex == 1)
        {
            speechBubble2.SetActive(false);

            doctorImage.SetActive(true);
            speechBubble3.SetActive(true);

            // Hide Mr. Snuggles
            mrSnugglesImage.SetActive(false);

            currentBubbleIndex++;
        }
        // Step 3: Transition to the game/map
        else if (currentBubbleIndex == 2)
        {
            StartGame();
        }
    }

    // Show the Tools panel and reset bubbles
    void ShowToolsPanel()
    {
        introductionPanel.SetActive(false);
        toolsPanel.SetActive(true);

        // Hide all bubbles when transitioning
        foreach (GameObject bubble in speechBubbles)
        {
            bubble.SetActive(false);
        }

        currentBubbleIndex = 0;  // Reset index for the next panel's sequence

        // Ensure the "nextButton" is visible
        nextButton.gameObject.SetActive(true);
    }

    // Skip button method to start the game immediately
    void StartGame()
    {
        var player = PlayerManager.Instance;
        Debug.Log("Game Started!");
        howToPlayPanel.SetActive(false);
        player.TutorialDone();
    }

    public void RestartTutorial()
    {
        introductionPanel.SetActive(false);
        toolsPanel.SetActive(false);
        gameFlowPanel.SetActive(false);

        foreach (GameObject bubble in speechBubbles)
        {
            bubble.SetActive(false);
        }

        mrSnugglesImage.SetActive(false);
        doctorImage.SetActive(false);
        speechBubble1.SetActive(false);
        speechBubble2.SetActive(false);
        speechBubble3.SetActive(false);

        currentBubbleIndex = 0;
        introductionPanel.SetActive(true);

        // Ensure the buttons are reset and visible
        nextButton.gameObject.SetActive(true);
        gameFlowNextButton.gameObject.SetActive(true);
    }
}
