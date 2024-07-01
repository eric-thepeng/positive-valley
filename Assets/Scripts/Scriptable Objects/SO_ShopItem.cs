using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "ScriptableObjects/Player Items/Shop Item")]
public class SO_ShopItem : SO_PlayerItem, ICollectionItem
{
    public int unlockLevel;
    public int buyCost;
    public enum UnlockState {NeedToReachLevel, NeedToBuy, Unlocked}
    public UnlockState unlockState = UnlockState.NeedToReachLevel;

    public bool IsUnlocked()
    {
        return unlockState == UnlockState.Unlocked;
    }

    public bool IsLevelReached()
    {
        return unlockState != UnlockState.NeedToReachLevel;
    }

    public void SetLevelReached()
    {
        unlockState = UnlockState.NeedToBuy;
    }

    public void SetUnlocked()
    {
        unlockState = UnlockState.Unlocked;
    }

    #region ICollectionItem

    public int GetUnlockLevel() { return unlockLevel; }

    public BarnItemSet GetUnlockBarnItemSet() { return new BarnItemSet(); }

    #endregion
}
