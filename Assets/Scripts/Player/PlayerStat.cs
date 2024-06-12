using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public static BroadcastStatInt money = new BroadcastStatInt(0);
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
        return level.GetValue() * 50;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            money.ChangeValue(10);
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            money.ChangeValue(100);
        }else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            experience.ChangeValue(10);
        }else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            experience.ChangeValue(100);
        }
    }
}