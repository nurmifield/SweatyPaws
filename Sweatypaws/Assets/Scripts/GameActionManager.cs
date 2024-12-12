using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BombLogicData;
//using static BombLogicData;

public class GameActionManager : MonoBehaviour
{
    private JsonReader jsonReader;
    public Player playerScript;
    public List<BombLogicData.Stages> levelStages;
    public BombLogicData.BombSetup bombSetup;
    public Score score;
    public PenaltyManager penaltyManager;
    public GameObject prefab;
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
                    GameObject bomb = Resources.Load<GameObject>(jsonReader.bombLogicData.level.bomb_name);
                    prefab = Instantiate(bomb);
                    Vector2 originalPostion = bomb.transform.position;

                    prefab.transform.position = originalPostion;
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
                    //Lähetetään tieto missä stagella kyseinen osa on
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
                    //Tässä kohtaa isompi tai sama voidaan edetä tarkistamaan mikä työ kalu kyseessä, koska ei suoraan ole fail
                    //Tarkistetaan onko oikea työkalu käytössä.
                    Debug.Log("Current stage index is " + bombSetup.current_stage + "levels stage current index is " + levelStages[i].current_stage_index);
                    CheckCorrectPartTool(action , levelStages[i]);
                    
                    break;
                }
                else
                {
                    // Tänne tehdä functio , joka huolehtii siitä, että current index oli pienempi eli johtaa siihen ,että fail ellei poikkeusta tilanteeseen ole
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
            if (action.tag == stage.stage_tools[i].part && playerTool == stage.stage_tools[i].tool && action.name == stage.stage_tools[i].action_part_name)
            {
                //Tässä olisi oikea työkalu ja se johtaisi jatkoon
                Debug.Log("Player is using: " + playerTool + " and part tag is: " + action.tag);
                CheckCorrectToolAction(stage.stage_tools[i],action, stage);

                break;
            }
            else if (action.tag == stage.stage_tools[i].part && playerTool != stage.stage_tools[i].tool)
            {
                for (int ii=0; ii < stage.stage_tools[i].wrong_tools.Count; ii++)
                {
                    if (playerTool == stage.stage_tools[i].wrong_tools[ii].tool)
                    {
                        //Täällä tarkastellaan millainen penalty tulee väärin käytetystä työkalusta
                        Debug.Log("Player is using wrong tool, penalty check must happen");
                        penaltyManager.CheckPenalty(stage.stage_tools[i].wrong_tools[ii].penalty);
                        
                        break;
                    }
                }
            }
            
        }
    }

    public void CheckCorrectToolAction(BombLogicData.StageTools stageTools , GameObject action, BombLogicData.Stages stage)
    {
        
        if (stageTools.correct_tool_action.action == "remove")
        {
            if (stageTools.correct_tool_action.broken_parts.Length > 0)
            {
                for (int i = 0; i < stageTools.correct_tool_action.broken_parts.Length; i++)
                {
                    GameObject brokenPart = FindInactiveObjectByName(prefab.transform , stageTools.correct_tool_action.broken_parts[i]);
                    brokenPart.SetActive(true);
                    
                }
            }
            
            CheckAnimation(stageTools);
            action.SetActive(false);
            IncreasePoints(stage, action);
        }
        else if (stageTools.correct_tool_action.action == "remove_extra")
        {
            if (stageTools.correct_tool_action.broken_parts.Length > 0)
            {
                for (int i = 0; i < stageTools.correct_tool_action.broken_parts.Length; i++)
                {
                    GameObject brokenPart = FindInactiveObjectByName(prefab.transform, stageTools.correct_tool_action.broken_parts[i]);
                    brokenPart.SetActive(true);
                }
            }
            GameObject failurePart = FindInactiveObjectByName(prefab.transform,stageTools.correct_tool_action.failure_part);
            failurePart.SetActive(false);
            CheckAnimation(stageTools);
            action.SetActive(false);
            IncreasePoints(stage, action);
        }
        else if (stageTools.correct_tool_action.action == "activate")
        {
            CheckAnimation(stageTools);
            if (stageTools.correct_tool_action.penalty_type != "none")
            {
                penaltyManager.CheckPenalty(stageTools.correct_tool_action.penalty_type);
            }
            else
            {
                IncreasePoints(stage, action);
            }
        }
        else if (stageTools.correct_tool_action.action == "fail")
        {
            if (stageTools.correct_tool_action.broken_parts.Length > 0)
            {
                for (int i = 0; i < stageTools.correct_tool_action.broken_parts.Length; i++)
                {
                    GameObject brokenPart = FindInactiveObjectByName(prefab.transform, stageTools.correct_tool_action.broken_parts[i]);
                    brokenPart.SetActive(true);
                }
            }
            CheckFailureAnimation(stageTools);
            action.SetActive(false);
        }else if (stageTools.correct_tool_action.action == "open_monitor")
        {
            GameObject monitor = FindInactiveObjectByName(prefab.transform, "ScreenObject");
            monitor.SetActive(true);
            playerScript.EquipTool("none");
            
        }
 

    }
    IEnumerator PlayAnimationAndWait(Animator animator, string triggerName)
    {
        // Set the trigger to start the animation
        animator.SetTrigger(triggerName);

        // Wait until the Animator transitions to the animation state to get its length
        yield return null; // Ensure a frame has passed so the animation state can update
        // Wait for the animation length
        yield return new WaitForSeconds(1f);
        penaltyManager.CheckPenalty("fail");
        Debug.Log("Animation has ended!");
    }

    public void CheckAnimation(BombLogicData.StageTools stageTools)
    {
        if (stageTools.correct_tool_action.animation.set_trigger != "none")
        {
            GameObject animationPart = FindInactiveObjectByName(prefab.transform, stageTools.correct_tool_action.animation.animation_part);
            Animator animator = animationPart.GetComponent<Animator>();
            animator.SetTrigger(stageTools.correct_tool_action.animation.set_trigger);
            
        }
    }
    public void CheckFailureAnimation(BombLogicData.StageTools stageTools)
    {
        if (stageTools.correct_tool_action.animation.set_trigger != "none")
        {
            GameObject animationPart = FindInactiveObjectByName(prefab.transform, stageTools.correct_tool_action.animation.animation_part);
            Animator animator = animationPart.GetComponent<Animator>();
            StartCoroutine(PlayAnimationAndWait(animator, stageTools.correct_tool_action.animation.set_trigger));

        }
        else
        {
            penaltyManager.CheckPenalty("fail");
        }
    }
    GameObject FindInactiveObjectByName(Transform parent, string name)
    {
        if (parent.name == name) return parent.gameObject;

        foreach (Transform child in parent)
        {
            GameObject result = FindInactiveObjectByName(child, name);
            if (result != null) return result;
        }
        return null;
    }

    public void IncreasePoints(BombLogicData.Stages stage, GameObject action)
    {
        stage.stage_parts.RemovePart(action.name);
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

        // TÄTÄ OSIOTA PITÄÄ VIELÄ HIENO SÄÄTÄÄ EI OTA KAIKKIA PENALTYJÄ NIIN HYVIN 
        if (stage.failure_condition.exception)
        {
            if ( exectionStageIndex == currentStageIndex && actionPartName == exeptionPartName && playerTool == exeptionTool )
            {
                // Laitetaan tähän sitten penalty asia
                Debug.Log("Penaltyä failuren alla tietty työkalu execption true");
                penaltyManager.CheckPenalty(penalty);

            }
            else if (bombSetup.current_stage > stage.failure_condition.current_stage_index)
            {
                Debug.Log("current stage isompi pisteet pois execption true");
                penaltyManager.CheckPenalty("points");
            }
            else
            {
                Debug.Log("Failure tuli penaltystä exeption true");
                penaltyManager.CheckPenalty("fail"); 
            }
        }
        else
        {
            if (bombSetup.current_stage >= stage.failure_condition.current_stage_index)
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

    public void IncreasePointsAndCurrentStage(int stageLevel , int points)
    {
        bombSetup.IncreaseCurrentStageAndPoints(stageLevel, points);
        score.AddScore();
    }

    public void IncreaseOnlyStage(int stageLevel)
    {
        bombSetup.IncreaseCurrentStageOnly(stageLevel);
    }
    public void DecreaseStage(int stageLevel)
    {
        bombSetup.DecreaseCurrentStage(stageLevel);
    }

    public void ActivateObject(string objectName)
    {
        GameObject gameObject = FindInactiveObjectByName(prefab.transform , objectName);
        gameObject.SetActive(true);
    }

    public void DeactivateObject(string objectName)
    {
        GameObject gameObject = FindInactiveObjectByName(prefab.transform, objectName);
        gameObject.SetActive(false);
    }

    public GameObject FindGameObject(string objectName)
    {
        return FindInactiveObjectByName(prefab.transform,objectName);
    }
}
