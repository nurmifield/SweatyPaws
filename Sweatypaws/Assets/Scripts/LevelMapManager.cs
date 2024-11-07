using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMapManager : MonoBehaviour
{
    public GameObject dialogCanvas;
    // Start is called before the first frame update
    void Start()
    {
        if (CheckDialogue())
        {
            StartDialog();
        }
            
    }

    public void StartDialog()
    {
        var player = PlayerManager.Instance;

        for (int i = 0; i < player.playerData.dialogue_progress.Count; i++)
        {
            if (player.playerData.dialogue_progress[i].dialogue_index == player.playerData.dialogue_level && player.playerData.dialogue_progress[i].watched == false && player.playerData.level == player.playerData.dialogue_progress[i].level_index)
            {
                
                dialogCanvas.GetComponent<DialogueManager>().DisplayDialogue();
                dialogCanvas.GetComponent<DialogueManager>().SetContinueButton(false);
                dialogCanvas.GetComponent<DialogueManager>().SetPreviousButton(false);

            }
        }
    }

    public void StartDialogChangeScene()
    {
        var player = PlayerManager.Instance;

        for (int i = 0; i < player.playerData.dialogue_progress.Count; i++)
        {
            if (player.playerData.dialogue_progress[i].dialogue_index == player.playerData.dialogue_level && player.playerData.dialogue_progress[i].watched == false && player.playerData.level == player.playerData.dialogue_progress[i].level_index)
            {

                dialogCanvas.GetComponent<DialogueManager>().DisplayDialogue();
                dialogCanvas.GetComponent<DialogueManager>().SetContinueButton(true);
                dialogCanvas.GetComponent<DialogueManager>().SetPreviousButton(true);

            }
        }
    }
    public bool CheckDialogue()
    {
        var player = PlayerManager.Instance;
        bool dialogueReady = false;

        for (int i = 0; i < player.playerData.dialogue_progress.Count; i++)
        {
            if (player.playerData.dialogue_progress[i].dialogue_index == player.playerData.dialogue_level && player.playerData.dialogue_progress[i].watched == false && player.playerData.level == player.playerData.dialogue_progress[i].level_index)
            {
                if (player.playerData.dialogue_progress[i].selected_level == player.GetSelectedLevel())
                {
                    dialogueReady = true;
                    break;
                }
               

            }
        }
        Debug.Log("Dialogue ready: " + dialogueReady);
        return dialogueReady;
    }
}
