using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionAnimationTrigger : MonoBehaviour
{
    public Score score;
    public Animator animator;
public void CollectionAnimationCheck()
    {
        var player = PlayerManager.Instance;
        for (int i = 0; i < player.playerData.level_progress.Count;i++)
        {
            if (player.GetSelectedLevel() == player.playerData.level_progress[i].level_name)
            {
                if (!player.playerData.level_progress[i].collection && player.playerData.level_progress[i].max_score == score.GetScore())
                {
                    animator.SetTrigger("CollectionAdded");
                    player.LevelCollection(player.GetSelectedLevel(), score.GetScore());
                    break;
                }
                else
                {
                    //Debug.Log("Collection on jo saatu tai ei riittävästi pisteitä");  
                    break;
                }
                
            }
        }
    }
}
