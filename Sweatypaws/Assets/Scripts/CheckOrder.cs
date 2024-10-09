using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MyCorrectOrder
{
    public string[] parts;  // Array of names
    public bool sectionCleared;

    // Method to remove a name from the array
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
public class CheckOrder : MonoBehaviour
{
    public MyCorrectOrder[] correctOrder = new MyCorrectOrder[5];
    public GameObject action;
    public bool correctTool = false;
    public GameObject actionCheck;
 
    GameObject alarm;
    GameObject powerSource;
    GameObject charge;
    GameObject wirePositiveAP;
    GameObject wireNegativeAP;
    GameObject wirePositiveAC;
    GameObject wireNegativeAC;
    // Start is called before the first frame update
    void Start()
    {
        alarm = GameObject.Find("Alarm");
        powerSource = GameObject.Find("PowerSource");
        charge = GameObject.Find("Charge");
        wirePositiveAP = GameObject.Find("WirePositiveAP");
        wireNegativeAP = GameObject.Find("WireNegativeAP");
        wirePositiveAC = GameObject.Find("WirePositiveAC");
        wireNegativeAC = GameObject.Find("WireNegativeAC");

        correctOrder[0] = new MyCorrectOrder() {  parts = new string[2] { wirePositiveAC.name, wireNegativeAC.name }, sectionCleared = false };
        correctOrder[1] = new MyCorrectOrder() { parts = new string[1] { charge.name }, sectionCleared = false };
        correctOrder[2] = new MyCorrectOrder() { parts = new string[2] { wirePositiveAP.name, wireNegativeAP.name }, sectionCleared = false };
        correctOrder[3] = new MyCorrectOrder() { parts = new string[1] { powerSource.name }, sectionCleared = false };
        correctOrder[4] = new MyCorrectOrder() { parts = new string[1] { alarm.name }, sectionCleared = false };
    }

    public void SetToolAndAction(bool correctTool , GameObject actionCheck)
    {
        Debug.Log("Pelaaja käyttää oikeata työkalua: "+correctTool);
        bool correctMove = CheckCorretMove(actionCheck, correctOrder,correctTool);
        if (correctMove)
        {
            //Tarkistetaan voitettiinko peli 
            if (CountSectionsCleared(correctOrder) == correctOrder.Length)
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
    bool CheckCorretMove(GameObject action, MyCorrectOrder[] order, bool correctTool)
    {
        bool correctMove = false;
        bool firstSection = false;

        //Debug.Log("Funkkarissa:" + order.Length);
        for (int i = 0; i < order.Length; i++)
        {
            if (order[i].sectionCleared==false && firstSection==false)
            {
                firstSection = true;
                
                for (int ii = 0; ii < order[i].parts.Length; ii++)
                {
                    if (action.name == order[i].parts[ii] && correctTool)
                    {
                        order[i].RemovePart(order[i].parts[ii]);
                        correctMove = true;
                        action.SetActive(false);
                        GetComponent<Score>().AddScore();
                        Debug.Log("parts pituus" + order[i].parts.Length);
                    }else if (action.name == order[i].parts[ii] && !correctTool)
                    {
                        GetComponent<Score>().MinusScore();
                        correctMove = true;
                        Debug.Log("Väärä työkalu oikea osa PENALTYÄ");
                    }
                    if (order[i].parts.Length == 0)
                    {
                        Debug.Log("Vaihe "+ i + " suoritettu");
                        order[i].sectionCleared = true;
                    }
                }
                
            }
        }
        return correctMove;
    }
    int CountSectionsCleared(MyCorrectOrder[] order)
    {
        int count = 0;
        for(int i=0; i < order.Length; i++)
        {
            if (order[i].sectionCleared==true)
            {
                count++;
            }
        }
        return count;
    }

}
