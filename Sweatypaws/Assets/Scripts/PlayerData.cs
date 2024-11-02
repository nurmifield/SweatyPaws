using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{

        public int level;
        public int dialogue_level;
        public List<DialogueProgress> dialogue_progress;
        public List<LevelProgress> level_progress;
 
    [System.Serializable]
    public class DialogueProgress
    {
        public string dialogue_name;
        public int dialogue_index;
        public int level_index;
        public bool watched;
    }
    [System.Serializable]
    public class LevelProgress
    {
        public string level_name;
        public int level_index;
        public bool complete;
        public int score;
    }
}
