using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RarityManager : MonoBehaviour
{
    static RarityManager instance;
    public static RarityManager i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<RarityManager>();
            }
            return instance;
        }
    }
    
    [SerializeField] private List<SO_Rarity> allRarities;

    public float GetTotalRarityWeight()
    {
        float weight = 0;
        foreach (var VARIABLE in allRarities)
        {
            weight += VARIABLE.baseWeight;
        }
        return weight;
    } 

    public SO_Rarity RollRarity()
    {
        float targetWeight = UnityEngine.Random.Range(0, GetTotalRarityWeight());
        float weightCount = 0;
        foreach (var VARIABLE in allRarities)
        {
            weightCount += VARIABLE.baseWeight;
            if (weightCount >= targetWeight) return VARIABLE;
        }
        return allRarities[allRarities.Count - 1];
    }
}
