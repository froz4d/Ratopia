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
    public List<Card> cardsInDeck;
    public List<GameManager.CardsHoldOn> cardHoldOn;
    public Card CurrentDisplayCard;
    public List<int> remainingHappinessInEndTurn;
    public List<int> remainingMoneyInEndTurn;
    public List<int> remainingPowerInEndTurn;
    public List<int> remainingStabilityInEndTurn;

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
            currentStability = GameManager.CurrentStability,
            cardsInDeck = GameManager.CardsInDeck,
            cardHoldOn = GameManager._cardHoldOn,
            CurrentDisplayCard = GameManager.CurrentDisplayCard,
            remainingHappinessInEndTurn = GameManager._remainingHappinessInEndTurn,
            remainingMoneyInEndTurn = GameManager._remainingMoneyInEndTurn,
            remainingPowerInEndTurn = GameManager._remainingPowerInEndTurn,
            remainingStabilityInEndTurn = GameManager._remainingStabilityInEndTurn
         

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
            GameManager.CurrentDisplayCard = data.CurrentDisplayCard;
            GameManager.CardsInDeck = data.cardsInDeck;
            GameManager._remainingHappinessInEndTurn = data.remainingHappinessInEndTurn;
            GameManager._remainingMoneyInEndTurn = data.remainingMoneyInEndTurn;
            GameManager._remainingPowerInEndTurn = data.remainingPowerInEndTurn;
            GameManager._remainingStabilityInEndTurn = data.remainingStabilityInEndTurn;
        }
        else
        {
            Debug.LogWarning("No saved game data found.");
        }
    }

 
}