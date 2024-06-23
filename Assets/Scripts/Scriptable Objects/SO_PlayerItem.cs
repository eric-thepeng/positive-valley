using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Item", menuName = "ScriptableObjects/Player Items/Player Item")]
public class SO_PlayerItem : SerializedScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
}
