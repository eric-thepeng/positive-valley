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
        if (currentGrowthTime >= seed.phaseGrowTime)
        {
            currentGrowthTime = 0;
            currentPhase += 1;
            OnPhaseChange.Invoke(this);
            if (currentPhase == seed.totalPhasesAmount) return true;
            return false;
        }

        return false;
    }

    public Sprite GetCurrentStageSeedSprite()
    {
        return seed.phasesSprites[currentPhase];
    }
}
