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

    [SerializeField, Header("Unlocked")] private GameObject unlockedGO;
    [SerializeField] private TMP_Text unlockedDescriptionText;

    private SOSI_Seed seed;
    private UI_BarnItemSetDisplayer barnItemSetDisplayer;
    
    public void SetUp(SOSI_Seed seedToDisplay)
    {
        barnItemSetDisplayer = GetComponent<UI_BarnItemSetDisplayer>();
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
            unlockedGO.SetActive(true);
            unlockedDescriptionText.text = seed.itemName + " UNLOCKED";
            return;
        }
            
        // NEED UNLOCK OR LEVEL
        // Display NEED TO UNLOCK
        if (seed.unlockLevel <= level) 
        {
            needToReachLevelGO.SetActive(false);
            needToUnlockGO.SetActive(true);

            iconImage.sprite = seed.itemIcon;
            //descriptionText.text = "test test need to spend barn item";

            unlockButton.onClick.RemoveAllListeners();
            unlockButton.onClick.AddListener(TryToUnlock);
            
            barnItemSetDisplayer.Display(new BarnItemSet(seed.unlockRequiredBarnItems));
        }
        // Display NEED TO REACH LEVEL
        else 
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
