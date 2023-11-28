using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewCardController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _initialPosition;
    private Vector3 leftinfo = new Vector3(140, 0, 0);
    private Vector3 Rightinfo = new Vector3(-140, 0, 0);
    private Vector3 _initialRotation;
    private float _distanceMoved;
    private bool _swipeLeft;
    float duration = 0.5f; // Adjust the duration as needed for the desired smoothness.
    private float elapsedTime = 0f;
    public static float cardHoldOnTime = 0f;
    private StateCard currenState = StateCard.None;
    private float time = 0;

    public static float timeToLockCard = 1.8f;
    
    private bool ChoiceSelectCalled = false;
    
    public event Action cardMoved;
    private ChoiceDisplay CD;

    private void Start()
    {
        CD = FindObjectOfType<ChoiceDisplay>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        currenState = StateCard.None;
            transform.localPosition =
                new Vector2(transform.localPosition.x + eventData.delta.x, transform.localPosition.y);
            if (transform.localPosition.x - _initialPosition.x > 0) //ไปทางขวา?
            {
                transform.localEulerAngles = new Vector3(0, 0,
                    Mathf.LerpAngle(0, -10, (_initialPosition.x + transform.localPosition.x) / (Screen.width / 2)));
                
            }
            else
            {
                transform.localEulerAngles = new Vector3(0, 0,
                    Mathf.LerpAngle(0, 10, (_initialPosition.x - transform.localPosition.x) / (Screen.width / 2)));
            }
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _initialPosition = transform.localPosition;
        _initialRotation = transform.localEulerAngles;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        cardHoldOnTime = 0f;
        _distanceMoved = Mathf.Abs(transform.localPosition.x - _initialPosition.x);
        if (_distanceMoved < 0.4 * Screen.width)
        {
            elapsedTime = 0;
            currenState = StateCard.ReturnCard;
        }
        else
        {
            if (transform.localPosition.x > _initialPosition.x)
            {
                
                _swipeLeft = false;
            }
            else
            {
                
                _swipeLeft = true;
            }
            currenState = StateCard.CardMove;
        }
    }
    private void MoveCard()
    {
        if (time < 0.2f)
        {
            time += Time.deltaTime;

            if (_swipeLeft)
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x,
                    transform.localPosition.x - Screen.width, time), transform.localPosition.y, 0);
                GetComponent<Image>().color = Color.Lerp(Color.white, Color.red, Mathf.SmoothStep(1, 0, 4 * time));
                
                if (!ChoiceSelectCalled) // Check if RightChoiceSelect was not called before
                {
                    FindObjectOfType<GameManager>().LeftChoiceSelect();
                    ChoiceSelectCalled = true; // Set the flag to true after calling
                }
            }
            else
            {
                transform.localPosition = new Vector3(Mathf.SmoothStep(transform.localPosition.x,
                    transform.localPosition.x + Screen.width, time), transform.localPosition.y, 0);
                GetComponent<Image>().color = Color.Lerp(Color.white, Color.green, Mathf.SmoothStep(1, 0, 4 * time));
                
                if (!ChoiceSelectCalled) // Check if RightChoiceSelect was not called before
                {
                    FindObjectOfType<GameManager>().RightChoiceSelect();
                    ChoiceSelectCalled = true; // Set the flag to true after calling
                }
            }
            //  GetComponent<Image>().color = new Color(1, 1, 1, Mathf.SmoothStep(1, 0, 4 * time));
        }
        else
        {
            cardMoved?.Invoke();
            Destroy(gameObject);
            ChoiceSelectCalled = false;
         
        }

    }

    private void ReturnCardToPosition()
    {
        Quaternion initialRotation = Quaternion.Euler(new Vector3(0, 0, 0)); // Set the initial rotation to zero degrees around the z-axis

        if (elapsedTime < duration)
        {
            transform.localPosition = Vector3.Slerp(transform.localPosition, new Vector3(0, 0, 0), elapsedTime / duration);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, initialRotation, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
        }
        else
        {
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = initialRotation;
            currenState = StateCard.None;
        }
    }


    private void ExcuteOutcome(bool swipleft)
    {
        
    }

    private void Update()
    {
        if (!CD.choiceHoldOn)
        {
            if (transform.localPosition.x > Rightinfo.x && transform.localPosition.x > leftinfo.x)
            {
                cardHoldOnTime += Time.deltaTime;
                // Debug.LogWarning("Show info Right");
               // GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, Color.gray, Time.deltaTime);

                CD.ShowChoiceRight(GetComponent<CardFoundation>().cardData);

                if (cardHoldOnTime >= timeToLockCard)
                {
                    CD.LockCardHoldOn();
                }
            }
            else if (transform.localPosition.x < leftinfo.x && transform.localPosition.x < Rightinfo.x)
            {
                cardHoldOnTime += Time.deltaTime;
                //  Debug.LogWarning("Show info Left");
              //  GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, Color.gray, Time.deltaTime);

                CD.ShowChoiceLeft(GetComponent<CardFoundation>().cardData);

                if (cardHoldOnTime >= timeToLockCard)
                {
                   CD.LockCardHoldOn();
                }
            }
            else
            {
                //กลับมาที่เดิม
                cardHoldOnTime = 0f;
               // GetComponent<Image>().color = Color.Lerp(GetComponent<Image>().color, Color.white, Time.deltaTime);

                if (!CD.choiceHoldOn)
                {
                    CD.Close();
                }
            }
        }
        else
        {
            currenState = StateCard.ReturnCard;
        }

        //Debug.LogWarning(currenState.ToString());
        switch (currenState)
        {
            case StateCard.None:
                break;
            case StateCard.CardMove:
                MoveCard();
                break;
            case StateCard.ReturnCard:
                ReturnCardToPosition();
                break;
            case StateCard.ExcuteOutcome:
                break;
        }

    }
    

    private enum StateCard
    {
        None,
        CardMove,
        ExcuteOutcome,
        ReturnCard
    }
}