using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;






public class CheckCorrectTool : MonoBehaviour
{
    
    public GameObject action;
    public List<BombData.ToolNeeded> correctTools;
    public bool toolCorrect;
    public JsonReader jsonReader;

    // Start is called before the first frame update
    void Start()
    {
        {
            GameObject reader = GameObject.Find("Reader");
            if (reader != null)
            {
                jsonReader = reader.GetComponent<JsonReader>();
                if (jsonReader != null)
                {
                    correctTools = jsonReader.bombData.level.tool_needed;
                }
            }
        }


    }

    public void SetAction(GameObject newAction)
    {
        toolCorrect = CheckTool(correctTools, newAction);
        GetComponent<CheckOrder>().SetToolAndAction(toolCorrect,newAction);
    }

    bool CheckTool(List<BombData.ToolNeeded> tool , GameObject action )
    {

        string playerTool=GetComponent<Player>().tool;
        bool correctTool = false;
        for (int i=0; i < tool.Count;i++)
        {
            if (action.tag== tool[i].part && playerTool == tool[i].tool)
            {
                Debug.Log(" PELAAJA käyttäää "+playerTool);
                correctTool = true;
            }
        }

        return correctTool;
    }

}
