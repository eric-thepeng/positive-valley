using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerStatDisplayer : MonoBehaviour
{
    [Header("Money Dependencies")]
    [SerializeField] private TMP_Text moneyAmountTMPT;
    [SerializeField] private Image moneyIconImage;

    private void OnEnable()
    {
        PlayerStat.money.SubscribeChangeValue(RefreshMoneyDisplay);
        RefreshMoneyDisplay(PlayerStat.money.GetValue());
    }

    private void OnDisable()
    {
        PlayerStat.money.UnsubscribeChangeValue(RefreshMoneyDisplay);
    }

    private void RefreshMoneyDisplay(int newValue)
    {
        moneyAmountTMPT.text = newValue + "";
    }
}
