using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New CardInfo", menuName = "Card/NewCardInfo")]
public class Card : ScriptableObject
{
    public string cardName;

    public Image cardImage;
    
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
    //ไว้ ChainEvent can be null
    public Card leftChainCard;
    [field:SerializeField][Tooltip("1 = nextTurn, Go on")]
    private int leftExcuteChain = 1;

    [Header("Choice Right")]
    public string rightParagraph;
    public string rightDescription;
    public int rightMoney;
    public int rightHappiness;
    public int rightPower;
    public int rightStability;
    //ไว้ ChainEvent can be null
    public Card rightChainCard;
    [field:SerializeField][Tooltip("1 = nextTurn, Go on")]
    private int rightExcuteChain = 1;

    public int left_ExcuteChainCardIn
    {
        get { return leftExcuteChain; }
        set { leftExcuteChain = Mathf.Clamp(value, 1, 24); }
    }
    
    public int right_ExcuteChainCardIn
    {
        get { return rightExcuteChain; }
        set { rightExcuteChain = Mathf.Clamp(value, 1, 24); }
    }
    
    //Possible Outcome? %? Fix?
    
}
