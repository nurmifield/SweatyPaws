using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : Unity.Services.Analytics.Event
{
    public LevelStart() : base("levelStart")
    {
    }

    public string Level { set { SetParameter("level", value); } }

}
