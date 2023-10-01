using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public static int CurrentTurn = 0;
    public static int CurrentMoney
    {
        get { return _currentMoney; }
        set { _currentMoney = Math.Max(0, Math.Min(10, value)); }
    }

    private static int _currentMoney;

    public static int CurrentHappiness
    {
        get { return _currentHappiness; }
        set { _currentHappiness = Math.Max(0, Math.Min(10, value)); }
    }

    private static int _currentHappiness;

    public static int CurrentPower
    {
        get { return _currentPower; }
        set { _currentPower = Math.Max(0, Math.Min(10, value)); }
    }

    private static int _currentPower;

    public static int CurrentStability
    {
        get { return _currentStability; }
        set { _currentStability = Math.Max(0, Math.Min(10, value)); }
    }

    private static int _currentStability;

    public static List<Card> CardsInDeck = new List<Card>(); //ChainEvent ไม่อยู่ใน Deck

    private class CardsHoldOn
    {
        public int TurnLeftToExcute; //0 = display
        public Card Card;
        public CardsHoldOn(Card card)
        {
            Card = card;
            TurnLeftToExcute = 0;
        }
        public CardsHoldOn(Card card,int turn)
        {
            Card = card;
            TurnLeftToExcute = turn;
        }
    }
    private static List<CardsHoldOn> _cardHoldOn = new List<CardsHoldOn>(); //ChainEvent หรือการ์ดที่จะโผล่ตามมา int = จำนวนเทิร์น
    
    private Queue<Card> _displayCard = new Queue<Card>(); //Q ของ การ์ดที่จะ Show ใน Turn นั้น
    private static Card CurrentDisplayCard;

    [SerializeField] private GameObject cardFoundation;

    [Header("setting")]
    
    [SerializeField]
    private int _Maxturn;
    
    [SerializeField][Tooltip("0-10")]
    private int _startMoney;

    [SerializeField][Tooltip("0-10")]
    private int _startHappiness;

    [SerializeField][Tooltip("0-10")]
    private int _startPower;

    [SerializeField][Tooltip("0-10")]
    private int _startStability;
    
    public int StartMoney
    {
        get { return _startMoney; }
        set { _startMoney = Mathf.Clamp(value, 0, 10); }
    }
    public int StartHappiness
    {
        get { return _startHappiness; }
        set { _startHappiness = Mathf.Clamp(value, 0, 10); }
    }
    public int StartPower
    {
        get { return _startPower; }
        set { _startPower = Mathf.Clamp(value, 0, 10); }
    }
    public int StartStability
    {
        get { return _startStability; }
        set { _startStability = Mathf.Clamp(value, 0, 10); }
    }

    [SerializeField]
    private int RandomCardPerTurn;

    void Start()
    {
        CurrentMoney = StartMoney;
        CurrentHappiness = StartHappiness;
        CurrentPower = StartPower;
        CurrentStability = StartStability;
        
        Card[] cards = Resources.LoadAll<Card>("FixEvent");
        CardsInDeck.AddRange(cards);
        
        Debug.Log(CardsInDeck.Count);
        
        //////////////Test/////////////////////
        EndTurn();
        //////////////Test/////////////////////
    }

    private void StartTurn()
    {
        CurrentTurn++;
        
        Debug.Log("StartTurn : " + CurrentTurn);
        //foreach checkCard Turn = 0 ให้ display Card
        if (_cardHoldOn.Count > 0)
        {
            for (int i = _cardHoldOn.Count - 1; i >= 0; i--)
            {
                if (_cardHoldOn[i].TurnLeftToExcute == 0)
                {
                    Debug.Log("CardHoldOn ReadyToExecute: " + _cardHoldOn[i].Card.cardName);
                    _displayCard.Enqueue(_cardHoldOn[i].Card);
                    _cardHoldOn.RemoveAt(i);
                }
            }
        }

        if (_displayCard.Count > 0)
        { 
            DisplayCard(_displayCard.Dequeue());
        }
    }

    public void EndTurn()
    {
        //ลดการ์ดที่มีอยู่
        if (_cardHoldOn.Count >= 0)
        {
            foreach (var VARIABLE in _cardHoldOn)
            {
                VARIABLE.TurnLeftToExcute -= 1;
            }
        }

        //สุ่มการ์ดใหม่
        AddCardToHoldOn(RandomCardPerTurn);
        
        StartTurn();
    }

    public static void UpdateResource(int money, int happy, int power, int stability,Card chainEvent,int excuteTurn)
    {
        Debug.Log($"UpdateResourceFor : {CurrentMoney} : {CurrentHappiness} : {CurrentPower} : {CurrentStability}");
        //รับค่าเป็น +-
        CurrentMoney += money;
        CurrentHappiness += happy;
        CurrentPower += power;
        CurrentStability += stability;
        
        Debug.Log($"UpdateResource : {money} : {happy} : {power} : {stability}");
        Debug.Log($"UpdateResourceTo : {CurrentMoney} : {CurrentHappiness} : {CurrentPower} : {CurrentStability}");
        
        //เขียน Debug.Log เพราะเพื่อไปเขียน History ไว้
        
        //Card
        if (chainEvent != null)
        {
            CardsHoldOn newcard = new CardsHoldOn(chainEvent,excuteTurn);
            _cardHoldOn.Insert(0,newcard);
        }
    }

    private void DisplayCard(Card card)
    {
        CurrentDisplayCard = card;
        Debug.Log("currentDisplayCard : " + CurrentDisplayCard.cardName);
        //Instantiate แล้วใส่ script .create? หรือวางที่ไว้เลยไม่ต้อง Instantiate??? ให้ป๋อมแป๋ม♥ ดีไซด์ ละกานนนนนนนนนน
        CardFoundation cardGameObject = Instantiate(cardFoundation).GetComponent<CardFoundation>();
    }

    private void AddCardToHoldOn(int numberCardToRandom)
    {
        //Random In Range มา ใน CardInDesk
        Random random = new Random();

        for (int i = 0; i < numberCardToRandom; i++)
        {
            if (CardsInDeck.Count == 0)
            {
                Debug.Log("Warning!! Card In Desk Is Out");
                break;
            }
            int randomIndex = random.Next(0, CardsInDeck.Count-1);
            
            Debug.Log("random number : " + randomIndex);

            //จากนั้นเรียก ใส่ใน CardHoldOn แล้วเอาเข้า list _CardHoldOn
            CardsHoldOn newcard = new CardsHoldOn(CardsInDeck[randomIndex]);
            _cardHoldOn.Add(newcard);

            Debug.Log("add newcard : " + newcard.Card.cardName);
            
            //อย่าลืม Remove ใน List ออก
            CardsInDeck.RemoveAt(randomIndex);
        }
    }
    
    public void NextCard()
    {
        if (_displayCard.Count == 0)
        {
            CurrentDisplayCard = null;
            Debug.Log("End Turn");
            EndTurn();
        }
        //ไว้ใช้ตอนเลือกเสร็จแล้ว
        else
        {
            DisplayCard(_displayCard.Dequeue());
        }

    }

    [ContextMenu("Execute LeftChoiceMethod")]
    public void LeftChoiceSelect()
    {
        Debug.Log("Excute " + CurrentDisplayCard.cardName + " LeftChoice");
        UpdateResource(CurrentDisplayCard.leftMoney,CurrentDisplayCard.leftMoney,CurrentDisplayCard.leftPower,CurrentDisplayCard.leftStability,CurrentDisplayCard.leftChainCard,CurrentDisplayCard.left_ExcuteChainCardIn);
        NextCard();
    }

    [ContextMenu("Execute RightChoiceMethod")]
    public void RightChoiceSelect()
    {
        Debug.Log("Excute " + CurrentDisplayCard.cardName + " RightChoice");
        UpdateResource(CurrentDisplayCard.rightMoney,CurrentDisplayCard.rightMoney,CurrentDisplayCard.rightPower,CurrentDisplayCard.rightStability,CurrentDisplayCard.rightChainCard,CurrentDisplayCard.right_ExcuteChainCardIn);
        NextCard();
    }
}
