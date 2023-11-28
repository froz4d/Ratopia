using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New CardInfo", menuName = "Card/NewNotifyCard")]
public class NotifyCard : Card
{
    [Header("All Choice")]
    public string Paragraph;
    [FormerlySerializedAs("Description")] public string DescriptionChoice;
    public int Money;
    public int Happiness;
    public int Power;
    public int Stability;
    public List<PossibleChainCard> possibleChainCards;
}
