using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "ScriptableObjects/Player Items/Shop Item")]
public class SO_ShopItem : SO_PlayerItem, ICollectionItem
{
    public int unlockLevel;
    public int buyCost;
    public enum UnlockState {CanUnlock, CanBuy, Unlocked}
    public UnlockState unlockState;

    #region ICollectionItem

    public int GetUnlockLevel() { return unlockLevel; }

    public BarnItemSet GetUnlockBarnItemSet() { return new BarnItemSet(); }

    #endregion
}
