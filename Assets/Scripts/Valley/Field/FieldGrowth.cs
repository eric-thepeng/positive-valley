using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class FieldGrowth
{
    public SOSI_Seed seed;
    public float currentGrowthTime;
    public UnityEvent<FieldGrowth> OnPhaseChange;
    
    public int currentlyGrowingCrop;
    // 0 to seed.cropPhasesAmount-1 -> growing
    // seed.cropPhasesAmount -> can harvest
    // -1 already harvested
    public List<int> allCropsPhases;

    public bool growingFinished = false;

    public FieldGrowth(SOSI_Seed seed, UnityAction<FieldGrowth> onPhaseChange)
    {
        this.seed = seed;
        currentGrowthTime = 0;
        
        currentlyGrowingCrop = 0;
        allCropsPhases = new List<int>();
        for (int i = 0; i < seed.cropPhasesAmount; i++) { allCropsPhases.Add(0); }
        
        ReassignOnPhaseChange(onPhaseChange);
    }

    public void ReassignOnPhaseChange(UnityAction<FieldGrowth> onPhaseChange, bool triggerImmediately = true)
    {
        OnPhaseChange = new UnityEvent<FieldGrowth>();
        OnPhaseChange.AddListener(onPhaseChange);
        if(triggerImmediately) OnPhaseChange.Invoke(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="growTime"></param>
    /// <returns>True if seed is ready to harvest; False if not.</returns>
    public bool Grow(float growTime)
    {
        if (growingFinished) return false;
        
        currentGrowthTime += growTime;
        while (currentGrowthTime >= seed.cropPhaseGrowTime)
        {
            currentGrowthTime -= seed.cropPhaseGrowTime;
            allCropsPhases[currentlyGrowingCrop]++;

            if (allCropsPhases[currentlyGrowingCrop] == seed.cropPhasesAmount)
            {
                currentlyGrowingCrop++;
            }

            if (currentlyGrowingCrop == seed.cropAmount)
            {
                growingFinished = true;
            }
            
            OnPhaseChange.Invoke(this);
        }
        return false;
    }

    public Sprite GetCropSprite(int cropIndex)
    {
        if (cropIndex >= allCropsPhases.Count)
        {
            Debug.LogError("Getting a crop spirte with index larger than crop amount");
            return null;
        }
        return seed.phasesSprites[allCropsPhases[cropIndex]];
    }

    public TimeSpan GetCurrentCropRemainingGrowTime()
    {
        int timeInSec = (int)Math.Floor(
            (seed.cropPhasesAmount - allCropsPhases[currentlyGrowingCrop]) * seed.cropPhaseGrowTime - currentGrowthTime
            );
        return TimeSpan.FromSeconds(timeInSec);
    }

    public string GetCurrentCropRemainingGrowTimeString()
    {
        TimeSpan timeSpan = GetCurrentCropRemainingGrowTime();
        
        string formattedTime = string.Format("{0}{1}{2}{3}",
            timeSpan.Days > 0 ? $"{timeSpan.Days} day{(timeSpan.Days > 1 ? "s" : "")} " : "",
            timeSpan.Hours > 0 ? $"{timeSpan.Hours} hour{(timeSpan.Hours > 1 ? "s" : "")} " : "",
            timeSpan.Minutes > 0 ? $"{timeSpan.Minutes} min{(timeSpan.Minutes > 1 ? "s" : "")} " : "",
            timeSpan.Seconds > 0 ? $"{timeSpan.Seconds} sec{(timeSpan.Seconds > 1 ? "s" : "")}" : "").Trim();
        return formattedTime;
    }

    public void TryToHarvest()
    {
        List<int> harvestIndex = new List<int>();
        for (int i = 0; i < allCropsPhases.Count; i++)
        {
            if (allCropsPhases[i] == seed.cropPhasesAmount)
            {
                harvestIndex.Add(i);
                allCropsPhases[i] = -1;
                // gain a crop
            }
        }
        OnPhaseChange.Invoke(this);
    }
}
