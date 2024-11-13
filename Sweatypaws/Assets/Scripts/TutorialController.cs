using UnityEngine;
using UnityEngine.UI;

public class HowToPlayManager : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public GameObject introductionPanel;
    public GameObject toolsPanel;
    public GameObject gameFlowPanel;

  
    public GameObject[] speechBubbles; 

    
    public GameObject firstGameFlowBubble;  
    public GameObject finalGameFlowBubble;  

    public GameObject introBubble;
    public Button nextPageButton;  
    public Button nextButton;      // Button to progress through bubbles in toolsPanel
    public Button gameFlowNextButton;  // Button to progress through bubbles in gameFlowPanel
    public Button skipButton;      // Button to skip the tutorial

    private int currentBubbleIndex = 0;  // Track which bubble to show next

    void Start()
    {

         var player = PlayerManager.Instance;
         if (!player.playerData.tutorial_done){
            howToPlayPanel.SetActive(true);
         }else
          {
            return;
        }
        // Ensure all speech bubbles and text are hidden at start
        foreach (GameObject bubble in speechBubbles)
        {
            bubble.SetActive(false);
        }
        if (firstGameFlowBubble != null) firstGameFlowBubble.SetActive(false);
        if (finalGameFlowBubble != null) finalGameFlowBubble.SetActive(false);

        // Show only the Introduction panel at the start
        if (introductionPanel != null) introductionPanel.SetActive(true);
        if (toolsPanel != null) toolsPanel.SetActive(false);
        if (gameFlowPanel != null) gameFlowPanel.SetActive(false);

        // Set up button listeners
        if (nextPageButton != null) nextPageButton.onClick.AddListener(ShowToolsPanel);
        if (nextButton != null) nextButton.onClick.AddListener(ShowNextBubbleInToolsPanel);
        if (gameFlowNextButton != null) gameFlowNextButton.onClick.AddListener(ShowNextBubbleInGameFlowPanel);
        if (skipButton != null) skipButton.onClick.AddListener(StartGame); // Start game if skipped
    }

    // Show the next speech bubble in toolsPanel
    void ShowNextBubbleInToolsPanel()
    {
        if(introBubble.activeSelf){
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

            // Show the first gameFlow bubble
            if (firstGameFlowBubble != null)
            {
                firstGameFlowBubble.SetActive(true);
            }
        }
    }

    // Show the next speech bubble in gameFlowPanel
    void ShowNextBubbleInGameFlowPanel()
    {
        if (currentBubbleIndex == 0 && firstGameFlowBubble != null)
        {
            firstGameFlowBubble.SetActive(false);  // Hide the first gameFlow bubble
            finalGameFlowBubble.SetActive(true);   // Show the final gameFlow bubble
            currentBubbleIndex++;
        }
        else if (currentBubbleIndex == 1) // After final bubble
        {
            StartGame();  // Start the game
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
    }

    // Skip button method to start the game immediately
    void StartGame()
    {
         var player = PlayerManager.Instance;
        Debug.Log("Game Started!");
        howToPlayPanel.SetActive(false);
        player.TutorialDone();
    }
}
