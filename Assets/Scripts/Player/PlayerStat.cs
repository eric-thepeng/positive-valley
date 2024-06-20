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

    private void Start()
    {
        LoadGameFile();
    }

    private void OnApplicationQuit()
    {
        SaveGameFile();
    }
    
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // Enter Pause
            SaveGameFile();
        }
        else
        {
            // Resume
            LoadGameFile();
        }
    }

    private void SaveGameFile()
    {
        if(SaveLoadManager.i.GetSaveMode() == SaveLoadManager.SaveMode.DoNotSave) return;
        ES3.Save("playerStatMoney", money.GetValue(), SaveLoadManager.i.saveFileName);
        ES3.Save("playerStatLevel", level.GetValue(), SaveLoadManager.i.saveFileName);
        ES3.Save("playerStatExperience", experience.GetValue(), SaveLoadManager.i.saveFileName);
    }
    
    private void LoadGameFile()
    {
        if(SaveLoadManager.i.GetLoadMode() == SaveLoadManager.LoadMode.NewGame) return;
        money.SetValue(ES3.Load<int>("playerStatMoney", SaveLoadManager.i.loadFileName));
        level.SetValue(ES3.Load<int>("playerStatLevel", SaveLoadManager.i.loadFileName));
        experience.SetValue(ES3.Load<int>("playerStatExperience", SaveLoadManager.i.loadFileName));
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
        return (int)(Mathf.Pow(level.GetValue(),1.3f) * 100);
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