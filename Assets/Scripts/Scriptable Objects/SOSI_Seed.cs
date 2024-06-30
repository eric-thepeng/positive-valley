using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New SOSI Seed", menuName = "ScriptableObjects/Player Items/SOSI Seed")]
public class SOSI_Seed : SO_ShopItem
{
    public List<Sprite> phasesSprites;

    public int cropPhasesAmount { get { return phasesSprites.Count -1; } }

    public float cropPhaseGrowTime { get { return cropGrowTime / cropPhasesAmount; } }

    public float cropGrowTime;
    public int cropAmount;
    
    public int harvestExperience;

    public Dictionary<SO_Rarity.RarityIdentifier, int> sellPriceByRarity =
        new Dictionary<SO_Rarity.RarityIdentifier, int>()
        {
            { SO_Rarity.RarityIdentifier.Normal, 0 },
            { SO_Rarity.RarityIdentifier.Uncommon, 0 },
            { SO_Rarity.RarityIdentifier.Rare, 0 },
            { SO_Rarity.RarityIdentifier.Epic, 0 },
            { SO_Rarity.RarityIdentifier.Legendary, 0 },
            { SO_Rarity.RarityIdentifier.Divine, 0 },
        };

    public Dictionary<BarnItem, int> unlockRequiredBarnItems;

    public int GetSellPriceByRarity(SO_Rarity rarity)
    {
        return sellPriceByRarity[rarity.rarityIdentifier];
    }
}

