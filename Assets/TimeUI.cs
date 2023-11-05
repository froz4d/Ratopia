using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = NewCardController.timeToLockCard;
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = NewCardController.cardHoldOnTime;
    }
}
