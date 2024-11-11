using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set;}

    public PlayerData playerData;
    public TextAsset jsonPlayerFile;
    private int latestVersion = 1;
    [SerializeField]
    private string selectedLevel = "none";
    private bool existingUser = false;

    public bool GetExistingUser()
    {
        return existingUser;
    }

    public void SetExistingUser(bool newExistingUser)
    {
        existingUser = newExistingUser;
    }
    public string GetSelectedLevel()
    {
        return selectedLevel;
    }

    public void SetSelectedLevel(string newSelectedLevel)
    {
        selectedLevel=newSelectedLevel;
    }

    private string playerDataFilePath;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            playerDataFilePath = Path.Combine(Application.persistentDataPath, "player_data.json");
            LoadPlayerData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
   
    public void SavePlayerData()
    {
        playerData.selectedLevel = selectedLevel;
        string jsonData = JsonUtility.ToJson(playerData);
        string encryptedData = EncryptionHelper.Encrypt(jsonData);
        File.WriteAllText(playerDataFilePath, encryptedData);
        Debug.Log("Player Data Saved");
    }

    public void LoadPlayerData()
    {
        if (File.Exists(playerDataFilePath))
        {
            string encryptedData = File.ReadAllText(playerDataFilePath);
            string decryptedData = EncryptionHelper.Decrypt(encryptedData);
            playerData = JsonUtility.FromJson<PlayerData>(decryptedData);
            PlayerData newPlayerData= JsonUtility.FromJson<PlayerData>(jsonPlayerFile.text);
            SetExistingUser(true);
            Debug.Log("Player data from:"+playerDataFilePath);

            if (playerData.version < latestVersion)
            {
                UpdatePlayerData(newPlayerData);
                playerData.version = newPlayerData.version;
                SavePlayerData();
                Debug.Log("Player data updated");

            }
            else
            {
                Debug.Log("Player data doesn't need update");
            }
           
        }
        else
        {
            Debug.Log("No player data, Creating new player data");
            NewGame();
        }
    }

    public void NewGame()
    {
        playerData = JsonUtility.FromJson<PlayerData>(jsonPlayerFile.text);
        SavePlayerData();
    }
    public void UpdatePlayerData(PlayerData newPlayerData)
    {
        foreach (var newLevel in newPlayerData.level_progress)
        {
            if (!playerData.LevelExists(newLevel.level_name))
            {
                playerData.level_progress.Add(new PlayerData.LevelProgress(newLevel.level_name,newLevel.level_index));
            }
        }

        foreach (var newDialogue in newPlayerData.dialogue_progress)
        {
            if (!playerData.DialogueExist(newDialogue.dialogue_name))
            {
                playerData.dialogue_progress.Add(new PlayerData.DialogueProgress(newDialogue.dialogue_name, newDialogue.dialogue_index, newDialogue.level_index ,newDialogue.selected_level));
            }
        }
    }

    public void DialogCompleted(string sceneName)
    {
        foreach (var dialogue in playerData.dialogue_progress)
        {
            if (sceneName == dialogue.dialogue_name)
            {
                dialogue.watched = true;
                playerData.dialogue_level++;
                break;
            }
        }
        SavePlayerData();
    }

    public void LevelCompleted(string levelName,int score)
    {
        foreach (var level in playerData.level_progress)
        {
            if (level.level_name == levelName)
            {
                level.complete = true;
                if (score > level.score)
                {
                    level.score = score;
                }
                if (playerData.level == level.level_index )
                {
                    playerData.level++;
                }
                break;
            }
        }
        SavePlayerData() ;
    }
 
}
