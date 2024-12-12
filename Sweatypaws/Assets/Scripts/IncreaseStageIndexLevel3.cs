using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseStageIndexLevel3 : MonoBehaviour
{
    public GameActionManager actionManager;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameActionObject = GameObject.Find("GameSystem");
        actionManager = gameActionObject.GetComponent<GameActionManager>();
    }

   public void IncreaseStageLevel()
    {
        actionManager.IncreaseOnlyStage(1);
    }
}
