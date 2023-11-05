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
            
            plusMoney.text = defaultCard.leftMoney.ToString();
            plusHappy.text = defaultCard.leftHappiness.ToString();
            plusPower.text = defaultCard.leftPower.ToString();
            plusStability.text = defaultCard.leftStability.ToString();

            sumMoney.text = (GameManager.CurrentMoney + defaultCard.leftMoney).ToString();
            sumHappy.text = (GameManager.CurrentHappiness + defaultCard.leftHappiness).ToString();
            sumPower.text = (GameManager.CurrentPower + defaultCard.leftPower).ToString();
            sumStability.text = (GameManager.CurrentStability + defaultCard.leftStability).ToString();
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

            plusMoney.text = defaultCard.rightMoney.ToString();
            plusHappy.text = defaultCard.rightHappiness.ToString();
            plusPower.text = defaultCard.rightPower.ToString();
            plusStability.text = defaultCard.rightStability.ToString();
            
            sumMoney.text = (GameManager.CurrentMoney + defaultCard.rightMoney).ToString();
            sumHappy.text = (GameManager.CurrentHappiness + defaultCard.rightHappiness).ToString();
            sumPower.text = (GameManager.CurrentPower + defaultCard.rightPower).ToString();
            sumStability.text = (GameManager.CurrentStability + defaultCard.rightStability).ToString();
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
