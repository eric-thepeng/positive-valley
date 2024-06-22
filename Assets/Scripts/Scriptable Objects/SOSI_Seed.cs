using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New SOSI Seed", menuName = "ScriptableObjects/Shop/SOSI Seed")]
public class SOSI_Seed : SO_ShopItem
{
    public List<Sprite> phasesSprites;

    public int cropPhasesAmount { get { return phasesSprites.Count -1; } }

    public float cropPhaseGrowTime { get { return cropGrowTime / cropPhasesAmount; } }

    public float cropGrowTime;
    public int cropAmount;
    
    public int harvestMoney;
    public int harvestExperience;
}
