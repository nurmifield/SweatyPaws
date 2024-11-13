using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : Unity.Services.Analytics.Event
{
    public LevelStart() : base("levelStart")
    {
    }

    public string level { set { SetParameter("level", value); } }

}
public class LevelCompleted : Unity.Services.Analytics.Event
{
    public LevelCompleted() : base("levelCompleted")
    {
    }

    public string level { set { SetParameter("level", value); } }
    public bool levelCompleted { set { SetParameter("levelCompleted", value); } }
    public float timeUsedOnManual { set { SetParameter("timeUsedOnManual", value); } }

}
