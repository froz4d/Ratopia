using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionDisplay : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI titleCard;
    [SerializeField]private TextMeshProUGUI CardDescription;
    [SerializeField]private GameObject panel;
    
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
