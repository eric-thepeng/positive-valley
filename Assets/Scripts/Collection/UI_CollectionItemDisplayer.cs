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
        if (seed.unlockLevel < level) // Display need to reach level
        {
            needToReachLevelGO.SetActive(false);
            needToUnlockGO.SetActive(true);

            iconImage.sprite = seed.itemIcon;
            descriptionText.text = "test test need to spend barn item";
        }
        else // Display need to unlock
        {
            needToReachLevelGO.SetActive(true);
            needToUnlockGO.SetActive(false);
            
            unlockLevelText.text = "Reach Level " + seed.GetUnlockLevel();
        }
    }

    private void OnDisable()
    {
        PlayerStat.level.UnsubscribeChangeValue(RefreshDisplay);
    }
}
