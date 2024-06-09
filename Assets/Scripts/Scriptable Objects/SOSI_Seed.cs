using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New SOSI Seed", menuName = "ScriptableObjects/Shop/SOSI Seed")]
public class SOSI_Seed : SO_ShopItem
{
    public List<Sprite> phasesSprites;
    public int totalPhases;
}
