using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public TextAsset jsonFile;
    public DialogueList dialogueList;


    [System.Serializable]
    public class DialogueParts
    {
        public string character_name;
        public string dialog;
        public string character_image;
        public string background_image;
    }
    [System.Serializable]
    public class DialogueList
    {
        public string dialogueName;
        public DialogueParts[] dialogues;
        
    }

    // Start is called before the first frame update
    void Awake()
    {
        var player = PlayerManager.Instance;
        //dialogueList = JsonUtility.FromJson<DialogueList>(jsonFile.text);
        string selectedDialogue = ExtractDialogueSet(jsonFile.text, player.playerData.dialogue_progress[player.playerData.dialogue_level].dialogue_name );
        dialogueList = JsonUtility.FromJson<DialogueList>(selectedDialogue);
        dialogueList.dialogueName = player.playerData.dialogue_progress[player.playerData.dialogue_level].dialogue_name;

        //Debug.Log(selectedDialogue);

        //Debug.Log(player.dialogue_progress[player.level].dialogue_name);
        
    }

    string ExtractDialogueSet(string jsonText, string dialogueSetName)
    {
        int startIndex = jsonText.IndexOf($"\"{dialogueSetName}\"");
        if (startIndex != -1)
        {
            int startArray = jsonText.IndexOf("[", startIndex);
            int endArray = jsonText.IndexOf("]", startArray) + 1;
            string dialogueJson = jsonText.Substring(startArray, endArray - startArray);
            return "{\"dialogues\":" + dialogueJson + "}";
        }
        Debug.LogError("Dialogue set not found: " + dialogueSetName);
        return null;
    }

}
