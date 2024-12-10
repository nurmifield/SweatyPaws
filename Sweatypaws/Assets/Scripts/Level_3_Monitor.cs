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
        }
    }
    public void DownButton()
    {
        if (moduleSelected)
        {
            selectIndex++;
 
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

    public void CloseScreenButton()
    {
        screenObject.SetActive(false);
        monitorData[monitorLevel].monitorPanel.SetActive(false);
        monitorData[0].monitorPanel.SetActive(true);
        monitorLevel = 0;
        
    }
}
