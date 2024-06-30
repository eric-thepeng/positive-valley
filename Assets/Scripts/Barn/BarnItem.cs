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

/*
 * 属性：
 * 1. SOSI
 * 2. 品质
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

    public bool SpendBarnItems(Dictionary<BarnItem, int> toSpend)
    {
        if (!HasEnoughBarnItems(toSpend)) return false;

        foreach (var VARIABLE in toSpend)
        {
            for (int i = 0; i < VARIABLE.Value; i++)
            {
                data.Remove(VARIABLE.Key);
            }
        }
        
        return true;
    }

    public void RemoveBarnItemAtIndex(int removingIndex)
    {
        data.RemoveAt(removingIndex);
    }

    // ORGANIZING OPERATIONS
    
    public Dictionary<BarnItem, int> GetDataInfoDictionary(bool sortByRarity = false)
    {
        Dictionary<BarnItem, int> export = new Dictionary<BarnItem, int>();
        foreach (var VARIABLE in data)
        {
            if (export.ContainsKey(VARIABLE))
            {
                export[VARIABLE]++;
            }
            else
            {
                export.Add(VARIABLE,1);
            }
        }
        return export;
    }

    public bool HasEnoughBarnItems(Dictionary<BarnItem, int> toCheck)
    {
        Dictionary<BarnItem, int> available = GetDataInfoDictionary();
        foreach (var VARIABLE in toCheck)
        {
            if (!available.ContainsKey(VARIABLE.Key)) return false;
            if (available[VARIABLE.Key] < VARIABLE.Value) return false;
        }

        return true;
    }
    
    // DISPLAYING OPERATIONS

    public List<BarnItem> GetDataInfoList()
    {
        return data;
    }

}