using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class  MonitorData
{
    public GameObject monitorPanel;
    public GameObject[] fields;

    public MonitorData(GameObject newMonitorPanel , GameObject[] newFields )
    {
        this.monitorPanel = newMonitorPanel;
        this.fields = newFields;
    }
}

public class Level_3_Monitor : MonoBehaviour
{
    public GameObject screenObject;
    public GameObject[] monitorPanels;
    public GameObject[] passwordField;
    public GameObject[] mainMenuSection;
    public GameObject[] settingsSection;
    public GameObject[] bootOrderSection;
    public GameObject[] newBootOrderSection;
    public GameObject[] currentBootOrderSection;
    public GameObject[] selectableBootOrderSection;
    public GameObject passwordInfo;
    public string[] newBootOrder;
    public string[] currentBootOrder;
    public string[] correctBootOrder;
    public string[] selectableBootOrder;
    public string correctPassword="000000";
    public string userTypedPassword = "";
    public int monitorLevel=0;
    public int selectIndex;
    public bool moduleSelected = false;
    public int newBootOrderIndex;
    public int selectedBootOrderIndex;
    public List<MonitorData> monitorData;

    // Start is called before the first frame update
    void Start()
    {
        newBootOrder= new string[3] {"Select","Select","Select"};
        currentBootOrder = new string[3] { "Failsafe.mdl", "Sec.mdl", "Os.mdl" };
        correctBootOrder = new string[3] { "Os.mdl" , "Sec.mdl", "Failsafe.mdl" };
        selectableBootOrder = new string[3] {  "Sec.mdl", "Failsafe.mdl", "Os.mdl" };
        UpdateCurrentBootOrder(newBootOrderSection,newBootOrder);
        UpdateCurrentBootOrder(currentBootOrderSection,currentBootOrder);
        UpdateCurrentBootOrder(selectableBootOrderSection, selectableBootOrder);
        for (int i = 0; i < monitorPanels.Length; i++)
        {
            if (monitorPanels[i].name == "LockScreen")
            {
                monitorData.Add(new MonitorData(monitorPanels[i], passwordField));
            }
            else if (monitorPanels[i].name == "MainMenuScreen")
            {
                monitorData.Add(new MonitorData(monitorPanels[i], mainMenuSection));
            }else if (monitorPanels[i].name == "SettingMenuScreen")
            {
                monitorData.Add(new MonitorData(monitorPanels[i], settingsSection));
            }
            else if (monitorPanels[i].name == "BootOrderScreen")
            {
                monitorData.Add(new MonitorData(monitorPanels[i], bootOrderSection));
            }
            else if (monitorPanels[i].name == "NewBootOrderScreen")
            {
                monitorData.Add(new MonitorData(monitorPanels[i], newBootOrderSection));
            }
            else if (monitorPanels[i].name == "BootOrderSelection")
            {
                monitorData.Add(new MonitorData(monitorPanels[i], selectableBootOrderSection));
            }
            else
            {
                GameObject[] empty = new GameObject[0];
                monitorData.Add(new MonitorData(monitorPanels[i], empty));
            }
        }
        monitorData[0].monitorPanel.SetActive(true);
    }

    public void ReturnButton()
    {
         if (monitorData[0].monitorPanel.activeSelf)
            {
                TextMeshProUGUI passwordText = monitorData[0].fields[0].GetComponent<TextMeshProUGUI>();
                int userTypedPasswordLenght = userTypedPassword.Length;
                if (userTypedPasswordLenght > 0)
                {
                    userTypedPassword= userTypedPassword.Substring(0, userTypedPasswordLenght-1);
                    passwordText.text = passwordText.text.Substring(0,passwordText.text.Length - 1);
                }

            }
         else if (moduleSelected)
        {
            moduleSelected= false;
            TextMeshProUGUI selectText = monitorData[monitorLevel].fields[selectIndex].GetComponent<TextMeshProUGUI>();
            string currentSelectText = selectText.text;
            selectText.text = RemoveSelectMarkers(currentSelectText);
            selectIndex = 0;

        }
         else if (!moduleSelected)
        {
            ReturnUpdateMonitorLevel(monitorData[monitorLevel].monitorPanel.name , monitorLevel);
        }
        

        
    }
    public void EnterButton()
    {
        if (monitorData[0].monitorPanel.activeSelf)
        {
            TextMeshProUGUI passwordText = monitorData[0].fields[0].GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI passwordInfoText = passwordInfo.GetComponent<TextMeshProUGUI>();
            if (userTypedPassword == correctPassword)
            {
                monitorData[0].monitorPanel.SetActive(false);
                monitorData[1].monitorPanel.SetActive(true);
                monitorLevel = 1;
                userTypedPassword ="";
                passwordText.text = userTypedPassword;
                passwordInfoText.text = "";
            }
            else
            {
                userTypedPassword = "";
                passwordText.text = userTypedPassword;
                passwordInfoText.text = "Incorrect";

            } 
        }else if (!moduleSelected)
        {
            if (monitorData[monitorLevel].fields.Length == 0)
            {
                Debug.Log("Takaisin main menu ei tutkittavaa");
                monitorData[monitorLevel].monitorPanel.SetActive(false);
                monitorData[1].monitorPanel.SetActive(true);
                monitorLevel = 1;
            }
            else
            {
                moduleSelected = true;
                selectIndex = 0;
                TextMeshProUGUI selectText = monitorData[monitorLevel].fields[selectIndex].GetComponent<TextMeshProUGUI>();
                string currentSelectText = selectText.text;
                selectText.text = AddSelectMarkers(currentSelectText, '-');
            }
            

        }else if (moduleSelected)
        {
            moduleSelected= false;
            TextMeshProUGUI selectText = monitorData[monitorLevel].fields[selectIndex].GetComponent<TextMeshProUGUI>();
            string currentSelectText = selectText.text;
            selectText.text = RemoveSelectMarkers(currentSelectText);
            UpdateMonitorLevel(monitorData[monitorLevel].fields[selectIndex].name,monitorLevel,selectIndex);
            selectIndex =0;


        }
    }
    public void DownButton()
    {
        int checkSelectIndex = selectIndex;
            checkSelectIndex++;
        if (moduleSelected && checkSelectIndex < monitorData[monitorLevel].fields.Length)
        {
            TextMeshProUGUI selectText = monitorData[monitorLevel].fields[selectIndex].GetComponent<TextMeshProUGUI>();
            string currentSelectText = selectText.text;
            selectText.text = RemoveSelectMarkers(currentSelectText);
            selectIndex++;
            selectText = monitorData[monitorLevel].fields[selectIndex].GetComponent<TextMeshProUGUI>();
            currentSelectText = selectText.text;
            selectText.text = AddSelectMarkers(currentSelectText, '-');


        }
    }

    public void UpButton()
    {
        Debug.Log("Up button tapahtuu");
        if (moduleSelected && selectIndex > 0)
        {
            Debug.Log("Up button tapahtuu IF sisällä");
            TextMeshProUGUI selectText = monitorData[monitorLevel].fields[selectIndex].GetComponent<TextMeshProUGUI>();
            string currentSelectText = selectText.text;
            selectText.text = RemoveSelectMarkers(currentSelectText);
            selectIndex--;
            selectText = monitorData[monitorLevel].fields[selectIndex].GetComponent<TextMeshProUGUI>();
            currentSelectText = selectText.text;
            selectText.text = AddSelectMarkers(currentSelectText, '-');


        }
    }
    public void ZeroButton()
    {
        if (monitorData[0].monitorPanel.activeSelf)
        {
            TextMeshProUGUI passwordText = monitorData[0].fields[0].GetComponent<TextMeshProUGUI>();
            int userTypedPasswordLenght = userTypedPassword.Length;
            if (userTypedPasswordLenght < 6)
            {
                userTypedPassword += "0";
                passwordText.text += "*";
                 
            }

        }

    }

    public void OneButton()
    {
        if (monitorData[0].monitorPanel.activeSelf)
        {
            TextMeshProUGUI passwordText = monitorData[0].fields[0].GetComponent<TextMeshProUGUI>();
            int userTypedPasswordLenght = userTypedPassword.Length;
            if (userTypedPasswordLenght < 6)
            {
                userTypedPassword += "1";
                passwordText.text += "*";
            }

        }

    }

    public void UpdateMonitorLevel(string selectedName, int monitorLevel, int selectIndex)
    {
        if (selectedName== "EXECUTE")
        {
            monitorData[monitorLevel].monitorPanel.SetActive(false);
            monitorData[2].monitorPanel.SetActive(true);
            this.monitorLevel = 2;

        }
        else if (selectedName == "STATUS")
        {
            monitorData[monitorLevel].monitorPanel.SetActive(false);
            monitorData[3].monitorPanel.SetActive(true);
            this.monitorLevel = 3;
        }
        else if (selectedName == "SETTINGS")
        {
            monitorData[monitorLevel].monitorPanel.SetActive(false);
            monitorData[4].monitorPanel.SetActive(true);
            this.monitorLevel = 4;
        }
        else if (selectedName == "ABOUT")
        {
            monitorData[monitorLevel].monitorPanel.SetActive(false);
            monitorData[5].monitorPanel.SetActive(true);
            this.monitorLevel = 5;
        }
        else if (selectedName == "BootOrderMenu")
        {
            monitorData[monitorLevel].monitorPanel.SetActive(false);
            monitorData[6].monitorPanel.SetActive(true);
            this.monitorLevel = 6;
        }
        else if (selectedName == "SetNewBootOrder")
        {
            monitorData[monitorLevel].monitorPanel.SetActive(false);
            monitorData[7].monitorPanel.SetActive(true);
            this.monitorLevel = 7;
        }
        else if (selectedName == "CurrentBootOrder")
        {
            monitorData[monitorLevel].monitorPanel.SetActive(false);
            monitorData[8].monitorPanel.SetActive(true);
            this.monitorLevel = 8;
        }else if (selectedName == "FirstOrder" || selectedName == "SecondOrder" || selectedName == "ThirdOrder")
        {
            monitorData[monitorLevel].monitorPanel.SetActive(false);
            monitorData[9].monitorPanel.SetActive(true);
            this.monitorLevel = 9;
            this.newBootOrderIndex = selectIndex;
        }else if (selectedName == "SelectFirst" || selectedName == "SelectSecond" || selectedName == "SelectThird")
        {
            this.selectedBootOrderIndex = selectIndex;
            newBootOrder[newBootOrderIndex] = selectableBootOrder[selectedBootOrderIndex];
            UpdateCurrentBootOrder(newBootOrderSection, newBootOrder);
            monitorData[monitorLevel].monitorPanel.SetActive(false);
            monitorData[7].monitorPanel.SetActive(true);
            this.monitorLevel = 7;

        }
        else if (selectedName == "Save")
        {
            bool containsSelect = Array.Exists(newBootOrder, element => element == "Select");
            if (containsSelect)
            {
                Debug.Log("Ei ole valittuna kaikki kohdat");
            }
            else
            {
                currentBootOrder = newBootOrder;
                UpdateCurrentBootOrder(currentBootOrderSection, currentBootOrder);
                newBootOrder = new string[3] { "Select", "Select", "Select" };
                UpdateCurrentBootOrder(newBootOrderSection, newBootOrder);
                Debug.Log("Päivitetty uudet ohjeet boottiin");
            }
            

        }
    }
    public void ReturnUpdateMonitorLevel(string monitorPanelName , int monitorLevel)
    {
        if (monitorPanelName == "ExecuteScreen" || monitorPanelName == "StatusScreen" || monitorPanelName == "SettingMenuScreen" || monitorPanelName == "AboutScreen")
        {
            monitorData[monitorLevel].monitorPanel.SetActive(false);
            monitorData[1].monitorPanel.SetActive(true);
            this.monitorLevel = 1;
        }
        else if (monitorPanelName == "BootOrderScreen")
        {
            monitorData[monitorLevel].monitorPanel.SetActive(false);
            monitorData[4].monitorPanel.SetActive(true);
            this.monitorLevel = 4;
        }
        else if (monitorPanelName == "NewBootOrderScreen" || monitorPanelName == "CurrentBootOrderScreen")
        {
            monitorData[monitorLevel].monitorPanel.SetActive(false);
            monitorData[6].monitorPanel.SetActive(true);
            this.monitorLevel = 6;
        }
    }
    public void UpdateCurrentBootOrder(GameObject[] currentBootOrderObject, string[] currentBootOrder)
    {
        for (int i = 0; i < currentBootOrder.Length;i++)
        {
            TextMeshProUGUI currentBootOrderText = currentBootOrderObject[i].GetComponent<TextMeshProUGUI>();
            currentBootOrderText.text = currentBootOrder[i];
        }
    }

    public void CloseScreenButton()
    {
        screenObject.SetActive(false);
        monitorData[monitorLevel].monitorPanel.SetActive(false);
        monitorData[0].monitorPanel.SetActive(true);
        monitorLevel = 0;
        TextMeshProUGUI passwordInfoText = passwordInfo.GetComponent<TextMeshProUGUI>();
        passwordInfoText.text = "";

    }

    public string AddSelectMarkers(string selectText , char marker)
    {
        return marker+ selectText+ marker;
    }

    public string RemoveSelectMarkers(string selectText)
    {
        return selectText.Substring(1,selectText.Length - 2);
    }
}
