using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerData;

[System.Serializable]
public class PlayerData
{
        public int version;
        public int level;
        public bool isSoundOn = true;
        public bool tutorial_done;
        public int dialogue_level;
        public List<DialogueProgress> dialogue_progress;
        public List<LevelProgress> level_progress;
        public string selectedLevel = "none";

    public bool LevelExists(string levelName)
    {
        return level_progress.Exists(level => level.level_name == levelName);
    }

    public bool DialogueExist(string dialogueName)
    {
        return dialogue_progress.Exists(dialogue => dialogue.dialogue_name == dialogueName);
    }

    [System.Serializable]
    public class DialogueProgress
    {
        public string dialogue_name;
        public int dialogue_index;
        public int level_index;
        public string selected_level;
        public bool watched;

        public DialogueProgress(string dialogueName , int dialogueIndex , int levelIndex,string selectedLevel)
        {
            this.dialogue_name = dialogueName;
            this.dialogue_index = dialogueIndex;
            this.level_index = levelIndex;
            this.selected_level = selectedLevel;
            this.watched = false;
        }
    }
    [System.Serializable]
    public class LevelProgress
    {
        public string level_name;
        public int level_index;
        public bool complete;
        public int score;
        public int max_score;
        public bool collection;

        public LevelProgress(string levelName , int levelIndex , int maxScore)
        {
            this.level_name = levelName;
            this.level_index = levelIndex;
            this.score = 0;
            this.complete = false;
            this.max_score = maxScore;
            this.collection = false;
    }
    }
}
