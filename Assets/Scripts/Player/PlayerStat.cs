using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static BroadcastStatInt money = new BroadcastStatInt(100);
    public static BroadcastStatInt level = new BroadcastStatInt(1);
    public static BroadcastStatInt experience = new BroadcastStatInt(0);

    private void Awake()
    {
        experience.SubscribeChangeValue(OnExperienceChangeValue);
    }

    public void OnExperienceChangeValue(int newExp)
    {
        if (newExp >= GetLevelUpExpRequirement()) LevelUp();
    }

    public void LevelUp()
    {
        experience.ChangeValue(- GetLevelUpExpRequirement());
        level.ChangeValue(1);
    }

    public static int GetLevelUpExpRequirement()
    {
        return level.GetValue() * 20;
    }
}