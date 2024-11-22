using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JsonReader : MonoBehaviour
{
    public TextAsset jsonFile;
    public TextAsset jsonBombLogicDataFile;
    public DialogueList dialogueList;
    public BombLogicData bombLogicData;


    [System.Serializable]
    public class DialogueParts
    {
        public string character_name;
        public string dialog;
        public string character_image;
        public string animation_trigger;
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
        if (SceneManager.GetActiveScene().name == "Map")
        {
            UpdateNewDialogueList();
            Debug.Log("Scene is Map");
        }
        else if(SceneManager.GetActiveScene().name == "Game")
        {
            UpdateNewBombLogicDataLevel();
            Debug.Log("Scene is Game");
        }


    }


    public void UpdateNewDialogueList()
    {
        var player = PlayerManager.Instance;
        //dialogueList = JsonUtility.FromJson<DialogueList>(jsonFile.text);
        string selectedDialogue = ExtractDialogueSet(jsonFile.text, player.playerData.dialogue_progress[player.playerData.dialogue_level].dialogue_name);
        dialogueList = JsonUtility.FromJson<DialogueList>(selectedDialogue);
        dialogueList.dialogueName = player.playerData.dialogue_progress[player.playerData.dialogue_level].dialogue_name;
    }

    /*
     * Aikaisemmin käytössä ollut
     * public void UpdateNewBombLevel()
    {
        var player = PlayerManager.Instance;
        //dialogueList = JsonUtility.FromJson<DialogueList>(jsonFile.text);
        string selectLevel = ExtractLevelSet(jsonBombFile.text,player.GetSelectedLevel());
        Debug.Log(player.GetSelectedLevel());
        Debug.Log(selectLevel);
        bombData = JsonUtility.FromJson<BombData>(selectLevel);
        bombData.level_name = player.GetSelectedLevel();

    }
    */
    public void UpdateNewBombLogicDataLevel()
    {
        var player = PlayerManager.Instance;
        //dialogueList = JsonUtility.FromJson<DialogueList>(jsonFile.text);
        string selectLevel = ExtractLevelSet(jsonBombLogicDataFile.text, player.GetSelectedLevel());
        Debug.Log(player.GetSelectedLevel());
        Debug.Log(selectLevel);
        bombLogicData = JsonUtility.FromJson<BombLogicData>(selectLevel);
        bombLogicData.level_name = player.GetSelectedLevel();

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

    string ExtractLevelSet(string jsonText, string levelSetName)
    {
        // Find the index of the levelSetName
        int startIndex = jsonText.IndexOf($"\"{levelSetName}\"");
        if (startIndex != -1)
        {
            // Find the starting index of the object
            int startArray = jsonText.IndexOf("{", startIndex);
            if (startArray != -1)
            {
                // Use a counter to properly find the matching closing brace for nested objects
                int braceCount = 1; // We found one '{' so we start counting from 1
                int endArray = startArray + 1;

                while (endArray < jsonText.Length && braceCount > 0)
                {
                    // Look for opening and closing braces
                    if (jsonText[endArray] == '{')
                    {
                        braceCount++;
                    }
                    else if (jsonText[endArray] == '}')
                    {
                        braceCount--;
                    }
                    endArray++;
                }

                // Extract the level JSON
                string levelJson = jsonText.Substring(startArray, endArray - startArray);
                return "{\"level\":" + levelJson + "}";
            }
        }

        // Log an error and return null if the level set was not found
        Debug.LogError("Level set not found: " + levelSetName);
        return null;
    }
}
