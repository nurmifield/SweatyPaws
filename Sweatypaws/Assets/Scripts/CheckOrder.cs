using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class CheckOrder : MonoBehaviour
{
    public List<BombData.CorrectOrder> correctOrder;
    public GameObject action;
    public bool correctTool = false;
    public GameObject actionCheck;
    public Score score;
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
                    correctOrder = jsonReader.bombData.level.correct_order;
                }
            }
        }
    }

    public void SetToolAndAction(bool correctTool , GameObject actionCheck)
    {
        Debug.Log("Pelaaja käyttää oikeata työkalua: "+correctTool);
        bool correctMove = CheckCorretMove(actionCheck, correctOrder,correctTool);
        if (correctMove)
        {
            //Tarkistetaan voitettiinko peli 
            if (CountSectionsCleared(correctOrder) == correctOrder.Count)
            {
                // TÄNNE PELIN VOITTO HOMMAT
                GetComponent<GameOverScreen>().YouWinScreenManage();
                Debug.Log("VOITIT PELIN!");
            }
            else
                // PELI JATKUU ETEENPÄIN
                Debug.Log("PELI JATKUU!");
        }
        else
        {
            //Tarkistetaan onko peli ohi
            GetComponent<CheckFailure>().SetSections(CountSectionsCleared(correctOrder));
        }
    }
    bool CheckCorretMove(GameObject action, List<BombData.CorrectOrder> order, bool correctTool)
    {
        bool correctMove = false;
        bool firstSection = false;

        //Debug.Log("Funkkarissa:" + order.Length);
        for (int i = 0; i < order.Count; i++)
        {
            if (order[i].section_cleared==false && firstSection==false)
            {
                firstSection = true;
                
                for (int ii = 0; ii < order[i].parts.Length; ii++)
                {
                    if (action.name == order[i].parts[ii] && correctTool)
                    {
                        order[i].RemovePart(order[i].parts[ii]);
                        correctMove = true;
                        action.SetActive(false);
                        score.AddScore();
                        Debug.Log("parts pituus" + order[i].parts.Length);
                    }else if (action.name == order[i].parts[ii] && !correctTool)
                    {
                        score.MinusScore();
                        correctMove = true;
                        Debug.Log("Väärä työkalu oikea osa PENALTYÄ");
                    }
                    if (order[i].parts.Length == 0)
                    {
                        Debug.Log("Vaihe "+ i + " suoritettu");
                        order[i].section_cleared = true;
                    }
                }
                
            }
        }
        return correctMove;
    }
    int CountSectionsCleared(List<BombData.CorrectOrder> order)
    {
        int count = 0;
        for(int i=0; i < order.Count; i++)
        {
            if (order[i].section_cleared==true)
            {
                count++;
            }
        }
        return count;
    }

}
