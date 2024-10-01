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

public class MyCorrectOrder
{
    public string parent;
    public string[] child;
    public bool allDone;
}
public class CheckAction : MonoBehaviour
{
    public MyCorrectOrder[] correctOrder = new MyCorrectOrder[5];
    public GameObject action;
    public MyObject[] objects = new MyObject[7];
    string actionName = "";

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

        objects[0] = new MyObject() { bombPart = alarm.name, correctTool = "tassu", defused = false };
        objects[1] = new MyObject() { bombPart = powerSource.name, correctTool = "tassu", defused = false };
        objects[2] = new MyObject() { bombPart = charge.name, correctTool = "tassu", defused = false };
        objects[3] = new MyObject() { bombPart = wirePositiveAP.name, correctTool = "pihdit", defused = false };
        objects[4] = new MyObject() { bombPart = wireNegativeAP.name, correctTool = "pihdit", defused = false };
        objects[5] = new MyObject() { bombPart = wirePositiveAC.name, correctTool = "pihdit", defused = false };
        objects[6] = new MyObject() { bombPart = wireNegativeAC.name, correctTool = "pihdit", defused = false };

        correctOrder[0] = new MyCorrectOrder() { parent = "Wires", child = new string[2] { wirePositiveAC.name, wireNegativeAC.name }, allDone=false };
        correctOrder[1] = new MyCorrectOrder() { parent = "Bomb", child = new string[1] { charge.name }, allDone = false };
        correctOrder[2] = new MyCorrectOrder() { parent = "Wires", child = new string[2] { wirePositiveAP.name, wireNegativeAP.name },allDone=false };
        correctOrder[3] = new MyCorrectOrder() { parent = "Bomb", child = new string[1] { powerSource.name } , allDone = false };
        correctOrder[4] = new MyCorrectOrder() { parent = "Bomb", child = new string[1] { alarm.name } ,allDone = false };

    }

    // Update is called once per frame
    void Update()
    {
        //Ei aleta tekemään mitään ennenkuin on clikattuna jotain ja saatu objecti takaisin
        if (action == null)
            return;
        //Otetaan objectista actionin nimi
        actionName = action.name;

        //EI välttämättä tarvitsisi , mutta katsotaan objectin nimi
        if (actionName != "")
        {
            //otetaan pelaajalta työkalu
            string usedTool = GetComponent<Player>().tool;

            //Otetaan objectin parent huomioon (bomb ja wires)
            Transform parentCheck = action.transform.parent;
            Debug.Log("Action name saatu: " + actionName);

            //Loopataan objectien pituuden verran
            for (int i = 0; i < objects.Length; i++)
            {
                //Jos toiminnan nimi löytyy pommin osista mitä clikattuna
                if (actionName == objects[i].bombPart)
                {
                    Debug.Log("ActionName on sama kuin pommipart ");

                    // katsotaan onko oikea työkalu ollut käytössä , kun kosketeltu osia
                    if (usedTool == objects[i].correctTool)
                    {
                        //katsotaan parent tagin kautta 
                        Debug.Log("Tools on myös oikea ");
                        for (int j=0;j<correctOrder.Length;j++) {

                            if (parentCheck.tag == correctOrder[j].parent && !correctOrder[j].allDone)
                            {
                                Debug.Log("ParentCheck Tag homma toimii");
                                //Loopataan oikean järjestyksen parent  child osat
                                for (int ii = 0; ii < correctOrder[j].child.Length; ii++)
                                {
                                    //katsotaan onko action mikä tehty sama kuin oikean orderin ensimmäinen kohta eli löytyykö child osiosta sitä
                                    if (correctOrder[j].child[ii] == actionName)
                                    {
                                        Debug.Log("Oikean työkalu oikea asia, hyvin menee");
                                        objects[i].defused = true;

                                        //EI TEE TÄTÄ OSIOTA MIKSI
                                        for (int jj=0; jj < correctOrder[j].child.Length;jj++)
                                        {
                                            if (correctOrder[j].child[jj] == objects[i].bombPart && objects[i].defused==false)
                                            {
                                                Debug.Log("Ei ole purettuna, ei mennä seuraavaan kohtaan");
                                                break;
                                            }
                                            else if(ii == correctOrder[j].child.Length)
                                            {
                                                Debug.Log("Kaikki tehty seuraava");
                                                correctOrder[j].allDone = true;
                                            }
                                        }
                                        Destroy(action);
                                        Debug.Log("Nimi: " + objects[i].bombPart + "\n" + "Tool: " + objects[i].correctTool + "//" + "Defused: " + objects[i].defused);
                                        return;
                                    }
                                    //työkalu on oikea ja jotain on tehty osalle mikä ei olekkaann järjestyksen ensimmäinen ja vituiksi meni
                                    else if (ii == correctOrder[j].child.Length)
                                    {
                                        Debug.Log("Pum pum pum ei ole oikea reitti tämä ei löydy Pum pum");
                                    }
                                }
                            }
                            else
                            {
                                Debug.Log("Oikea työkalu , väärä järjestys PUM PUM PUM");
                            }
                        }
                    }

                    else
                        Debug.Log("Väärä työkalukäytössä");
                    action = null;
                    break;
                }


            }

        }
    }

}
