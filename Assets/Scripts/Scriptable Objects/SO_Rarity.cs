using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rarity", menuName = "ScriptableObjects/Rarity/Rarity")]
public class SO_Rarity : ScriptableObject
{
    public enum RarityIdentifier
    {
        Normal,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Divine
    }

    public RarityIdentifier rarityIdentifier;
    public Color color;
    public string rarityName;
    public float baseWeight;
}
