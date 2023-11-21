using UnityEngine;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int currentTurn;
    public int currentMoney;
    public int currentHappiness;
    public int currentPower;
    public int currentStability;
   
  
}


public class SaveGameManager : MonoBehaviour
{
    public static void SaveGame()
    {
        GameData data = new GameData
        {
            currentTurn = GameManager.CurrentTurn,
            currentMoney = GameManager.CurrentMoney,
            currentHappiness = GameManager.CurrentHappiness,
            currentPower = GameManager.CurrentPower,

            // Set other relevant data...
        };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/gameData.json", json);
    }
    public static void LoadGame()
    {
        string path = Application.persistentDataPath + "/gameData.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameData data = JsonUtility.FromJson<GameData>(json);

            GameManager.CurrentTurn = data.currentTurn;
            GameManager.CurrentMoney = data.currentMoney;
            GameManager.CurrentHappiness = data.currentHappiness;
            GameManager.CurrentPower = data.currentPower;
            GameManager.CurrentStability = data.currentStability;

            // Set other relevant data...
        }
        else
        {
            Debug.LogWarning("No saved game data found.");
        }
    }
}