using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapManager : MonoBehaviour
{
    public GameObject dialogCanvas;
    // Start is called before the first frame update
    void Start()
    {
        var player= PlayerManager.Instance;
   
                for (int i=0;i< player.playerData.dialogue_progress.Count;i++)
                {
                    if (player.playerData.dialogue_progress[i].dialogue_index== player.playerData.dialogue_level && player.playerData.dialogue_progress[i].watched == false && player.playerData.level== player.playerData.dialogue_progress[i].level_index )
                    {
                       Instantiate(dialogCanvas, new Vector2(0, 0), Quaternion.identity);
   
                    }
                } 
    }
}
