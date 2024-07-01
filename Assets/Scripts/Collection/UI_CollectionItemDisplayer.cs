using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
public interface ILevelUnlock
{
    public bool GetIsUnlocked();
    public void SetIsUnlocked(bool SetTo);
    public int GetUnlockLevel();
    public void OnUnlocked();

    /// <summary>
    /// Need to be added to the listeners of PlayerStat.level
    /// </summary>
    /// <param name="currentLevel"></param>
    public void UnlockCheck(int currentLevel)
    {
        if(GetIsUnlocked()) return;
        if (currentLevel >= GetUnlockLevel())
        {
            OnUnlocked();
            SetIsUnlocked(true);
        }
    }

}*/

public class UI_CollectionItemDisplayer : MonoBehaviour
{
    [SerializeField,Header("Need To Reach Level")] private GameObject needToReachLevelGO;
    [SerializeField] private TMP_Text unlockLevelText;

    [SerializeField,Header("Need To Unlock")] private GameObject needToUnlockGO;
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Button unlockButton;

    private SOSI_Seed seed;
    
    public void SetUp(SOSI_Seed seedToDisplay)
    {
        seed = seedToDisplay;
        PlayerStat.level.SubscribeChangeValue(RefreshDisplay);
        RefreshDisplay(PlayerStat.level.GetValue());
    }

    public void RefreshDisplay(int level)
    {
        // IS UNLOCKED
        if (seed.IsUnlocked())
        {
            needToReachLevelGO.SetActive(false);
            needToUnlockGO.SetActive(false);
            unlockLevelText.text = "UNLOCKED";
            return;
        }
            
        // NEED UNLOCK OR LEVEL
        if (seed.unlockLevel <= level) // Display need to unlock
        {
            needToReachLevelGO.SetActive(false);
            needToUnlockGO.SetActive(true);

            iconImage.sprite = seed.itemIcon;
            descriptionText.text = "test test need to spend barn item";

            unlockButton.onClick.RemoveAllListeners();
            unlockButton.onClick.AddListener(TryToUnlock);
        }
        else // Display need to reach level 
        {
            needToReachLevelGO.SetActive(true);
            needToUnlockGO.SetActive(false);
            
            unlockLevelText.text = "Reach Level " + seed.GetUnlockLevel();
        }
    }

    private void TryToUnlock()
    {
        if (BarnPanelManager.i.SpendBarnItems(seed.unlockRequiredBarnItems))
        {
            seed.SetUnlocked();
            RefreshDisplay(PlayerStat.level.GetValue());
        }
    }

    private void OnDisable()
    {
        PlayerStat.level.UnsubscribeChangeValue(RefreshDisplay);
    }
}
