using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public TextAsset jsonFile;
    public TextAsset jsonPlayerFile;
    public DialogueList dialogueList = new DialogueList();
    public Player player= new Player();


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
        public DialogueParts[] scene_1;
        public DialogueParts[] scene_2;
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
        dialogueList = JsonUtility.FromJson<DialogueList>(jsonFile.text);
        player=JsonUtility.FromJson<Player>(jsonPlayerFile.text);
        
    }

}
