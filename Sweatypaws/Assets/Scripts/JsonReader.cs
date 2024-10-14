using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    public TextAsset jsonFile;
    public DialogueList dialogueList = new DialogueList();


    [System.Serializable]
    public class DialogueParts
    {
        public string character_name;
        public string dialog;
    }
    [System.Serializable]
    public class DialogueList
    {
        public DialogueParts[] scene_1;
        public DialogueParts[] scene_2;
    }

   
    // Start is called before the first frame update
    void Awake()
    {
        dialogueList = JsonUtility.FromJson<DialogueList>(jsonFile.text); 
    }

}
