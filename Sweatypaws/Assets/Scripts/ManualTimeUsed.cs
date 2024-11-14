using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ManualTimeUsed : MonoBehaviour
{
    public float overAllTimeUsed=0;

    public void CalculateTime(float timeStart , float timeEnd)
    {
        float newTime=timeEnd - timeStart;
        this.overAllTimeUsed += newTime;
    }
}
