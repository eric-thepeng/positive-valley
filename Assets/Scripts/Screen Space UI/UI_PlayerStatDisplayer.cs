using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.UI;

public class UI_PlayerStatDisplayer : MonoBehaviour
{
    [Header("Money Dependencies")] 
    [SerializeField] private GameObject moneyGameObject;
    [SerializeField] private bool displayMoney = true;
    [SerializeField] private bool moneyIncreaseAnimation = true;
    [SerializeField] private TMP_Text moneyAmountTMPT;
    
    [Header("Experience Dependencies")]
    [SerializeField] private GameObject experienceGameObject;
    [SerializeField] private bool displayExperience = true;
    [SerializeField] private bool experienceIncreaseAnimation = true;
    [SerializeField] private TMP_Text experienceAmountTMPT;
    
    [Header("Experience Dependencies")]
    [SerializeField] private GameObject levelGameObject;
    [SerializeField] private bool displayLevel = true;
    [SerializeField] private bool levelIncreaseAnimation = true;
    [SerializeField] private TMP_Text levelAmountTMPT;

    private void OnEnable()
    {
        // MONEY 
        PlayerStat.money.SubscribeChangeDelta(RefreshMoneyDisplay);
        RefreshMoneyDisplay(0);
        if (displayMoney) {moneyGameObject.SetActive(true); }
        else{moneyGameObject.SetActive(false); }

        // EXPERIENCE
        PlayerStat.experience.SubscribeChangeDelta(RefreshExperienceDisplay);
        RefreshExperienceDisplay(0);
        if (displayExperience) {experienceGameObject.SetActive(true); }
        else{experienceGameObject.SetActive(false); }
        
        // LEVEL
        PlayerStat.level.SubscribeChangeDelta(RefreshLevelDisplay);
        RefreshLevelDisplay(0);
        if (displayLevel) {levelGameObject.SetActive(true); }
        else{levelGameObject.SetActive(false); }
    }

    private void OnDisable()
    {
        PlayerStat.money.UnsubscribeChangeDelta(RefreshMoneyDisplay);
        PlayerStat.experience.UnsubscribeChangeDelta(RefreshExperienceDisplay);
        PlayerStat.level.UnsubscribeChangeDelta(RefreshLevelDisplay);
    }

    private void RefreshMoneyDisplay(int delta)
    {
        moneyAmountTMPT.text = PlayerStat.money.GetValue() + "";
        if (delta > 0 && moneyIncreaseAnimation)
        {
            moneyAmountTMPT.gameObject.transform.localScale = new Vector3(1, 1, 1);
            moneyAmountTMPT.gameObject.transform.DOKill();
            moneyAmountTMPT.gameObject.transform.DOPunchScale(new Vector3(.5f, .5f, .5f), 0.5f,3,0f);
        }
    }
    

    private void RefreshExperienceDisplay(int delta)
    {
        experienceAmountTMPT.text = PlayerStat.experience.GetValue() + "/" + PlayerStat.GetLevelUpExpRequirement();
        if (delta > 0 && experienceIncreaseAnimation)
        {
            experienceAmountTMPT.gameObject.transform.localScale = new Vector3(1, 1, 1);
            experienceAmountTMPT.gameObject.transform.DOKill();
            experienceAmountTMPT.gameObject.transform.DOPunchScale(new Vector3(.5f, .5f, .5f), 0.5f,3,0f);
        }
    }

    private void RefreshLevelDisplay(int delta)
    {
        levelAmountTMPT.text = PlayerStat.level.GetValue() + "";
        RefreshExperienceDisplay(delta);
        if (delta > 0 && levelIncreaseAnimation)
        {
            levelAmountTMPT.gameObject.transform.localScale = new Vector3(1, 1, 1);
            levelAmountTMPT.gameObject.transform.DOKill();
            levelAmountTMPT.gameObject.transform.DOPunchScale(new Vector3(.5f, .5f, .5f), 0.5f,3,0f);
        }
    }
}
