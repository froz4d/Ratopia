using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBook : MonoBehaviour
{
    [SerializeField] private Transform cardBookingParent;
    [SerializeField] private GameObject cardBookingPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateCardBooking(Card inputCard)
    {
        Instantiate(cardBookingPrefab, cardBookingParent).GetComponent<CardBooking>().SetCardBooking(inputCard);
    }
}
