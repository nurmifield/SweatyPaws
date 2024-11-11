using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static BombLogicData;

public class GameActionManager : MonoBehaviour
{
    private JsonReader jsonReader;
    public List<BombLogicData.Stages> levelStages;
    public BombLogicData.BombSetup bombSetup;
    public Score score;
    public PenaltyManager penaltyManager;
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
                    levelStages = jsonReader.bombLogicData.level.stages;
                    bombSetup = jsonReader.bombLogicData.level;
                }
            }
        }


    }
    public void CheckStagePart(GameObject action)
    {
        for (int i =0; i< levelStages.Count;i++)
        {
            for (int ii=0;ii < levelStages[i].stage_parts.parts.Length;ii++)
            {
                if (action.name == levelStages[i].stage_parts.parts[ii])
                {
                    //L�hetet��n tieto miss� stagella kyseinen osa on
                    Debug.Log("Stage" + levelStages[i].stage_name + " Contains " + action.name + " part");
                    ChekcCurrentStageIndex(levelStages[i].stage_name, action);
                    
                    break;
                }
            }
        }
    }
    public void ChekcCurrentStageIndex(string currentStage, GameObject action)
    {
        for (int i =0;i < levelStages.Count; i++)
        {
            if (levelStages[i].stage_name == currentStage)
            {
                if (bombSetup.current_stage >= levelStages[i].current_stage_index)
                {
                    //Tarkistus onko current index sama tai isompi, kuin stagessa oleva index
                    //T�ss� kohtaa isompi tai sama voidaan edet� tarkistamaan mik� ty� kalu kyseess�, koska ei suoraan ole fail
                    //Tarkistetaan onko oikea ty�kalu k�yt�ss�.
                    Debug.Log("Current stage index is " + bombSetup.current_stage + "levels stage current index is " + levelStages[i].current_stage_index);
                    CheckCorrectPartTool(action , levelStages[i]);
                    
                    break;
                }
                else
                {
                    // T�nne tehd� functio , joka huolehtii siit�, ett� current index oli pienempi eli johtaa siihen ,ett� fail ellei poikkeusta tilanteeseen ole
                    Debug.Log("Current stage index is  smaller than level stage current indec");
                    CheckExecptionCondition(levelStages[i],action);
                    
                }
            }
        }
    }

    public void CheckCorrectPartTool(GameObject action , BombLogicData.Stages stage)
    {
        string playerTool = GetComponent<Player>().tool;
        for (int i = 0; i < stage.stage_tools.Count; i++)
        {
            if (action.tag == stage.stage_tools[i].part && playerTool == stage.stage_tools[i].tool)
            {
                //T�ss� olisi oikea ty�kalu ja se johtaisi jatkoon
                Debug.Log("Player is using: " + playerTool + " and part tag is: " + action.tag);
                IncreasePoints(stage,action);
                
                break;
            }
            else if (action.tag == stage.stage_tools[i].part && playerTool != stage.stage_tools[i].tool)
            {
                for (int ii=0; ii < stage.stage_tools[i].wrong_tools.Count; ii++)
                {
                    if (playerTool == stage.stage_tools[i].wrong_tools[ii].tool)
                    {
                        //T��ll� tarkastellaan millainen penalty tulee v��rin k�ytetyst� ty�kalusta
                        Debug.Log("Player is using wrong tool, penalty check must happen");
                        penaltyManager.CheckPenalty(stage.stage_tools[i].wrong_tools[ii].penalty);
                        
                        break;
                    }
                }
            }
            
        }
    }
    public void IncreasePoints(BombLogicData.Stages stage, GameObject action)
    {
        stage.stage_parts.RemovePart(action.name);
        action.SetActive(false);
        score.AddScore();
        if (stage.stage_parts.parts.Length == 0)
        {
            bombSetup.IncreaseCurrentStage(stage);
            bombSetup.IncreaseCurrentWinPoints(stage);
        }
        if (bombSetup.win_condition == bombSetup.win_points)
        {
            GetComponent<GameOverScreen>().YouWinScreenManage();
            Debug.Log("VOITIT PELIN!");
        }

    }

    public void CheckExecptionCondition(BombLogicData.Stages stage, GameObject action)
    {
        int exectionStageIndex = stage.failure_condition.exception_condition.stage_index;
        int currentStageIndex = bombSetup.current_stage;
        string actionPartName = action.name;
        string exeptionPartName = stage.failure_condition.exception_condition.part;
        string playerTool = GetComponent<Player>().tool;
        string exeptionTool = stage.failure_condition.exception_condition.tool;
        string penalty = stage.failure_condition.exception_condition.penalty;

        // T�T� OSIOTA PIT�� VIEL� HIENO S��T�� EI OTA KAIKKIA PENALTYJ� NIIN HYVIN 
        if (stage.failure_condition.exception)
        {
            if ( exectionStageIndex == currentStageIndex && actionPartName == exeptionPartName && playerTool == exeptionTool )
            {
                // Laitetaan t�h�n sitten penalty asia
                Debug.Log("Penalty� failuren alla tietty ty�kalu execption true");
                penaltyManager.CheckPenalty(penalty);

            }
            else if (bombSetup.current_stage > stage.failure_condition.current_stage_index)
            {
                Debug.Log("current stage isompi pisteet pois execption true");
                penaltyManager.CheckPenalty("points");
            }
            else
            {
                Debug.Log("Failure tuli penaltyst� exeption true");
                penaltyManager.CheckPenalty("fail"); 
            }
        }
        else
        {
            if (bombSetup.current_stage > stage.failure_condition.current_stage_index)
            {
                Debug.Log("current stage isompi pisteet pois exception false");
                penaltyManager.CheckPenalty("points");
            }
            else
            {
                Debug.Log("current stage pienempi failure execption false");
                penaltyManager.CheckPenalty("fail");
            }
        }
    }

}