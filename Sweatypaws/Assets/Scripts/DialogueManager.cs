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
                GameObject dialogueCanvas = this.gameObject;
                Transform dialogueWindow = dialogueCanvas.transform.Find("DialogueWindow");
                GameObject speakerNameObject = dialogueWindow.Find("SpeakerName").gameObject;
                GameObject dialogueObject = dialogueWindow.Find("DialogueText").gameObject;
                GameObject continueButtonObject = dialogueWindow.Find("ContinueButton").gameObject;

                continueButton = continueButtonObject.GetComponent<UnityEngine.UI.Button>();
                continueButton.onClick.AddListener(DisplayNextDialogue);

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
            dialogue.text = currentDialogue.dialog;

            currentDialogueIndex++;
        }
        else
        {
            Debug.Log("End of dialogue.");
        }
    }
}
