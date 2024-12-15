using UnityEngine;
using UnityEngine.UI;

public class HowToPlayManager : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public GameObject introductionPanel;
    public GameObject toolsPanel;
    public GameObject gameFlowPanel;

    public GameObject[] speechBubbles; 

    public GameObject mrSnugglesImage;  
    public GameObject doctorImage;      
    public GameObject speechBubble1;    
    public GameObject speechBubble2;    
    public GameObject speechBubble3;    
    public GameObject speechBubble4;    

    public GameObject introBubble; 
    public Button nextPageButton;
    public Button nextButton;      
    public Button gameFlowNextButton;  
    public Button skipButton;
    public Button startOverButton;      

    private int currentBubbleIndex = 0;  

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

        
        foreach (GameObject bubble in speechBubbles)
        {
            bubble.SetActive(false);
        }

        mrSnugglesImage.SetActive(false);
        doctorImage.SetActive(false);
        speechBubble1.SetActive(false);
        speechBubble2.SetActive(false);
        speechBubble3.SetActive(false);
        speechBubble4.SetActive(false);

        
        if (introductionPanel != null) introductionPanel.SetActive(true);
        if (toolsPanel != null) toolsPanel.SetActive(false);
        if (gameFlowPanel != null) gameFlowPanel.SetActive(false);

        
        if (nextPageButton != null) nextPageButton.onClick.AddListener(ShowToolsPanel);
        if (nextButton != null) nextButton.onClick.AddListener(ShowNextBubbleInToolsPanel);
        if (gameFlowNextButton != null) gameFlowNextButton.onClick.AddListener(ShowNextBubbleInGameFlowPanel);
        if (skipButton != null) skipButton.onClick.AddListener(StartGame); // Start game if skipped
        if (startOverButton != null) startOverButton.onClick.AddListener(RestartTutorial);
    }

    
    void ShowNextBubbleInToolsPanel()
    {
        if (introBubble.activeSelf)
        {
            introBubble.SetActive(false);
        }

        
        if (currentBubbleIndex < speechBubbles.Length)
        {
            speechBubbles[currentBubbleIndex].SetActive(true);
            currentBubbleIndex++;
        }
        else if (currentBubbleIndex == speechBubbles.Length) 
        {
            
            toolsPanel.SetActive(false);
            gameFlowPanel.SetActive(true);

            
            currentBubbleIndex = 0;

            
            mrSnugglesImage.SetActive(true);
            speechBubble1.SetActive(true);

            
            gameFlowNextButton.gameObject.SetActive(true);
        }
    }

    
    void ShowNextBubbleInGameFlowPanel()
    {
        
        if (currentBubbleIndex == 0)
        {
            speechBubble1.SetActive(false);
            speechBubble2.SetActive(true);

            
            mrSnugglesImage.SetActive(true);
            doctorImage.SetActive(false);
            speechBubble3.SetActive(false);

            currentBubbleIndex++;
        }
        
        else if (currentBubbleIndex == 1)
        {
            speechBubble2.SetActive(false);

            doctorImage.SetActive(true);
            speechBubble3.SetActive(true);

           
            mrSnugglesImage.SetActive(false);

            currentBubbleIndex++;
        }
        
        else if (currentBubbleIndex == 2)
        {
            speechBubble3.SetActive(false);
            doctorImage.SetActive(false);
            mrSnugglesImage.SetActive(true);
            speechBubble4.SetActive(true);

            currentBubbleIndex++;
        }
        
        else if (currentBubbleIndex == 3)
        {
            StartGame();
        }
    }

    
    void ShowToolsPanel()
    {
        introductionPanel.SetActive(false);
        toolsPanel.SetActive(true);

        
        foreach (GameObject bubble in speechBubbles)
        {
            bubble.SetActive(false);
        }

        currentBubbleIndex = 0;  

        
        nextButton.gameObject.SetActive(true);
    }

    
    void StartGame()
    {
        var player = PlayerManager.Instance;
        //Debug.Log("Game Started!");
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
        speechBubble4.SetActive(false);

        currentBubbleIndex = 0;
        introductionPanel.SetActive(true);

        
        nextButton.gameObject.SetActive(true);
        gameFlowNextButton.gameObject.SetActive(true);
    }
}
