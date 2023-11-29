using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class gamerulemanager : MonoBehaviour
{
    [SerializeField] private Slider SliderStartMoney;
    [SerializeField] private Slider SliderStartHappiness;
    [SerializeField] private Slider SliderStartPower;
    [SerializeField] private Slider SliderStartStability;
    [SerializeField] private Slider SliderMaxTurn;
    [SerializeField] private Slider SliderRandomCardPerTurn;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI HappinessText;
    [SerializeField] private TextMeshProUGUI PowerText;
    [SerializeField] private TextMeshProUGUI StabilityText;
    [SerializeField] private TextMeshProUGUI TurnText;
    [SerializeField] private TextMeshProUGUI RandomCardPerTurnText;

    // Update is called once per frame
    void Update()
    {
        // Sync text values with slider values
        moneyText.text = ((int) SliderStartMoney.value).ToString();
        HappinessText.text =  ((int)SliderStartHappiness.value).ToString();
        PowerText.text =  ((int)SliderStartPower.value).ToString();
        StabilityText.text = ((int) SliderStartStability.value).ToString();
        TurnText.text = ((int)SliderMaxTurn.value).ToString();
        RandomCardPerTurnText.text = ((int) SliderRandomCardPerTurn.value).ToString();
    }
}