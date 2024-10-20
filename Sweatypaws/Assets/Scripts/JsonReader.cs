using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public TextAsset jsonFile;
    public TextAsset jsonPlayerFile;
    public DialogueList dialogueList;
    public Player player;


    [System.Serializable]
    public class DialogueParts
    {
        public string character_name;
        public string dialog;
        public string character_image;
    }
    [System.Serializable]
    public class DialogueList
    {
        public DialogueParts[] dialogues;
        
    }
    [System.Serializable]
    public class Player
    {
        public int level;
        public DialogueProgress[] dialogue_progress;
        public LevelProgress[] level_progress;
    }
    [System.Serializable]
    public class DialogueProgress
    {
        public string dialogue_name;
        public int level_index;
        public bool watched;
    }
    [System.Serializable]
    public class LevelProgress
    {
        public string level_name;
        public bool complete;
        public int score;
    }

    // Start is called before the first frame update
    void Awake()
    {
        //dialogueList = JsonUtility.FromJson<DialogueList>(jsonFile.text);
        player=JsonUtility.FromJson<Player>(jsonPlayerFile.text);

        string selectedDialogue = ExtractDialogueSet(jsonFile.text, player.dialogue_progress[player.level].dialogue_name);
        dialogueList = JsonUtility.FromJson<DialogueList>(selectedDialogue);
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
