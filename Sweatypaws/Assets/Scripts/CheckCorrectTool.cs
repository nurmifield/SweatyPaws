using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;





public class ToolNeeded
{
    public string tool;
    public string part;

}
public class CheckCorrectTool : MonoBehaviour
{
    
    public GameObject action;
    public ToolNeeded[] correctTools= new ToolNeeded[4];
    public bool toolCorrect;

    // Start is called before the first frame update
    void Start()
    {

        correctTools[0] = new ToolNeeded() { tool = "tassu", part = "alarm" };
        correctTools[1] = new ToolNeeded() { tool = "tassu", part = "power" };
        correctTools[2] = new ToolNeeded() { tool = "tassu", part = "charge" };
        correctTools[3] = new ToolNeeded() { tool = "pihdit", part = "wire" };

    }

    public void SetAction(GameObject newAction)
    {
        toolCorrect = CheckTool(correctTools, newAction);
        GetComponent<CheckOrder>().SetToolAndAction(toolCorrect,newAction);
    }

    bool CheckTool(ToolNeeded[] tool , GameObject action )
    {

        string playerTool=GetComponent<Player>().tool;
        bool correctTool = false;
        for (int i=0; i < tool.Length;i++)
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
