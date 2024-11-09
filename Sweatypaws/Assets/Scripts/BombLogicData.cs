using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BombData;
[System.Serializable]
public class BombLogicData
{
    public string level_name;
    public BombSetup level;

    [System.Serializable]
    public class BombSetup
    {
        public int current_stage;
        public int win_points;
        public int time;
        public int win_condition;
        public string bomb_name;
        public List<Stages> stages;

        public void IncreaseCurrentStage(Stages stages)
        {
            current_stage+=stages.increase_stage_by;
        }

        public void IncreaseCurrentWinPoints(Stages stages)
        {
            win_points += stages.increase_win_points_by;
        }
    }
    [System.Serializable]
    public class Stages
    {
        public string stage_name;
        public int current_stage_index;
        public FailureCondition failure_condition;
        public bool section_cleared;
        public int increase_stage_by;
        public int increase_win_points_by;
        public List<StageTools> stage_tools;
        public StageParts stage_parts;

        public void SetSectionCleared()
        {
            section_cleared = true;
        }

    }
    [System.Serializable]
    public class FailureCondition
    {
        public int current_stage_index;
        public bool exception;
        public ExecptionCondition exception_condition;
    }
    [System.Serializable]
    public class ExecptionCondition
    {
        public int stage_index;
        public string part;
        public string tool;
        public string penalty;
    }
    [System.Serializable]
    public class StageTools
    {
        public string tool;
        public string part;
        public List<WrongTools> wrong_tools;
        
    }
    [System.Serializable]
    public class WrongTools
    {
        public string tool;
        public string penalty;
    }
    [System.Serializable]
    public class StageParts
    {
        public string[] parts;

        public void RemovePart(string partToRemove)
        {
            // Create a list to dynamically handle the removal
            List<string> tempPartList = new List<string>(parts); // Convert array to a list

            // Remove the name from the list
            if (tempPartList.Contains(partToRemove))
            {
                tempPartList.Remove(partToRemove);
            }

            // Convert the list back to an array
            parts = tempPartList.ToArray();
        }
    }
}
