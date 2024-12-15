using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEndScreenAnimation : MonoBehaviour
{
    public EndScreenManager endScreenManager;
    void Start()
    {
        GameObject endScreenObject = GameObject.Find("LevelButtons");
        endScreenManager = endScreenObject.GetComponent<EndScreenManager>();
    }
    public void DestroyEndScreenAnimation() 
    {
        endScreenManager.DestroyEndScreen();
    }
   
}
