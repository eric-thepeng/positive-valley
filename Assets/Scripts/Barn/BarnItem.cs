using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]public struct BarnItem
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

public class SerializableBarnItemSetInput
{
    
}

/*
 * 属性：
 * 1. SOSI
 * 2. 品质
 *
 * --- 自动根据品质sort
 * 3. 数量
 */
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