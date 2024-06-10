using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UI;

public class UI_PlayerStatDisplayer : MonoBehaviour
{
    [Header("Money Dependencies")] 
    [SerializeField] private GameObject moneyGameObject;
    [SerializeField] private bool displayMoney = true;
    [SerializeField] private TMP_Text moneyAmountTMPT;
    
    [Header("Experience Dependencies")]
    [SerializeField] private GameObject experienceGameObject;
    [SerializeField] private bool displayExperience = true;
    [SerializeField] private TMP_Text experienceAmountTMPT;
    
    [Header("Experience Dependencies")]
    [SerializeField] private GameObject levelGameObject;
    [SerializeField] private bool displayLevel = true;
    [SerializeField] private TMP_Text levelAmountTMPT;

    private void OnEnable()
    {
        PlayerStat.money.SubscribeChangeValue(RefreshMoneyDisplay);
        RefreshMoneyDisplay(0);
        
        PlayerStat.experience.SubscribeChangeValue(RefreshExperienceDisplay);
        RefreshExperienceDisplay(0);
        
        PlayerStat.level.SubscribeChangeValue(RefreshLevelDisplay);
        RefreshLevelDisplay(0);
    }

    private void OnDisable()
    {
        PlayerStat.money.UnsubscribeChangeValue(RefreshMoneyDisplay);
        PlayerStat.experience.UnsubscribeChangeValue(RefreshExperienceDisplay);
        PlayerStat.level.UnsubscribeChangeValue(RefreshLevelDisplay);
    }

    private void RefreshMoneyDisplay(int newValue)
    {
        moneyAmountTMPT.text = PlayerStat.money.GetValue() + "";
    }

    private void RefreshExperienceDisplay(int newValue)
    {
        experienceAmountTMPT.text = PlayerStat.experience.GetValue() + "/" + PlayerStat.GetLevelUpExpRequirement();
    }

    private void RefreshLevelDisplay(int newValue)
    {
        levelAmountTMPT.text = PlayerStat.level.GetValue() + "";
        RefreshExperienceDisplay(0);
    }
}
