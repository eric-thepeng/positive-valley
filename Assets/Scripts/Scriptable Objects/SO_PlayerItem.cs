using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Item", menuName = "ScriptableObjects/Player Items/Player Item")]
public class SO_PlayerItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
}
