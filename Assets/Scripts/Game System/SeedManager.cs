using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedManager : MonoBehaviour
{
    // SINGLETON
    static SeedManager instance;
    public static SeedManager i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<SeedManager>();
            }
            return instance;
        }
    }
    
    [SerializeField] private List<SOSI_Seed> allSeeds;
    
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
        foreach (var seed in allSeeds)
        {
            ES3.Save("SOSI_Seed_" + seed.name + "_unlockState",seed.unlockState,SaveLoadManager.i.saveFileName);
        }
        //ES3.Save("BarnPanelManager_barnItemSetStorage", barnItemSetStorage, SaveLoadManager.i.saveFileName);
    }
    
    private void LoadGameFile()
    {
        if(SaveLoadManager.i.GetLoadMode() == SaveLoadManager.LoadMode.NewGame) return;
        foreach (var seed in allSeeds)
        {
            ES3.Save("SOSI_Seed_" + seed.name + "_unlockState",seed.unlockState,SaveLoadManager.i.saveFileName);
            seed.unlockState = ES3.Load<SOSI_Seed.UnlockState>("SOSI_Seed_" + seed.name + "_unlockState",
                SaveLoadManager.i.loadFileName);
        }
        //barnItemSetStorage = ES3.Load<BarnItemSet>("BarnPanelManager_barnItemSetStorage", SaveLoadManager.i.loadFileName);
    }

}
