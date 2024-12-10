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
    public string[] newBootOrder;
    public string[] currentBootOrder;
    public string correctPassword="000000";
    public string userTypedPassword = "";
    public int monitorLevel=0;
    public int selectIndex;
    public bool moduleSelected = false;
    public List<MonitorData> monitorData;

    // Start is called before the first frame update
    void Start()
    {
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
                    passwordText.text = userTypedPassword;
                }

            }
        

        
    }
    public void EnterButton()
    {
        if (monitorData[0].monitorPanel.activeSelf)
        {
            TextMeshProUGUI passwordText = monitorData[0].fields[0].GetComponent<TextMeshProUGUI>();
            if (userTypedPassword == correctPassword)
            {
                monitorData[0].monitorPanel.SetActive(false);
                monitorData[1].monitorPanel.SetActive(true);
                monitorLevel = 1;
                userTypedPassword ="";
                passwordText.text = userTypedPassword;
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
            moduleSelected = true;
            selectIndex = 0;
            TextMeshProUGUI selectText = monitorData[monitorLevel].fields[selectIndex].GetComponent<TextMeshProUGUI>();
            string currentSelectText = selectText.text;
            selectText.text = AddSelectMarkers(currentSelectText, '-');

        }else if (moduleSelected)
        {
            moduleSelected= false;
            TextMeshProUGUI selectText = monitorData[monitorLevel].fields[selectIndex].GetComponent<TextMeshProUGUI>();
            string currentSelectText = selectText.text;
            selectText.text = RemoveSelectMarkers(currentSelectText);
            UpdateMonitorLevel(monitorData[monitorLevel].fields[selectIndex].name,monitorLevel);
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
                passwordText.text = userTypedPassword;
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
                passwordText.text = userTypedPassword;
            }

        }

    }

    public void UpdateMonitorLevel(string selectedName, int monitorLevel)
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
        }
    }

    public void CloseScreenButton()
    {
        screenObject.SetActive(false);
        monitorData[monitorLevel].monitorPanel.SetActive(false);
        monitorData[0].monitorPanel.SetActive(true);
        monitorLevel = 0;
        
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
