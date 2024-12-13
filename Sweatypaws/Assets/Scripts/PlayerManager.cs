using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set;}

    public PlayerData playerData;
    public TextAsset jsonPlayerFile;
    [SerializeField]
    private string selectedLevel = "none";
    private bool existingUser = false;
    private bool isSoundOn = true;
    
    public bool IsSoundOn
    {
        get => isSoundOn;
        set
        {
            isSoundOn = value;
            AudioListener.volume = isSoundOn ? 1 : 0;
        }
    }

    public void ToggleSound()
{
    IsSoundOn = !IsSoundOn;
    Debug.Log("Sound toggled: " + (IsSoundOn ? "On" : "Off"));
    SavePlayerData();
}
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
            Instance.SetExistingUser(true);

        }
    }
   
    public void SavePlayerData()
    {
        playerData.selectedLevel = selectedLevel;
        playerData.isSoundOn = isSoundOn;
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
            IsSoundOn = playerData.isSoundOn;
            Debug.Log("Player data from:"+playerDataFilePath);

            if (playerData.version < newPlayerData.version)
            {

                if (playerData.version < 3)
                {
                    NewGame();
                    SetExistingUser(false);
                    Debug.Log("Versio number is smaller than 3 , newGame activated");

                }
                else
                {
                    UpdatePlayerData(newPlayerData);
                    playerData.version = newPlayerData.version;
                    for (int i = 0; i < playerData.level_progress.Count; i++)
                    {
                        if (playerData.level_progress[i].collection == default)
                        {
                            Debug.Log("Adding Collection default value false");
                            playerData.level_progress[i].collection = false;
                        }
                    }
                    SavePlayerData();
                    Debug.Log("Player data updated");
                }
                

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

    public bool PlayerDataFileExists()
    {
        return File.Exists(playerDataFilePath);
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
                Debug.Log("adding new level information");
                playerData.level_progress.Add(new PlayerData.LevelProgress(newLevel.level_name,newLevel.level_index,newLevel.max_score));
            }
        }

        foreach (var newDialogue in newPlayerData.dialogue_progress)
        {
            if (!playerData.DialogueExist(newDialogue.dialogue_name))
            {
                Debug.Log("adding new dialogue information");
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

    public void LevelCollection(string levelName, int score)
    {
        foreach (var level in playerData.level_progress)
        {
            if (level.level_name == levelName)
            {
                if (score == level.max_score && !level.collection)
                {
                    level.collection = true;
                }
                break;
            }
        }
        SavePlayerData();
    }
    public void TutorialDone()
    {
        playerData.tutorial_done=true;
        SavePlayerData() ;
    }
 
}
