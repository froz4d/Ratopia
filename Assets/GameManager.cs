using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;


public class GameManager : MonoBehaviour
{
    #region Setting & Current Status

    private static GameManager instance;
    private static History _history;
    private static CardBook _cardBook;
    public Slider SliderStartMoney;
    public Slider SliderStartHappiness;
    public Slider SliderStartPower;
    public Slider SliderStartStability;
    public Slider SliderMaxTurn;
    public Slider SliderRandomCardPerTurn;
    //ไว้โชว์ Devlog
    public static bool ShowDevLog = true;
    
    //ใส่เกินได้ ใน Inspector เดี่ยวมันปรับให้เอง
    public static int CurrentTurn = 0;

    public static int CurrentMoney
    {
        get { return _currentMoney; }
        set { _currentMoney = Clamp(value, -10, 10); }
    }

    private static int _currentMoney;

    public static int CurrentHappiness
    {
        get { return _currentHappiness; }
        set { _currentHappiness = Clamp(value, -10, 10); }
    }

    private static int _currentHappiness;

    public static int CurrentPower
    {
        get { return _currentPower; }
        set { _currentPower = Clamp(value, -10, 10); }
    }

    private static int _currentPower;

    public static int CurrentStability
    {
        get { return _currentStability; }
        set { _currentStability = Clamp(value, -10, 10); }
    }

    private static int _currentStability;
    
    private static int Clamp(int value, int min, int max)
    {
        if (value < min)
        {
            return min;
        }
        else if (value > max)
        {
            return max;
        }
        else
        {
            return value;
        }
    }

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
        
        EndingPanel.SetActive(false);

        NewGame();

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
          //  NextDisplayCard(_displayCard.Dequeue());
        }
        
        _history.DevRecord("Have CardsIn Deck Left : " + CardsInDeck.Count);
        StartCoroutine(UpdateResource(0, 0, 0, 0));
    }

    public void EndTurn()
    {
        if (CurrentTurn != MaxTurn)
        {
            TranManager.current.OpenTranPanelWithFade();
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
            CollectRemainingResourceData();
            StartTurn();
        }
        else
        {
            CollectRemainingResourceData();
            CheckEndLastTurn();
        }
        
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
        
        cardFoundationScript.HideTilePanel();
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
        
        cardFoundationScript.HideTilePanel();
    }

    #endregion

    private IEnumerator UpdateResource(int money,int happy,int power,int stability)
    {
        if (money >= 0)
        {
            moneyText.color = Color.green;
        }
        else if (money < 0)
        {
            moneyText.color = Color.red;
        }
        if (power >= 0)
        {
            PowerText.color = Color.green;
        }
        else if (power < 0)
        {
            PowerText.color = Color.red;
        }
        if (stability >= 0)
        {
            StabilityText.color = Color.green;
        }
        else if (stability < 0)
        {
            StabilityText.color = Color.red;
        }
        if (happy >= 0)
        {
            HappinessText.color = Color.green;
        }
        else if (happy < 0)
        {
            HappinessText.color = Color.red;
        }

        TurnText.text = CurrentTurn.ToString();
        moneyText.text = CurrentMoney.ToString();
        PowerText.text = CurrentPower.ToString();
        StabilityText.text = CurrentStability.ToString();
        HappinessText.text = CurrentHappiness.ToString();
        
        yield return new WaitForSeconds(0.5f);
        
        TurnText.color = Color.black;
        moneyText.color = Color.black;
        PowerText.color = Color.black;
        StabilityText.color = Color.black;
        HappinessText.color = Color.black;
    }
    

    private static void UpdateResource(int money, int happy, int power, int stability,string sample)
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
       /* CardsHoldOn newcard = new CardsHoldOn(CardsInDeck[numberCardToRandom]);
        _cardHoldOn.Add(newcard);*/
        //Random In Range มา ใน CardInDesk

        for (int i = 0; i < numberCardToRandom; i++)
        {
            if (CardsInDeck.Count == 0)
            {
                Debug.Log("Warning!! Card In Desk Is Out");
                break;
            }

            int randomIndex = Random.Range(0, CardsInDeck.Count - 1);

         //   Debug.Log("random number : " + randomIndex);

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
         //   Debug.LogWarning(_displayCard.Count);
        //    Debug.Log("End Turn");
            EndTurn();
        }
        //ไว้ใช้ตอนเลือกเสร็จแล้ว
        else
        {
        //    Debug.LogWarning(CurrentDisplayCard);
        //   Debug.LogWarning(_displayCard.Count);
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
            UpdateResource(ThisCard.leftMoney,ThisCard.leftHappiness,ThisCard.leftPower,ThisCard.leftStability,"simple");
            StartCoroutine(UpdateResource(ThisCard.leftMoney,ThisCard.leftHappiness,ThisCard.leftPower,ThisCard.leftStability));
            //คำนวน Possible Event
            if (ThisCard.leftPossibleChainCards != null)
            {
                CalculatePossibility(ThisCard.leftPossibleChainCards);
            }
           
        }
        else
        {
            NotifyCard ThisCard = CurrentDisplayCard as NotifyCard;
            UpdateResource(ThisCard.Money,ThisCard.Happiness,ThisCard.Power,ThisCard.Stability,"simple");
            StartCoroutine(UpdateResource(ThisCard.Money,ThisCard.Happiness,ThisCard.Power,ThisCard.Stability));
            //คำนวน Possible Event
            if (ThisCard.possibleChainCards != null)
            {
                CalculatePossibility(ThisCard.possibleChainCards);
            }
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
            UpdateResource(ThisCard.rightMoney,ThisCard.rightHappiness,ThisCard.rightPower,ThisCard.rightStability,"simple");
            StartCoroutine(UpdateResource(ThisCard.rightMoney,ThisCard.rightHappiness,ThisCard.rightPower,ThisCard.rightStability));

            //คำนวน Possible Event
            if (ThisCard.rightPossibleChainCards != null)
            {
                CalculatePossibility(ThisCard.rightPossibleChainCards);
            }
            
        }
        else
        {
            NotifyCard ThisCard = CurrentDisplayCard as NotifyCard;
            UpdateResource(ThisCard.Money,ThisCard.Happiness,ThisCard.Power,ThisCard.Stability,"simple");
            StartCoroutine(UpdateResource(ThisCard.Money,ThisCard.Happiness,ThisCard.Power,ThisCard.Stability));
            //คำนวน Possible Event
            if (ThisCard.possibleChainCards != null)
            {
                CalculatePossibility(ThisCard.possibleChainCards);
            }
        }
        if(_cardHoldOn.Count >= 0)
        {
            NextCard();
        }
        
    }

    private void CalculatePossibility(List<Card.PossibleChainCard> Cards)
    {
        
        int randomNumber = Random.Range(1, 101);
        
        int previousPossibleOutcome = 0;
        foreach (var VARIABLE in Cards)
        {
            if (VARIABLE.PossibleOutcome == 100)
            {
                AddCardsHoldOn(VARIABLE.ChainCard,VARIABLE.ExcuteChainCardIn);
                continue;
            }
            
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
    private Card GenerateNewCard()
    {
        NotifyCard card = ScriptableObject.CreateInstance<NotifyCard>();
        int money = Random.Range(0, 3);
        int happy = Random.Range(0, 3);
        int power = Random.Range(0, 3);
        int stabi = Random.Range(0, 3);
        
        card.setDefault();
        card.setValue(money,happy,power,stabi);

        return card;
    }

    private void AddGenCardInToDesk(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CardsInDeck.Add(GenerateNewCard());
        }
    }
    

    #region ConditionEvent&Ending

    
    private int crisisTriggerCount = 0;
    public static List<int> _remainingHappinessInEndTurn = new List<int>();
    public static List<int> _remainingMoneyInEndTurn = new List<int>();
    public static List<int> _remainingPowerInEndTurn = new List<int>();
    public static List<int> _remainingStabilityInEndTurn = new List<int>();
    [Header("ConditionCrisis")]
    public int criticalValue;
    public int crisisTrigger;
    public List<Card> crisisHappiness = new List<Card>();
    public List<Card> crisisMoney = new List<Card>();
    public List<Card> crisisPower = new List<Card>();
    public List<Card> crisisStability = new List<Card>();

    public Transform EndingParentShow;
    public Card HappyEnd;
    public Card MoneyEnd;
    public Card PowerEnd;
    public Card StabilityEnd;
    public Card DefaultEnd;

    public int EndingPointCondition;
    
    private void CheckCondition()
    {
        //ถ้า ถึงเท่านี้ๆ ให้ขึ้นเตือนก่อน ถ้าเทิร์นต่อไปยังอยู่อีกให้สุ่ม Event ร้ายมา
        if (CurrentHappiness <= criticalValue || CurrentMoney <= criticalValue || CurrentPower <= criticalValue || CurrentStability <= criticalValue)
        {
            crisisTriggerCount++;
        }
        else
        {
            crisisTriggerCount = 0;
        }

        if (crisisTriggerCount >= crisisTrigger)
        {
            if (CurrentHappiness <= criticalValue)
            {
                AddCrisisEvent(crisisHappiness);
            }
            if (CurrentMoney <= criticalValue)
            {
                AddCrisisEvent(crisisMoney);
            }
            if (CurrentPower <= criticalValue)
            {
                AddCrisisEvent(crisisPower);
            }
            if (CurrentStability <= criticalValue)
            {
                AddCrisisEvent(crisisStability);
            }
        }
        
    }

    private void AddCrisisEvent(List<Card> listEvent)
    {
        int randomIndex = Random.Range(0, listEvent.Count);

        //จากนั้นเรียก ใส่ใน CardHoldOn แล้วเอาเข้า list _CardHoldOn
        CardsHoldOn newcard = new CardsHoldOn(listEvent[randomIndex]);
        _cardHoldOn.Add(newcard);
        _history.DevRecord("add newCard For Crisis : " + newcard.Card.cardName);
    }

    public GameObject EndingPanel;
    private void CheckEndLastTurn()
    {
        double averageHappiness = _remainingHappinessInEndTurn.Average();
        double averageMoney = _remainingMoneyInEndTurn.Average();
        double averagePower = _remainingPowerInEndTurn.Average();
        double averageStability = _remainingStabilityInEndTurn.Average();
        //Show Ending Result ทั้งหมด
        EndingPanel.SetActive(true);

        if (averageHappiness >= EndingPointCondition)
        {
            Instantiate(cardFoundation, EndingParentShow);
            cardFoundation.GetComponent<CardFoundation>().ShowCardDisplay(HappyEnd);
            _cardBook.AddCardToCollection(HappyEnd);
        }
        if (averagePower >= EndingPointCondition)
        {
            Instantiate(cardFoundation, EndingParentShow);
            cardFoundation.GetComponent<CardFoundation>().ShowCardDisplay(PowerEnd);
            _cardBook.AddCardToCollection(PowerEnd);
        }
        if (averageMoney >= EndingPointCondition)
        {
            Instantiate(cardFoundation, EndingParentShow);
            cardFoundation.GetComponent<CardFoundation>().ShowCardDisplay(MoneyEnd);
            _cardBook.AddCardToCollection(MoneyEnd);
        }
        if (averageStability >= EndingPointCondition)
        {
            Instantiate(cardFoundation, EndingParentShow);
            cardFoundation.GetComponent<CardFoundation>().ShowCardDisplay(StabilityEnd);
            _cardBook.AddCardToCollection(StabilityEnd);
        }
        
        Instantiate(cardFoundation, EndingParentShow);
        cardFoundation.GetComponent<CardFoundation>().ShowCardDisplay(DefaultEnd);
        _cardBook.AddCardToCollection(DefaultEnd);

    }

    private void CollectRemainingResourceData()
    {
        _remainingHappinessInEndTurn.Add(CurrentHappiness);
        _remainingMoneyInEndTurn.Add(CurrentMoney);
        _remainingPowerInEndTurn.Add(CurrentPower);
        _remainingStabilityInEndTurn.Add(CurrentStability);
        
    }

    #endregion
    
    #region SaveAndLoad
    public void NewGame()
    {
        CardsInDeck.Clear();
        _cardHoldOn.Clear();
        _displayCard.Clear();

        CurrentMoney = (int) SliderStartMoney.value;
        CurrentHappiness = (int) SliderStartHappiness.value;
        CurrentPower = (int) SliderStartPower.value;
        CurrentStability = (int) SliderStartStability.value;
        MaxTurn = (int) SliderMaxTurn.value;
        CurrentTurn = 0;
        _remainingHappinessInEndTurn.Clear();
        _remainingMoneyInEndTurn.Clear();
        _remainingPowerInEndTurn.Clear();
        _remainingStabilityInEndTurn.Clear();
        
        Card[] cards = Resources.LoadAll<Card>("FixEvent");
        CardsInDeck.AddRange(cards);
        AddGenCardInToDesk((MaxTurn*RandomCardPerTurn)-CardsInDeck.Count);
        RandomCardInDeckToHoldOn((int) SliderRandomCardPerTurn.value);
        DeleteAllChildren();
        StartTurn();
    
        EndingPanel.SetActive(false);
    }
    public void ContinueGame()
    {
        SaveGameManager.LoadGame();
     //   Debug.LogWarning("load");
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
        StartCoroutine(UpdateResource(0, 0, 0, 0));
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
      //  Debug.LogWarning("save");
    }
    
    #endregion

   
}
