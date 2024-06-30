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

    // INITIALIZATION
    public BarnItemSet()
    {
        data = new List<BarnItem>();
    }

    public BarnItemSet(Dictionary<BarnItem,int> inputDic)
    {
        data = new List<BarnItem>();
        AddBarnItem(inputDic);
    }
    
    // ADDING OPERATIONS
    public void AddBarnItem(BarnItem biToAdd)
    {
        data.Add(biToAdd);
    }

    public void AddBarnItem(List<BarnItem> bilToAdd)
    {
        foreach (var VARIABLE in bilToAdd)
        {
            data.Add(VARIABLE);
        }
    }

    public void AddBarnItem(Dictionary<BarnItem, int> bidToAdd)
    {
        foreach (var kvp in bidToAdd)
        {
            for (int i = 0; i < kvp.Value; i++)
            {
                data.Add(kvp.Key);
            }
        }
    }
    
    // REMOVING OPERATIONS

    // DISPLAYING OPERATIONS
    
    
}