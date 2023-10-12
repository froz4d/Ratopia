using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CardInfo", menuName = "Card/NewCardInfo")]
public class DefaultCard : Card
{
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
}
