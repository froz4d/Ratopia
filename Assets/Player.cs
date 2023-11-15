using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Player
{
    //ต้องใส่ การ์ดที่เจอแล้ว กับ Achivment?
    public string PlayerName;
    public Sprite PlayerProfile;
    public int PlayerScore;

    public Player(string playerName , int playerScore)
    {
        PlayerName = playerName;
        PlayerScore = playerScore;
    }

}
