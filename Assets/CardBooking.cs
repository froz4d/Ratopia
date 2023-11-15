using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardBooking : MonoBehaviour
{
    public Card cardData;
    
    public TextMeshProUGUI titleCard;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCardBooking(Card inputCard)
    {
        cardData = inputCard;
        titleCard.text = inputCard.cardName;
        GetComponent<Image>().sprite = inputCard.cardImage;
    }
}
