using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BarnItem
{
    public SOSI_Seed itemSeed;
    public SO_Rarity itemRarity;

    public BarnItem(SOSI_Seed itemSeed, SO_Rarity itemRarity)
    {
        this.itemSeed = itemSeed;
        this.itemRarity = itemRarity;
    }

    public int GetSellPrice()
    {
        return itemSeed.GetSellPriceByRarity(itemRarity);
    }
}

public class BarnItemSet
{
    public List<BarnItem> data;

    public BarnItemSet()
    {
        data = new List<BarnItem>();
    }

    public BarnItemSet(Dictionary<BarnItem,int> inputDic)
    {
        data = new List<BarnItem>();
        foreach (var kvp in inputDic)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                data.Add(kvp.Key);
            }
        }
    }
    
    
}