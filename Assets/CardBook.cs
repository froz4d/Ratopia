using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardBook : MonoBehaviour
{
    [SerializeField] private Transform cardBookingParent;
    [SerializeField] private GameObject cardBookingPrefab;
    
    private HashSet<Card> _cardCollection = new HashSet<Card>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCardToCollection(Card inputCard)
    {
        if (!_cardCollection.Contains(inputCard))
        {
            _cardCollection.Add(inputCard);
        }
        
        RefreshCollection();
    }

    public void RefreshCollection()
    {
        //ลบอันเก่า
        RemoveAllChildren();
        //Create มาจาก list CardCollection for each
        foreach (var VARIABLE in _cardCollection)
        {
            Instantiate(cardBookingPrefab, cardBookingParent).GetComponent<CardBooking>().SetCardBooking(VARIABLE);
        }
    }
    
    private void RemoveAllChildren()
    {
        // Loop through each child and destroy it
        foreach (Transform child in cardBookingParent)
        {
            Destroy(child.gameObject); // Use DestroyImmediate for immediate removal
        }
    }
}
