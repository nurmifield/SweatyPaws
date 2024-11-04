using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BombData
{
    public string level_name;
    public BombSettings level;

    [System.Serializable]
    public class BombSettings
    {
        
        public int time;
        public int must_be_cleared;
        public string bomb_name;
        public List<CorrectOrder> correct_order;
        public List<ToolNeeded> tool_needed;
    }


    [System.Serializable]
    public class CorrectOrder
    {
        public bool section_cleared;
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
    [System.Serializable]
    public class ToolNeeded
    {
        public string tool;
        public string part;
    }

 
}
