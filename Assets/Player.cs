using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    public string PlayerName { get; set; }
    public Sprite PlayerProfile { get; set; }
    public int PlayerScore { get; set; }

    public Player(string playerName , int playerScore)
    {
        PlayerName = playerName;
        PlayerScore = playerScore;
    }

}
