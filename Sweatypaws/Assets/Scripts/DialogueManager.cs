using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    private UnityEngine.UI.Button continueButton;
    private UnityEngine.UI.Button previousButton;
    private GameObject prefab;
    public LoadingScene loadingScreen;
    private int currentDialogueIndex = 0;

    public void SetPrefab(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public GameObject GetPrefab()
    {
        return prefab;
    }

    public int GetDialogueIndex()
    {
        return currentDialogueIndex;
    }
    public int GetPreviousDialogueIndex()
    {
        currentDialogueIndex--;
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

    public void SetPreviousButton(bool loadScene)
    {
        GameObject dialogueCanvas = this.gameObject;
        //DialogueGroup sis‰lt‰‰ character image ja dialogiWindowin
        Transform dialogueGroup = dialogueCanvas.transform.Find("DialogueGroup");
        Transform dialogueWindow = dialogueGroup.transform.Find("DialogueWindow");
        //DialogueGroup objectit
        GameObject continueButtonObject = dialogueWindow.Find("PreviousButton").gameObject;

        //Asetetaan arvot ja onclick sek‰ kutsutaan functiota
        previousButton = continueButtonObject.GetComponent<UnityEngine.UI.Button>();
        if (!loadScene)
        {
            previousButton.onClick.AddListener(DisplayPreviousDialogue);
        }
        else
        {
            previousButton.onClick.AddListener(DisplayPreviousDialogue);
        }

    }


    public void DisplayDialogue()
    {
        GameObject dialogueCanvas = this.gameObject;
        Transform dialogueGroup = dialogueCanvas.transform.Find("DialogueGroup");
        Transform dialogueWindow = dialogueGroup.transform.Find("DialogueWindow");
        characterName = SetupCharacterNameComponent();
        characterImage = SetupCharacterImageComponent();
        dialogue = SetupDialogueComponent();

        GameObject reader = GameObject.Find("Reader");
        jsonReader = reader.GetComponent<JsonReader>();
        dialogueCanvas.SetActive(true);

        GameObject previousButtonObject = dialogueWindow.Find("PreviousButton").gameObject;
        previousButtonObject.SetActive(false);
        GameObject prefabObject = Resources.Load<GameObject>(jsonReader.dialogueList.dialogueName);
        GameObject prefabInstantiate=Instantiate(prefabObject,dialogueGroup.position,dialogueGroup.rotation);
        prefabInstantiate.transform.SetParent(dialogueGroup);
        prefabInstantiate.transform.SetAsFirstSibling();
        SetPrefab(prefabInstantiate);
        Animator prefabAnimator = prefab.GetComponent<Animator>();
        DialogueParts currentDialogue = jsonReader.dialogueList.dialogues[0];
        prefabAnimator.SetTrigger(currentDialogue.animation_trigger);
            characterName.text = currentDialogue.character_name;
            Sprite newCharacterSprite = Resources.Load<Sprite>(currentDialogue.character_image);
            characterImage.sprite = newCharacterSprite;
            dialogue.text = currentDialogue.dialog;
            //Debug.Log(newCharacterSprite);

    }


    public void DisplayNextDialogue()
    {
        GameObject dialogueCanvas = this.gameObject;
        Transform dialogueGroup = dialogueCanvas.transform.Find("DialogueGroup");
        Transform dialogueWindow = dialogueGroup.transform.Find("DialogueWindow");
        characterName =SetupCharacterNameComponent();
        characterImage=SetupCharacterImageComponent();
        dialogue = SetupDialogueComponent();

        GameObject reader = GameObject.Find("Reader");
        jsonReader = reader.GetComponent<JsonReader>();
        dialogueCanvas.SetActive(true);
        Animator prefabAnimator=prefab.GetComponent<Animator>();

        int currentDialogueIndexNew=GetDialogueIndex();
        currentDialogueIndexNew++;
        SetDialogueIndex(currentDialogueIndexNew);
        //Debug.Log(currentDialogueIndexNew);
        if (currentDialogueIndexNew>0)
        {    
            GameObject previousButtonObject = dialogueWindow.Find("PreviousButton").gameObject;
            previousButtonObject.SetActive(true);
        }

        if (currentDialogueIndexNew < jsonReader.dialogueList.dialogues.Length)
        {
            DialogueParts currentDialogue = jsonReader.dialogueList.dialogues[currentDialogueIndexNew];
            prefabAnimator.SetTrigger(currentDialogue.animation_trigger);
            characterName.text = currentDialogue.character_name;
            Sprite newCharacterSprite = Resources.Load<Sprite>(currentDialogue.character_image);
            characterImage.sprite = newCharacterSprite;
            dialogue.text = currentDialogue.dialog;
            //Debug.Log(newCharacterSprite);
            
        }
        else
        {

            //Debug.Log("Ei dialogueita j‰ljell‰");
            EndDialogue();
        }
    }

    public void DisplayPreviousDialogue()
    {
        GameObject dialogueCanvas = this.gameObject;
        Transform dialogueGroup = dialogueCanvas.transform.Find("DialogueGroup");
        Transform dialogueWindow = dialogueGroup.transform.Find("DialogueWindow");
        characterName = SetupCharacterNameComponent();
        characterImage = SetupCharacterImageComponent();
        dialogue = SetupDialogueComponent();

        GameObject reader = GameObject.Find("Reader");
        jsonReader = reader.GetComponent<JsonReader>();
        dialogueCanvas.SetActive(true);
        Animator prefabAnimator = prefab.GetComponent<Animator>();

        int currentDialogueIndexNew = GetPreviousDialogueIndex();
        SetDialogueIndex(currentDialogueIndexNew);
        //Debug.Log(currentDialogueIndexNew);
        if (currentDialogueIndexNew<=0)
        {
            GameObject previousButtonObject = dialogueWindow.Find("PreviousButton").gameObject;
            previousButtonObject.SetActive(false);
        }
        if (currentDialogueIndexNew < jsonReader.dialogueList.dialogues.Length)
        {
            DialogueParts currentDialogue = jsonReader.dialogueList.dialogues[currentDialogueIndexNew];
            prefabAnimator.SetTrigger(currentDialogue.animation_trigger);
            characterName.text = currentDialogue.character_name;
            Sprite newCharacterSprite = Resources.Load<Sprite>(currentDialogue.character_image);
            characterImage.sprite = newCharacterSprite;
            dialogue.text = currentDialogue.dialog;
            //Debug.Log(newCharacterSprite);

           
        }
        else
        {
            //Debug.Log("Ei dialogueita j‰ljell‰");
            dialogueCanvas.SetActive(false);
        }
    }

    public void DisplayNextDialogueChangeScene()
    {
        GameObject dialogueCanvas = this.gameObject;
        Transform dialogueGroup = dialogueCanvas.transform.Find("DialogueGroup");
        Transform dialogueWindow = dialogueGroup.transform.Find("DialogueWindow");
        characterName = SetupCharacterNameComponent();
        characterImage = SetupCharacterImageComponent();
        dialogue = SetupDialogueComponent();

        GameObject reader = GameObject.Find("Reader");
        jsonReader = reader.GetComponent<JsonReader>();
        dialogueCanvas.SetActive(true);
        Animator prefabAnimator = prefab.GetComponent<Animator>();

        int currentDialogueIndexNew = GetDialogueIndex();
        currentDialogueIndexNew++;
        SetDialogueIndex(currentDialogueIndexNew);

        if (currentDialogueIndexNew > 0)
        {
            GameObject previousButtonObject = dialogueWindow.Find("PreviousButton").gameObject;
            previousButtonObject.SetActive(true);
        }


        if (currentDialogueIndexNew < jsonReader.dialogueList.dialogues.Length)
        {
            DialogueParts currentDialogue = jsonReader.dialogueList.dialogues[currentDialogueIndexNew];
            prefabAnimator.SetTrigger(currentDialogue.animation_trigger);
            characterName.text = currentDialogue.character_name;
            Sprite newCharacterSprite = Resources.Load<Sprite>(currentDialogue.character_image);
            characterImage.sprite = newCharacterSprite;
            dialogue.text = currentDialogue.dialog;
            //Debug.Log(newCharacterSprite);

            
        }
        else
        {
            //Debug.Log("Ei dialogueita j‰ljell‰");
            EndDialogueLoadScene();
        }
    }

    public TextMeshProUGUI SetupCharacterNameComponent()
    {
        GameObject dialogueCanvas = this.gameObject;
        Transform dialogueGroup = dialogueCanvas.transform.Find("DialogueGroup");
        Transform dialogueWindow = dialogueGroup.transform.Find("DialogueWindow");
        GameObject speakerNameObject = dialogueWindow.Find("SpeakerName").gameObject;
       return characterName = speakerNameObject.GetComponent<TextMeshProUGUI>();
    }
    public UnityEngine.UI.Image SetupCharacterImageComponent()
    {
        GameObject dialogueCanvas = this.gameObject;
        Transform dialogueGroup = dialogueCanvas.transform.Find("DialogueGroup");
        Transform spreakerCharacter = dialogueGroup.transform.Find("SpeakerCharacter");
        GameObject speakerCharacterObject = spreakerCharacter.gameObject;
        return characterImage = speakerCharacterObject.GetComponent<UnityEngine.UI.Image>();
    }


    public TextMeshProUGUI SetupDialogueComponent()
    {
        GameObject dialogueCanvas = this.gameObject;
        Transform dialogueGroup = dialogueCanvas.transform.Find("DialogueGroup");
        Transform dialogueWindow = dialogueGroup.transform.Find("DialogueWindow");
        GameObject dialogueObject = dialogueWindow.Find("DialogueText").gameObject;
        return dialogue = dialogueObject.GetComponent<TextMeshProUGUI>();
    }

    public void EndDialogueLoadScene()
    {
        GameObject dialogueCanvas = this.gameObject;
        PlayerManager.Instance.DialogCompleted(jsonReader.dialogueList.dialogueName);
        SetDialogueIndex(0);
        dialogueCanvas.SetActive(false);
        loadingScreen.PlayTimeLine();
        continueButton.onClick.RemoveListener(DisplayNextDialogueChangeScene);
        previousButton.onClick.RemoveListener(DisplayPreviousDialogue);
        Destroy(prefab);
        //SceneManager.LoadScene("Game");
    }



    public void EndDialogue()
    {
        GameObject dialogueCanvas = this.gameObject;
        PlayerManager.Instance.DialogCompleted(jsonReader.dialogueList.dialogueName);
        SetDialogueIndex(0);
        dialogueCanvas.SetActive(false);
        continueButton.onClick.RemoveListener(DisplayNextDialogue);
        previousButton.onClick.RemoveListener(DisplayPreviousDialogue);
        //Debug.Log("End of dialogue.");
        Destroy(prefab);
    }
}
