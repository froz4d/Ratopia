using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardFoundation : MonoBehaviour
{

    public Card cardData;
    
    public TextMeshProUGUI titleCard;
    public Image imageCard;
    public TextMeshProUGUI paragraphText;
    //public TextMeshProUGUI descriptionText;
    
    
    public void ShowCardDisplay(Card card)
    {
        cardData = card;
        if (cardData != null )
        {
            titleCard.text = cardData.cardName;
            imageCard.sprite = cardData.cardImage;
            paragraphText.text = cardData.paragraph;
            //descriptionText.text = cardData.description;
        }
        else
        {
            Debug.Log("Card data is null");
        }
    }
}
