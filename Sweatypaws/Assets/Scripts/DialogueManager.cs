using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static JsonReader;

public class DialogueManager : MonoBehaviour
{
    private JsonReader jsonReader;
    private TextMeshProUGUI characterName;
    private TextMeshProUGUI dialogue;
    private UnityEngine.UI.Image characterImage;
    private UnityEngine.UI.Image backgroundImage;
    private UnityEngine.UI.Button continueButton;
    private int currentDialogueIndex = 0;

    public int GetDialogueIndex()
    {
        return currentDialogueIndex;
    }

    public void SetDialogueIndex(int index)
    {
        currentDialogueIndex = index;
    }
    // Start is called before the first frame update
  
    public void SetContinueButton(bool loadScene)
    {
        GameObject dialogueCanvas = this.gameObject;
        Transform dialogueBackgroun = dialogueCanvas.transform.Find("DialogueBackground");
        GameObject dialogueBackgroundObject = dialogueBackgroun.gameObject;
        //DialogueGroup sis‰lt‰‰ character image ja dialogiWindowin
        Transform dialogueGroup = dialogueCanvas.transform.Find("DialogueGroup");
        Transform dialogueWindow = dialogueGroup.transform.Find("DialogueWindow");
        //DialogueGroup objectit
        GameObject continueButtonObject = dialogueWindow.Find("ContinueButton").gameObject;

        //Asetetaan arvot ja onclick sek‰ kutsutaan functiota
        continueButton = continueButtonObject.GetComponent<UnityEngine.UI.Button>();
        if (!loadScene)
        {
            continueButton.onClick.AddListener(DisplayNextDialogue);
        }
        else
        {
            continueButton.onClick.AddListener(DisplayNextDialogueChangeScene);
        }
        
    }
    public void DisplayNextDialogue()
    {
        GameObject dialogueCanvas = this.gameObject;
        GameObject reader = GameObject.Find("Reader");
        jsonReader = reader.GetComponent<JsonReader>();
        dialogueCanvas.SetActive(true);
        Transform dialogueBackgroun = dialogueCanvas.transform.Find("DialogueBackground");
        GameObject dialogueBackgroundObject = dialogueBackgroun.gameObject;
        Transform dialogueGroup = dialogueCanvas.transform.Find("DialogueGroup");
        Transform dialogueWindow = dialogueGroup.transform.Find("DialogueWindow");
        Transform spreakerCharacter = dialogueGroup.transform.Find("SpeakerCharacter");
        GameObject speakerCharacterObject = spreakerCharacter.gameObject;
        GameObject speakerNameObject = dialogueWindow.Find("SpeakerName").gameObject;
        GameObject dialogueObject = dialogueWindow.Find("DialogueText").gameObject;
        characterName = speakerNameObject.GetComponent<TextMeshProUGUI>();
        characterImage = speakerCharacterObject.GetComponent<UnityEngine.UI.Image>();
        backgroundImage = dialogueBackgroundObject.GetComponent<UnityEngine.UI.Image>();
        dialogue = dialogueObject.GetComponent<TextMeshProUGUI>();


        int currentDialogueIndexNew=GetDialogueIndex();

        if (currentDialogueIndexNew < jsonReader.dialogueList.dialogues.Length)
        {
            DialogueParts currentDialogue = jsonReader.dialogueList.dialogues[currentDialogueIndex];
            characterName.text = currentDialogue.character_name;
            Sprite newCharacterSprite = Resources.Load<Sprite>(currentDialogue.character_image);
            Sprite newBackgroundSprite = Resources.Load<Sprite>(currentDialogue.background_image);
            characterImage.sprite = newCharacterSprite;
            backgroundImage.sprite = newBackgroundSprite;
            dialogue.text = currentDialogue.dialog;
            Debug.Log(newCharacterSprite);

            currentDialogueIndexNew++;
            SetDialogueIndex(currentDialogueIndexNew);
            if (currentDialogueIndex==jsonReader.dialogueList.dialogues.Length)
            {
                
                GameObject continueButtonObject = dialogueWindow.Find("ContinueButton").gameObject;
                TextMeshProUGUI continueButtonTextObject = continueButtonObject.GetComponentInChildren<TextMeshProUGUI>();

                continueButtonTextObject.text = "End";

                continueButton = continueButtonObject.GetComponent<UnityEngine.UI.Button>();

                continueButton.onClick.AddListener(EndDialogue);
        
            }
        }
        else
        {
            Debug.Log("Ei dialogueita j‰ljell‰");
            dialogueCanvas.SetActive(false);
        }
    }

    public void DisplayNextDialogueChangeScene()
    {
        GameObject dialogueCanvas = this.gameObject;
        GameObject reader = GameObject.Find("Reader");
        jsonReader = reader.GetComponent<JsonReader>();
        dialogueCanvas.SetActive(true);
        Transform dialogueBackgroun = dialogueCanvas.transform.Find("DialogueBackground");
        GameObject dialogueBackgroundObject = dialogueBackgroun.gameObject;
        Transform dialogueGroup = dialogueCanvas.transform.Find("DialogueGroup");
        Transform dialogueWindow = dialogueGroup.transform.Find("DialogueWindow");
        Transform spreakerCharacter = dialogueGroup.transform.Find("SpeakerCharacter");
        GameObject speakerCharacterObject = spreakerCharacter.gameObject;
        GameObject speakerNameObject = dialogueWindow.Find("SpeakerName").gameObject;
        GameObject dialogueObject = dialogueWindow.Find("DialogueText").gameObject;
        characterName = speakerNameObject.GetComponent<TextMeshProUGUI>();
        characterImage = speakerCharacterObject.GetComponent<UnityEngine.UI.Image>();
        backgroundImage = dialogueBackgroundObject.GetComponent<UnityEngine.UI.Image>();
        dialogue = dialogueObject.GetComponent<TextMeshProUGUI>();


        int currentDialogueIndexNew = GetDialogueIndex();

        if (currentDialogueIndexNew < jsonReader.dialogueList.dialogues.Length)
        {
            DialogueParts currentDialogue = jsonReader.dialogueList.dialogues[currentDialogueIndex];
            characterName.text = currentDialogue.character_name;
            Sprite newCharacterSprite = Resources.Load<Sprite>(currentDialogue.character_image);
            Sprite newBackgroundSprite = Resources.Load<Sprite>(currentDialogue.background_image);
            characterImage.sprite = newCharacterSprite;
            backgroundImage.sprite = newBackgroundSprite;
            dialogue.text = currentDialogue.dialog;
            Debug.Log(newCharacterSprite);

            currentDialogueIndexNew++;
            SetDialogueIndex(currentDialogueIndexNew);
            if (currentDialogueIndex == jsonReader.dialogueList.dialogues.Length)
            {

                GameObject continueButtonObject = dialogueWindow.Find("ContinueButton").gameObject;
                TextMeshProUGUI continueButtonTextObject = continueButtonObject.GetComponentInChildren<TextMeshProUGUI>();

                continueButtonTextObject.text = "Start level";

                continueButton = continueButtonObject.GetComponent<UnityEngine.UI.Button>();

                continueButton.onClick.AddListener(EndDialogueLoadScene);

            }
        }
        else
        {
            Debug.Log("Ei dialogueita j‰ljell‰");
            dialogueCanvas.SetActive(false);
        }
    }





    public void EndDialogueLoadScene()
    {
        SceneManager.LoadScene("Game");
    }



    public void EndDialogue()
    {
        GameObject dialogueCanvas = this.gameObject;
        PlayerManager.Instance.DialogCompleted(jsonReader.dialogueList.dialogueName);
        SetDialogueIndex(0);
        dialogueCanvas.SetActive(false);
        Debug.Log("End of dialogue.");
    }
}
