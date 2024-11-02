using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set;}

    public PlayerData playerData;
    public TextAsset jsonPlayerFile;

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
            Debug.Log("Player data from:"+playerDataFilePath);
        }
        else
        {
            Debug.Log("No player data, Creating new player data");
            playerData= JsonUtility.FromJson<PlayerData>(jsonPlayerFile.text);
            SavePlayerData();
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
