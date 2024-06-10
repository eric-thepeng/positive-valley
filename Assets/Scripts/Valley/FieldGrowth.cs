using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldGrowth
{
    public SOSI_Seed seed;
    public int currentPhase;
    public float currentGrowthTime;
    private UnityEvent<Sprite> OnPhaseChange;

    public FieldGrowth(SOSI_Seed seed, UnityAction<Sprite> PhaseChangeSprite)
    {
        this.seed = seed;
        currentPhase = 0;
        currentGrowthTime = 0;
        OnPhaseChange = new UnityEvent<Sprite>();
        OnPhaseChange.AddListener(PhaseChangeSprite);
        OnPhaseChange.Invoke(seed.phasesSprites[currentPhase]);
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
            OnPhaseChange.Invoke(seed.phasesSprites[currentPhase]);
            if (currentPhase == seed.totalPhasesAmount) return true;
            return false;
        }

        return false;
    }
}
