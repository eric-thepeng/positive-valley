using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "ScriptableObjects/Shop/Shop Item")]
public class SO_ShopItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int unlockLevel;
    public int buyCost;
}
