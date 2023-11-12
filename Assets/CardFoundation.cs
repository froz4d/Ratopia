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

    public void ShowCardDisplay(Card card)
    {
        cardData = card;
        if (cardData != null )
        {
            titleCard.text = cardData.cardName;
            imageCard.sprite = cardData.cardImage;
            paragraphText.text = cardData.paragraph;
        }
        else
        {
            Debug.Log("Card data is null");
        }
    }

    public void DescriptionButton()
    {
        //show
        FindObjectOfType<DescriptionDisplay>().ShowCard(cardData);
    }

    public void HideTilePanel()
    {
        GameObject[] uiPanels = GameObject.FindGameObjectsWithTag("UIpanel");
        foreach (var VARIABLE in uiPanels)
        {
            VARIABLE.SetActive(false);
        }
    }

    public void ShowTilePanel()
    {
        FindObjectOfType<UIPanel>().ShowTilePanel();
    }
    
}
