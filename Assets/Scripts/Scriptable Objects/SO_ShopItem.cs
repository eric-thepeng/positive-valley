using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "ScriptableObjects/Player Items/Shop Item")]
public class SO_ShopItem : SO_PlayerItem
{
    public int unlockLevel;
    public int buyCost;
}
