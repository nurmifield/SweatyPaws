using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    // Start is called before the first frame update
    void Start()
    {
        
        GameObject reader = GameObject.Find("Reader");
        if (reader != null)
        {
            jsonReader = reader.GetComponent<JsonReader>();

            if (jsonReader != null)
            {
                //Canvas
                GameObject dialogueCanvas = this.gameObject;
                Transform dialogueBackgroun = dialogueCanvas.transform.Find("DialogueBackground");
                GameObject dialogueBackgroundObject = dialogueBackgroun.gameObject;
                //DialogueGroup sis‰lt‰‰ character image ja dialogiWindowin
                Transform dialogueGroup = dialogueCanvas.transform.Find("DialogueGroup");
                Transform dialogueWindow = dialogueGroup.transform.Find("DialogueWindow");
                Transform spreakerCharacter = dialogueGroup.transform.Find("SpeakerCharacter");
                //DialogueGroup objectit
                GameObject speakerNameObject = dialogueWindow.Find("SpeakerName").gameObject;
                GameObject dialogueObject = dialogueWindow.Find("DialogueText").gameObject;
                GameObject speakerCharacterObject = spreakerCharacter.gameObject;
                GameObject continueButtonObject = dialogueWindow.Find("ContinueButton").gameObject;

                //Asetetaan arvot ja onclick sek‰ kutsutaan functiota
                continueButton = continueButtonObject.GetComponent<UnityEngine.UI.Button>();
                continueButton.onClick.AddListener(DisplayNextDialogue);
                characterImage= speakerCharacterObject.GetComponent<UnityEngine.UI.Image>();
                backgroundImage = dialogueBackgroundObject.GetComponent<UnityEngine.UI.Image>();

                characterName = speakerNameObject.GetComponent<TextMeshProUGUI>();
                dialogue = dialogueObject.GetComponent<TextMeshProUGUI>();
                DisplayNextDialogue();



            }
            else Debug.Log("jsonReader is null");
        }
        else Debug.Log("reader is null");
    }

    public void DisplayNextDialogue()
    {
        if (currentDialogueIndex < jsonReader.dialogueList.dialogues.Length)
        {
            DialogueParts currentDialogue = jsonReader.dialogueList.dialogues[currentDialogueIndex];
            characterName.text = currentDialogue.character_name;
            Sprite newCharacterSprite = Resources.Load<Sprite>(currentDialogue.character_image);
            Sprite newBackgroundSprite = Resources.Load<Sprite>(currentDialogue.background_image);
            characterImage.sprite = newCharacterSprite;
            backgroundImage.sprite = newBackgroundSprite;
            dialogue.text = currentDialogue.dialog;
            Debug.Log(newCharacterSprite);

            currentDialogueIndex++;
            if (currentDialogueIndex==jsonReader.dialogueList.dialogues.Length)
            {
                GameObject dialogueCanvas = this.gameObject;
                Transform dialogueGroup = dialogueCanvas.transform.Find("DialogueGroup");
                Transform dialogueWindow = dialogueGroup.transform.Find("DialogueWindow");
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
            Destroy(this.gameObject);
        }
    }
    public void EndDialogue()
    {
        PlayerManager.Instance.DialogCompleted(jsonReader.dialogueList.dialogueName);
        Destroy(this.gameObject,1f);
        Debug.Log("End of dialogue.");
    }
}
