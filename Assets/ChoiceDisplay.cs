using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceDisplay : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI choiceCard;
    [SerializeField]private TextMeshProUGUI cardChoiceParagraph;
    [SerializeField]private GameObject panel;

    [SerializeField] private TextMeshProUGUI sumMoney;
    [SerializeField] private TextMeshProUGUI sumHappy;
    [SerializeField] private TextMeshProUGUI sumPower;
    [SerializeField] private TextMeshProUGUI sumStability;

    [SerializeField] private TextMeshProUGUI plusMoney;
    [SerializeField] private TextMeshProUGUI plusHappy;
    [SerializeField] private TextMeshProUGUI plusPower;
    [SerializeField] private TextMeshProUGUI plusStability;

    [Header("Setting")] public float timeToHold;
    public bool choiceHoldOn = false;

    public void ShowChoiceLeft(Card inputCard)
    {
        panel.SetActive(true);
        if (inputCard is DefaultCard)
        {
            DefaultCard defaultCard = (DefaultCard)inputCard;
            choiceCard.text = defaultCard.leftTitle;
            cardChoiceParagraph.text = defaultCard.leftParagraph;

            choiceCard.alignment = TextAlignmentOptions.Baseline;
            cardChoiceParagraph.alignment = TextAlignmentOptions.Baseline;
            
            plusHappy.color = Color.red;
            plusMoney.color = Color.red;
            plusPower.color = Color.red;
            plusStability.color = Color.red;
            sumHappy.color = Color.red;
            sumMoney.color = Color.red;
            sumPower.color = Color.red;
            sumStability.color = Color.red;

                plusMoney.text = defaultCard.leftMoney.ToString();
            plusHappy.text = defaultCard.leftHappiness.ToString();
            plusPower.text = defaultCard.leftPower.ToString();
            plusStability.text = defaultCard.leftStability.ToString();

            if (defaultCard.leftHappiness > 0)
            {
                plusHappy.color = Color.green;
                sumHappy.color = Color.green;
            }
            else if (defaultCard.leftHappiness == 0)
            {
                plusHappy.color = Color.white;
                sumHappy.color = Color.white;
            }
            
            if (defaultCard.leftMoney > 0)
            {
                plusMoney.color = Color.green;
                sumMoney.color = Color.green;
            }
            else if (defaultCard.leftMoney == 0)
            {
                plusMoney.color = Color.white;
                sumMoney.color = Color.white;
            }
            
            if (defaultCard.leftPower > 0)
            {
                plusPower.color = Color.green;
                sumPower.color = Color.green;
            }
            else if (defaultCard.leftPower == 0)
            {
                plusPower.color = Color.white;
                sumPower.color = Color.white;
            }
            
            if (defaultCard.leftStability > 0)
            {
                plusStability.color = Color.green;
                sumStability.color = Color.green;
            }
            else if (defaultCard.leftStability == 0)
            {
                plusStability.color = Color.white;
                sumStability.color = Color.white;
            }
            
            sumMoney.text = (GameManager.CurrentMoney + defaultCard.leftMoney).ToString();
            sumHappy.text = (GameManager.CurrentHappiness + defaultCard.leftHappiness).ToString();
            sumPower.text = (GameManager.CurrentPower + defaultCard.leftPower).ToString();
            sumStability.text = (GameManager.CurrentStability + defaultCard.leftStability).ToString();
        }
        else if(inputCard is NotifyCard)
        {
            NotifyCard notifyCard = (NotifyCard)inputCard;
            choiceCard.text = notifyCard.cardName;
            cardChoiceParagraph.text = notifyCard.Paragraph;
            
            choiceCard.alignment = TextAlignmentOptions.Baseline;
            cardChoiceParagraph.alignment = TextAlignmentOptions.Baseline;
            
            plusHappy.color = Color.red;
            plusMoney.color = Color.red;
            plusPower.color = Color.red;
            plusStability.color = Color.red;
            sumHappy.color = Color.red;
            sumMoney.color = Color.red;
            sumPower.color = Color.red;
            sumStability.color = Color.red;

            plusMoney.text = notifyCard.Money.ToString();
            plusHappy.text = notifyCard.Happiness.ToString();
            plusPower.text = notifyCard.Power.ToString();
            plusStability.text = notifyCard.Stability.ToString();
            
            if (notifyCard.Happiness > 0)
            {
                plusHappy.color = Color.green;
                sumHappy.color = Color.green;
            }
            else if (notifyCard.Happiness == 0)
            {
                plusHappy.color = Color.white;
                sumHappy.color = Color.white;
            }
            
            if (notifyCard.Money > 0)
            {
                plusMoney.color = Color.green;
                sumMoney.color = Color.green;
            }
            else if (notifyCard.Money == 0)
            {
                plusMoney.color = Color.white;
                sumMoney.color = Color.white;
            }
            
            if (notifyCard.Power > 0)
            {
                plusPower.color = Color.green;
                sumPower.color = Color.green;
            }
            else if (notifyCard.Power == 0)
            {
                plusPower.color = Color.white;
                sumPower.color = Color.white;
            }
            
            if (notifyCard.Stability > 0)
            {
                plusStability.color = Color.green;
                sumStability.color = Color.green;
            }
            else if (notifyCard.Stability == 0)
            {
                plusStability.color = Color.white;
                sumStability.color = Color.white;
            }
            
            sumMoney.text = (GameManager.CurrentMoney + notifyCard.Money).ToString();
            sumHappy.text = (GameManager.CurrentHappiness + notifyCard.Happiness).ToString();
            sumPower.text = (GameManager.CurrentPower + notifyCard.Power).ToString();
            sumStability.text = (GameManager.CurrentStability + notifyCard.Stability).ToString();
        }
    }
    public void ShowChoiceRight(Card inputCard)
    {
        panel.SetActive(true);
        if (inputCard is DefaultCard)
        {
            DefaultCard defaultCard = (DefaultCard)inputCard;
            choiceCard.text = defaultCard.rightTitle;
            cardChoiceParagraph.text = defaultCard.rightParagraph;
            
            choiceCard.alignment = TextAlignmentOptions.Capline;
            cardChoiceParagraph.alignment = TextAlignmentOptions.Capline;

            plusHappy.color = Color.red;
            plusMoney.color = Color.red;
            plusPower.color = Color.red;
            plusStability.color = Color.red;
            sumHappy.color = Color.red;
            sumMoney.color = Color.red;
            sumPower.color = Color.red;
            sumStability.color = Color.red;
            
            plusMoney.text = defaultCard.rightMoney.ToString();
            plusHappy.text = defaultCard.rightHappiness.ToString();
            plusPower.text = defaultCard.rightPower.ToString();
            plusStability.text = defaultCard.rightStability.ToString();
            
            if (defaultCard.rightHappiness > 0)
            {
                plusHappy.color = Color.green;
                sumHappy.color = Color.green;
            }
            else if (defaultCard.rightHappiness == 0)
            {
                plusHappy.color = Color.white;
                sumHappy.color = Color.white;
            }
            
            if (defaultCard.rightMoney > 0)
            {
                plusMoney.color = Color.green;
                sumMoney.color = Color.green;
            }
            else if (defaultCard.rightMoney == 0)
            {
                plusMoney.color = Color.white;
                sumMoney.color = Color.white;
            }
            
            if (defaultCard.rightPower > 0)
            {
                plusPower.color = Color.green;
                sumPower.color = Color.green;
            }
            else if (defaultCard.rightPower == 0)
            {
                plusPower.color = Color.white;
                sumPower.color = Color.white;
            }
            
            if (defaultCard.rightStability > 0)
            {
                plusStability.color = Color.green;
                sumStability.color = Color.green;
            }
            else if (defaultCard.rightStability == 0)
            {
                plusStability.color = Color.white;
                sumStability.color = Color.white;
            }
            
            sumMoney.text = (GameManager.CurrentMoney + defaultCard.rightMoney).ToString();
            sumHappy.text = (GameManager.CurrentHappiness + defaultCard.rightHappiness).ToString();
            sumPower.text = (GameManager.CurrentPower + defaultCard.rightPower).ToString();
            sumStability.text = (GameManager.CurrentStability + defaultCard.rightStability).ToString();
        }
        else if(inputCard is NotifyCard)
        {
            NotifyCard notifyCard = (NotifyCard)inputCard;
            choiceCard.text = notifyCard.cardName;
            cardChoiceParagraph.text = notifyCard.Paragraph;
            
            choiceCard.alignment = TextAlignmentOptions.Baseline;
            cardChoiceParagraph.alignment = TextAlignmentOptions.Baseline;
            
            plusHappy.color = Color.red;
            plusMoney.color = Color.red;
            plusPower.color = Color.red;
            plusStability.color = Color.red;
            sumHappy.color = Color.red;
            sumMoney.color = Color.red;
            sumPower.color = Color.red;
            sumStability.color = Color.red;

            plusMoney.text = notifyCard.Money.ToString();
            plusHappy.text = notifyCard.Happiness.ToString();
            plusPower.text = notifyCard.Power.ToString();
            plusStability.text = notifyCard.Stability.ToString();
            
            if (notifyCard.Happiness > 0)
            {
                plusHappy.color = Color.green;
                sumHappy.color = Color.green;
            }
            else if (notifyCard.Happiness == 0)
            {
                plusHappy.color = Color.white;
                sumHappy.color = Color.white;
            }
            
            if (notifyCard.Money > 0)
            {
                plusMoney.color = Color.green;
                sumMoney.color = Color.green;
            }
            else if (notifyCard.Money == 0)
            {
                plusMoney.color = Color.white;
                sumMoney.color = Color.white;
            }
            
            if (notifyCard.Power > 0)
            {
                plusPower.color = Color.green;
                sumPower.color = Color.green;
            }
            else if (notifyCard.Power == 0)
            {
                plusPower.color = Color.white;
                sumPower.color = Color.white;
            }
            
            if (notifyCard.Stability > 0)
            {
                plusStability.color = Color.green;
                sumStability.color = Color.green;
            }
            else if (notifyCard.Stability == 0)
            {
                plusStability.color = Color.white;
                sumStability.color = Color.white;
            }
            
            sumMoney.text = (GameManager.CurrentMoney + notifyCard.Money).ToString();
            sumHappy.text = (GameManager.CurrentHappiness + notifyCard.Happiness).ToString();
            sumPower.text = (GameManager.CurrentPower + notifyCard.Power).ToString();
            sumStability.text = (GameManager.CurrentStability + notifyCard.Stability).ToString();
        }
        
    }

    public void LockCardHoldOn()
    {
        choiceHoldOn = true;
        panel.gameObject.GetComponent<Image>().raycastTarget = true;
    }

    public void UnlockCardHoldOn(bool yes)
    {
        choiceHoldOn = yes;
    }

    public void Close()
    {
        panel.SetActive(false);
        panel.gameObject.GetComponent<Image>().raycastTarget = false;
    }
}
