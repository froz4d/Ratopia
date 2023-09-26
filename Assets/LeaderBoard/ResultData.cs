using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PlayerData
{
   public string playerName;
   public int noNumber;
   public string survivalWeek;
   public string conditionEnd;

   public PlayerData(int noNumber, string playerName, string survivalWeek, string conditionEnd)
   {
      this.noNumber = noNumber;
      this.playerName = playerName;
      this.survivalWeek = survivalWeek;
      this.conditionEnd = conditionEnd;
   }
   
   public PlayerData(string playerName, string survivalWeek, string conditionEnd)
   {
      this.playerName = playerName;
      this.survivalWeek = survivalWeek;
      this.conditionEnd = conditionEnd;
   }
   
}

public class ResultData : MonoBehaviour
{
   public PlayerData playerData;
   
   [SerializeField] private TextMeshProUGUI noText;
   [SerializeField] private TextMeshProUGUI nameText;
   [SerializeField] private TextMeshProUGUI week;
   [SerializeField] private TextMeshProUGUI condition;


   public void UpdateData()
   {
      noText.text = playerData.noNumber.ToString();
      nameText.text = playerData.playerName;
      week.text = playerData.survivalWeek;
      condition.text = playerData.conditionEnd;
   }
   
}
