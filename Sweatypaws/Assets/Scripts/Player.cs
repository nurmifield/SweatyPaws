using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string tool="none";


   

    public void EquipTool(string newTool)
    {
        tool = newTool;
    }


}