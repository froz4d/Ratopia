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


    //Possible Outcome? %? Fix?
    
    [System.Serializable]
    public class PossibleChainCard
    {
        //ใส่เกินได้ใน Inspector เดี่ยวมันปรับให้เอง
        //can be null
        public Card ChainCard;
        [SerializeField][Tooltip("0-100, if 'sum' of all event more then 100 it will ignore all event")]
        private int possibleOutcome;
        public int  PossibleOutcome
        {
            get { return  possibleOutcome; }
            set {  possibleOutcome = Math.Max(0, Math.Min(100, value)); }
        }
        [field:SerializeField][Tooltip("0 = thisTurn, 1 = nextTurn, Go on")]
        private int excuteChainIn;
        public int ExcuteChainCardIn
        {
            get { return excuteChainIn; }
            set { excuteChainIn = Mathf.Clamp(value, 0, GameManager.MaxTurn-GameManager.CurrentTurn); }
        }
    }
} 
