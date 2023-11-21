using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = System.Random;


public class GameManager : MonoBehaviour
{
    #region Setting & Current Status

    private static GameManager instance;
    private static History _history;
    private static CardBook _cardBook;

    //ไว้โชว์ Devlog
    public static bool ShowDevLog = true;
    
    //ใส่เกินได้ ใน Inspector เดี่ยวมันปรับให้เอง
    public static int CurrentTurn = 0;

    public static int CurrentMoney
    {
        get { return _currentMoney; }
        set { _currentMoney = Math.Max(-10, Math.Min(10, value)); }
    }

    private static int _currentMoney;

    public static int CurrentHappiness
    {
        get { return _currentHappiness; }
        set { _currentHappiness = Math.Max(-10, Math.Min(10, value)); }
    }

    private static int _currentHappiness;

    public static int CurrentPower
    {
        get { return _currentPower; }
        set { _currentPower = Math.Max(-10, Math.Min(10, value)); }
    }

    private static int _currentPower;

    public static int CurrentStability
    {
        get { return _currentStability; }
        set { _currentStability = Math.Max(-10, Math.Min(10, value)); }
    }

    private static int _currentStability;

    public static List<Card> CardsInDeck = new List<Card>(); //ChainEvent ไม่อยู่ใน Deck

    public class CardsHoldOn
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

    public static List<CardsHoldOn> _cardHoldOn = new List<CardsHoldOn>(); //ChainEvent หรือการ์ดที่จะโผล่ตามมา int = จำนวนเทิร์น
    
    private static Queue<Card> _displayCard = new Queue<Card>(); //Q ของ การ์ดที่จะ Show ใน Turn นั้น
    public static Card CurrentDisplayCard;
    
    
    [SerializeField] private GameObject cardFoundation;
    public GameObject cardParent;

    [field: Header("setting")] 
    [SerializeField]
    private int _MaxTurn;
    public static int MaxTurn;

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
        set { _startMoney = Mathf.Clamp(value, -10, 10); }
    }
    public int StartHappiness
    {
        get { return _startHappiness; }
        set { _startHappiness = Mathf.Clamp(value, -10, 10); }
    }
    public int StartPower
    {
        get { return _startPower; }
        set { _startPower = Mathf.Clamp(value, -10, 10); }
    }
    public int StartStability
    {
        get { return _startStability; }
        set { _startStability = Mathf.Clamp(value, -10, 10); }
    }

    [SerializeField]
    private int RandomCardPerTurn;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI HappinessText;
    [SerializeField] private TextMeshProUGUI PowerText;
    [SerializeField] private TextMeshProUGUI StabilityText;
    [SerializeField] private TextMeshProUGUI TurnText;
    

    #endregion
    
    void Start()
    {
        _history = FindObjectOfType<History>();
        _cardBook = FindObjectOfType<CardBook>();

        NewGame();
        
 //       Debug.Log(CardsInDeck.Count);
        
        //////////////Test/////////////////////
        //EndTurn();
        //////////////Test/////////////////////
        ///
        /// แม่งเอ้ยน่าเขียน Finite state Machine
        /// แต่ขก ค่อย Optimize ล้ะกานนนนนนนนนนนนนนนนนนนนน
        ///
    }

    private void StartTurn()
    {
        CurrentTurn++;
        _history.Record("StartTurn" + CurrentTurn);

        //_history.Record("StartTurn : " + CurrentTurn + " / " + MaxTurn);
        //foreach checkCard Turn = 0 ให้ display Card
        if (_cardHoldOn.Count > 0)
        {
            for (int i = _cardHoldOn.Count - 1; i >= 0; i--)
            {
                if (_cardHoldOn[i].TurnLeftToExcute == 0)
                {
                    _history.DevRecord("CardHoldOn For DeckCard ReadyToExecute: " + _cardHoldOn[i].Card.cardName);
                    _displayCard.Enqueue(_cardHoldOn[i].Card);
                    _cardHoldOn.RemoveAt(i);
                }
            }
        }

        if (_displayCard.Count > 0)
        { 
            DisplayCard(_displayCard.Dequeue());
         //   NextDisplayCard(_displayCard.Dequeue());
        }
        
        _history.DevRecord("Have CardsIn Deck Left : " + CardsInDeck.Count);
        
        StartCoroutine(UpdateResource());
    }

    public void EndTurn()
    {
        _history.Record("End "+ CurrentTurn + "Turn");
     //   Debug.LogWarning("EndTurn" + CardsInDeck.Count);
        //ลดการ์ดที่มีอยู่
        if (_cardHoldOn.Count >= 0)
        {
            foreach (var VARIABLE in _cardHoldOn)
            {
                VARIABLE.TurnLeftToExcute -= 1;
            }
        }

        //สุ่มการ์ดใหม่
        RandomCardInDeckToHoldOn(RandomCardPerTurn);
        CheckCondition();
        
        StartTurn();
        
        //ใส่ กรณี End LastTurn
    }

    #region Display

    private void DisplayCard(Card card)
    {
        CurrentDisplayCard = card;
        
        _cardBook.AddCardToCollection(CurrentDisplayCard);
        _history.Record("currentDisplayCard : " + CurrentDisplayCard.cardName);

        GameObject cardObject = Instantiate(cardFoundation, cardParent.transform, false); // Change true to false
        cardObject.transform.SetAsFirstSibling(); 
        
        RectTransform cardObjectRect = cardObject.GetComponent<RectTransform>();

        CardFoundation cardFoundationScript = cardObject.GetComponent<CardFoundation>();
        RectTransform cardFoundaRect = cardFoundation.GetComponent<RectTransform>();
        cardFoundationScript.cardData = card;
        cardFoundationScript.ShowCardDisplay(card);
        cardObject.AddComponent<NewCardController>();
        //Set scale
        cardObject.transform.localScale = new Vector3(1, 1, 1);
        
        // Set RectTransform
        cardObjectRect.anchoredPosition = cardFoundaRect.anchoredPosition;
        cardObjectRect.sizeDelta = cardFoundaRect.sizeDelta;
        cardObjectRect.pivot = cardFoundaRect.pivot;
        cardObjectRect.anchorMin = cardFoundaRect.anchorMin;
        cardObjectRect.anchorMax = cardFoundaRect.anchorMax;
    }
    
    private void NextDisplayCard(Card card)
    {
        CurrentDisplayCard = card;
        
        _cardBook.AddCardToCollection(CurrentDisplayCard);
        _history.Record("currentDisplayCard : " + CurrentDisplayCard.cardName);

        GameObject cardObject = Instantiate(cardFoundation, cardParent.transform, false); // Change true to false
        cardObject.transform.SetAsFirstSibling(); 
        RectTransform cardObjectRect = cardObject.GetComponent<RectTransform>();

        CardFoundation cardFoundationScript = cardObject.GetComponent<CardFoundation>();
        RectTransform cardFoundaRect = cardFoundation.GetComponent<RectTransform>();
        cardFoundationScript.cardData = card;
        cardFoundationScript.ShowCardDisplay(card);

        cardObject.AddComponent<NextCard>();
        //Set scale
        cardObject.transform.localScale = new Vector3(1, 1, 1);
        
        // Set RectTransform
        cardObjectRect.anchoredPosition = cardFoundaRect.anchoredPosition;
        cardObjectRect.sizeDelta = cardFoundaRect.sizeDelta;
        cardObjectRect.pivot = cardFoundaRect.pivot;
        cardObjectRect.anchorMin = cardFoundaRect.anchorMin;
        cardObjectRect.anchorMax = cardFoundaRect.anchorMax;
        
    }
    
    //Display ถ้าเป็นNotifyCard ให้โชว์เหมือนกัน
    
    public static void DisplayLeftChoice()
    {
        Debug.Log("กำลังโชว์ฝั่งซ้าย");
    }
    
    public static void DisplayRightChoice()
    {
        Debug.Log("กำลังโชว์ฝั่งขวา");
    }

    public static void DisplayCloseAllDetail()
    {
        Debug.Log("ปิด display หมด");
    }

    #endregion

    private IEnumerator UpdateResource()
    {
        TurnText.color = Color.red;
        moneyText.color = Color.red;
        PowerText.color = Color.red;
        StabilityText.color = Color.red;
        HappinessText.color = Color.red;
        
        TurnText.text = CurrentTurn.ToString();
        moneyText.text = CurrentMoney.ToString();
        PowerText.text = CurrentPower.ToString();
        StabilityText.text = CurrentStability.ToString();
        HappinessText.text = CurrentHappiness.ToString();
        
        yield return new WaitForSeconds(1f);
        
        TurnText.color = Color.black;
        moneyText.color = Color.black;
        PowerText.color = Color.black;
        StabilityText.color = Color.black;
        HappinessText.color = Color.black;
    }
    

    private static void UpdateResource(int money, int happy, int power, int stability)
    {

        _history.Record($"UpdateResourceFor : {CurrentMoney} : {CurrentHappiness} : {CurrentPower} : {CurrentStability}");
        //รับค่าเป็น +-
        CurrentMoney += money;
        CurrentHappiness += happy;
        CurrentPower += power;
        CurrentStability += stability;
        
        _history.Record($"UpdateResourceTo : {CurrentMoney} : {CurrentHappiness} : {CurrentPower} : {CurrentStability}");
        
        //เขียน Debug.Log เพราะเพื่อไปเขียน History ไว้
        
        //Card
    }

    public static void AddCardsHoldOn(Card chainEvent, int excuteTurn)
    {
        if (chainEvent != null && excuteTurn != 0)
        {
            CardsHoldOn newcard = new CardsHoldOn(chainEvent,excuteTurn);
            if (_cardHoldOn.Count > 0)
            {
                _cardHoldOn.Insert(_cardHoldOn.Count-1,newcard);
            }
            else
            {
                _cardHoldOn.Insert(0,newcard);
            }
            
            _history.DevRecord("Add "+ newcard.Card.cardName + " To CardHoldOn and will Excute In " + newcard.TurnLeftToExcute);
        }

        else if (chainEvent != null && excuteTurn == 0)
        {
            _displayCard.Enqueue(chainEvent);
        }
    }

    private void RandomCardInDeckToHoldOn(int numberCardToRandom)
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

            _history.DevRecord("add newCard For CardInDesk : " + newcard.Card.cardName);
            
            //อย่าลืม Remove ใน List ออก
            CardsInDeck.RemoveAt(randomIndex);
        }
    }
    
    public void NextCard()
    {
        if (_displayCard.Count == 0)
        {
            CurrentDisplayCard = null;
            Debug.LogWarning(_displayCard.Count);
            Debug.Log("End Turn");
            EndTurn();
        }
        //ไว้ใช้ตอนเลือกเสร็จแล้ว
        else
        {
            Debug.LogWarning(CurrentDisplayCard);
            Debug.LogWarning(_displayCard.Count);
            NextDisplayCard(_displayCard.Dequeue());
        }

  //      NextDisplayCard(_displayCard.Dequeue());
    }

    [ContextMenu("Execute LeftChoiceMethod")]
    public void LeftChoiceSelect()
    {
        _history.Record("Excute <color=red>LeftChoiceMethod</color> Of <b>" + CurrentDisplayCard.cardName + "</b>");
        if (CurrentDisplayCard is DefaultCard)
        {
            DefaultCard ThisCard = CurrentDisplayCard as DefaultCard;
            //Update Resource ทันที
            UpdateResource(ThisCard.leftMoney,ThisCard.leftHappiness,ThisCard.leftPower,ThisCard.leftStability);
            StartCoroutine(UpdateResource());
            //คำนวน Possible Event
           CalculatePossibility(ThisCard.leftPossibleChainCards);
        }
        else
        {
            NotifyCard ThisCard = CurrentDisplayCard as NotifyCard;
            UpdateResource(ThisCard.Money,ThisCard.Happiness,ThisCard.Power,ThisCard.Stability);
            StartCoroutine(UpdateResource());
            //คำนวน Possible Event
            CalculatePossibility(ThisCard.possibleChainCards);
        }
        if(_cardHoldOn.Count >= 0)
        {
            NextCard();
        }
    }

    [ContextMenu("Execute RightChoiceMethod")]
    public void RightChoiceSelect()
    {
        _history.Record("Excute <color=green>RightChoiceMethod</color> Of <b>" + CurrentDisplayCard.cardName + "</b>");
        if (CurrentDisplayCard is DefaultCard)
        {
            DefaultCard ThisCard = CurrentDisplayCard as DefaultCard;
            //Update Resource ทันที
            UpdateResource(ThisCard.rightMoney,ThisCard.rightHappiness,ThisCard.rightPower,ThisCard.rightStability);
            StartCoroutine(UpdateResource());

            //คำนวน Possible Event
            CalculatePossibility(ThisCard.rightPossibleChainCards);
        }
        else
        {
            NotifyCard ThisCard = CurrentDisplayCard as NotifyCard;
            UpdateResource(ThisCard.Money,ThisCard.Happiness,ThisCard.Power,ThisCard.Stability);
            StartCoroutine(UpdateResource());
            //คำนวน Possible Event
            CalculatePossibility(ThisCard.possibleChainCards);
        }
        if(_cardHoldOn.Count >= 0)
        {
            NextCard();
        }
        
    }

    private void CalculatePossibility(List<Card.PossibleChainCard> Cards)
    {
        Random random = new Random();
        int randomNumber = random.Next(1, 101);
        
        int previousPossibleOutcome = 0;
        foreach (var VARIABLE in Cards)
        {
            if (randomNumber > VARIABLE.PossibleOutcome+previousPossibleOutcome)
            {
                previousPossibleOutcome += VARIABLE.PossibleOutcome;
                continue;
            }
            else
            {
                AddCardsHoldOn(VARIABLE.ChainCard,VARIABLE.ExcuteChainCardIn);
                break;
            }
        }
    }

    #region ConditionEvent&Ending

    private void CheckCondition()
    {
        //ถ้า ถึงเท่านี้ๆ ให้ขึ้นเตือนก่อน ถ้าเทิร์นต่อไปยังอยู่อีกให้สุ่ม Event ร้ายมา
    }

    private void EndLastTurn()
    {
        //Show Ending Result ทั้งหมด
    }

    #endregion
    
    #region SaveAndLoad
    public void NewGame()
    {
        CurrentMoney = StartMoney;
        CurrentHappiness = StartHappiness;
        CurrentPower = StartPower;
        CurrentStability = StartStability;
        MaxTurn = _MaxTurn;
        CurrentTurn = 0;
        
        Card[] cards = Resources.LoadAll<Card>("FixEvent");
        CardsInDeck.AddRange(cards);
        RandomCardInDeckToHoldOn(RandomCardPerTurn);
        DeleteAllChildren();
        StartTurn();
       
    }
    public void ContinueGame()
    {
        SaveGameManager.LoadGame();
        Debug.LogWarning("load");
        CardsHoldOn newcard = new CardsHoldOn(CurrentDisplayCard,0);
        _cardHoldOn.Insert(0,newcard);
      //  StartCoroutine(UpdateResource());

        //  StartTurn();
        DeleteAllChildren();
      LoadStartTurn();
    }
    
    private void LoadStartTurn()
    {
      
        _history.Record("LoadStartTurn" + CurrentTurn);

        //_history.Record("StartTurn : " + CurrentTurn + " / " + MaxTurn);
        //foreach checkCard Turn = 0 ให้ display Card
        if (_cardHoldOn.Count > 0)
        {
            for (int i = _cardHoldOn.Count - 1; i >= 0; i--)
            {
                if (_cardHoldOn[i].TurnLeftToExcute == 0)
                {
                    _history.DevRecord("CardHoldOn For DeckCard ReadyToExecute: " + _cardHoldOn[i].Card.cardName);
                    _displayCard.Enqueue(_cardHoldOn[i].Card);
                    _cardHoldOn.RemoveAt(i);
                }
            }
        }

        if (_displayCard.Count > 0)
        { 
            DisplayCard(_displayCard.Dequeue());
            //   NextDisplayCard(_displayCard.Dequeue());
        }
        
        _history.DevRecord("Have CardsIn Deck Left : " + CardsInDeck.Count);
        
        StartCoroutine(UpdateResource());
    }
    public void DeleteAllChildren()
    {
        // Iterate through all child GameObjects
        foreach (Transform child in cardParent.transform)
        {
            // Destroy each child GameObject
            Destroy(child.gameObject);
        }
    }
   
    public void SaveAndQuit()
    {
        SaveGameManager.SaveGame();
        Debug.LogWarning("save");
    }
    
    #endregion

   
}
