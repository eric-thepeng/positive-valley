using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldGrowth
{
    public SOSI_Seed seed;
    public int currentPhase;
    public float currentGrowthTime;
    public UnityEvent<FieldGrowth> OnPhaseChange;

    public FieldGrowth(SOSI_Seed seed, UnityAction<FieldGrowth> onPhaseChange)
    {
        this.seed = seed;
        currentPhase = 0;
        currentGrowthTime = 0;
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
        currentGrowthTime += growTime;
        while (currentGrowthTime >= seed.phaseGrowTime)
        {
            currentGrowthTime -= seed.phaseGrowTime;
            currentPhase += 1;
            OnPhaseChange.Invoke(this);
            if (currentPhase == seed.totalPhasesAmount)
            {
                return true;
            }
        }

        return false;
    }

    public Sprite GetCurrentStageSeedSprite()
    {
        return seed.phasesSprites[currentPhase];
    }

    public TimeSpan GetTotalRemainingGrowTime()
    {
        int timeInSec = (int)Math.Floor((seed.totalPhasesAmount - currentPhase + 1) * seed.phaseGrowTime - currentGrowthTime);
        return TimeSpan.FromSeconds(timeInSec);
    }

    public string GetTotalRemainingGrowTimeString()
    {
        TimeSpan timeSpan = GetTotalRemainingGrowTime();
        
        string formattedTime = string.Format("{0}{1}{2}{3}",
            timeSpan.Days > 0 ? $"{timeSpan.Days} day{(timeSpan.Days > 1 ? "s" : "")} " : "",
            timeSpan.Hours > 0 ? $"{timeSpan.Hours} hour{(timeSpan.Hours > 1 ? "s" : "")} " : "",
            timeSpan.Minutes > 0 ? $"{timeSpan.Minutes} minute{(timeSpan.Minutes > 1 ? "s" : "")} " : "",
            timeSpan.Seconds > 0 ? $"{timeSpan.Seconds} second{(timeSpan.Seconds > 1 ? "s" : "")}" : "").Trim();

        return formattedTime;
    }

    public bool CanHarvest()
    {
        return currentPhase == seed.totalPhasesAmount;
    }
}
