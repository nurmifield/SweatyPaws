using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;


public class MyObject
{
    public string bombPart;
    public string correctTool;
    public bool defused;
}



public class ToolNeeded
{
    public string tool;
    public string part;

}
public class CheckCorrectTool : MonoBehaviour
{
    
    public GameObject action;
    public MyObject[] objects = new MyObject[7];
    public ToolNeeded[] correctTools= new ToolNeeded[4];
    public bool toolCorrect;

    // Start is called before the first frame update
    void Start()
    {
      
        /*
        objects[0] = new MyObject() { bombPart = alarm.name, correctTool = "tassu", defused = false };
        objects[1] = new MyObject() { bombPart = powerSource.name, correctTool = "tassu", defused = false };
        objects[2] = new MyObject() { bombPart = charge.name, correctTool = "tassu", defused = false };
        objects[3] = new MyObject() { bombPart = wirePositiveAP.name, correctTool = "pihdit", defused = false };
        objects[4] = new MyObject() { bombPart = wireNegativeAP.name, correctTool = "pihdit", defused = false };
        objects[5] = new MyObject() { bombPart = wirePositiveAC.name, correctTool = "pihdit", defused = false };
        objects[6] = new MyObject() { bombPart = wireNegativeAC.name, correctTool = "pihdit", defused = false };
        */
       

        correctTools[0] = new ToolNeeded() { tool = "tassu", part = "alarm" };
        correctTools[1] = new ToolNeeded() { tool = "tassu", part = "power" };
        correctTools[2] = new ToolNeeded() { tool = "tassu", part = "charge" };
        correctTools[3] = new ToolNeeded() { tool = "pihdit", part = "wire" };

    }

    // Update is called once per frame
    void Update()
    {
        if (action)
        {
            toolCorrect = CheckTool(correctTools, action);
            GetComponent<CheckOrder>().actionCheck = action;
            GetComponent<CheckOrder>().correctTool=toolCorrect;
            Debug.Log("Työkalusi on: " + toolCorrect);
            action = null;
        }
        
    }

    bool CheckTool(ToolNeeded[] tool , GameObject action )
    {

        string playerTool=GetComponent<Player>().tool;
        bool correctTool = false;
        for (int i=0; i < tool.Length;i++)
        {
            if (action.tag== tool[i].part && playerTool == tool[i].tool)
            {
                correctTool = true;
            }
        }

        return correctTool;
    }

}
