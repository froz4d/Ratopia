using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Card : ScriptableObject
{
    public string cardName;

    public Sprite cardImage;
    
    [TextArea]
    public string description;
    [TextArea(minLines:1,maxLines:4)]
    public string paragraph;

    [Header("Choice Left")]
    public string leftParagraph;
    public string leftDescription;
    public int leftMoney;
    public int leftHappiness;
    public int leftPower;
    public int leftStability;
    public List<PossibleChainCard> leftPossibleChainCards;

    [Header("Choice Right")]
    public string rightParagraph;
    public string rightDescription;
    public int rightMoney;
    public int rightHappiness;
    public int rightPower;
    public int rightStability;
    public List<PossibleChainCard> rightPossibleChainCards;

    //Possible Outcome? %? Fix?
    
    [System.Serializable]
    public class PossibleChainCard
    {
        //ใส่เกินได้ใน Inspector เดี่ยวมันปรับให้เอง
        //can be null
        public Card leftChainCard;
        [SerializeField][Tooltip("0-100, if 'sum' of all event more then 100 it will ignore all event")]
        private int possibleOutcome;
        public int  PossibleOutcome
        {
            get { return  possibleOutcome; }
            set {  possibleOutcome = Math.Max(0, Math.Min(100, value)); }
        }
        [field:SerializeField][Tooltip("1 = nextTurn, Go on")]
        private int excuteChainIn;
        public int ExcuteChainCardIn
        {
            get { return excuteChainIn; }
            set { excuteChainIn = Mathf.Clamp(value, 1, GameManager.MaxTurn-GameManager.CurrentTurn); }
        }
    }
}
