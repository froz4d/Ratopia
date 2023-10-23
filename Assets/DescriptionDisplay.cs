using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionDisplay : MonoBehaviour
{
    public TextMeshProUGUI titleCard;
    public TextMeshProUGUI CardDescription;
    public GameObject panel;
    
    public void ShowCard(Card card)
    {
        panel.SetActive(true);
        titleCard.text = card.cardName;
        CardDescription.text = card.description;
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}
