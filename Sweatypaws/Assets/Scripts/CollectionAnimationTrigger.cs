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
            if (player.GetSelectedLevel() == player.playerData.level_progress[i].level_name && player.playerData.level_progress[i].max_score == score.GetScore())
            {
                animator.SetTrigger("CollectionInfo");
            }
        }
    }
}
