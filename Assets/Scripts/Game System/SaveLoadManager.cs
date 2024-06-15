using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    // SINGLETON
    static SaveLoadManager instance;
    public static SaveLoadManager i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<SaveLoadManager>();
            }
            return instance;
        }
    }
    // ----------
    public bool firstNewThenLoad = false;
    
    public enum LoadMode{NewGame, LoadGame}
    [SerializeField] private LoadMode loadMode = LoadMode.NewGame;
    public string loadFileName = "";

    public LoadMode GetLoadMode()
    {
        if (firstNewThenLoad)
        {
            if (ES3.FileExists(loadFileName)) return LoadMode.LoadGame;
            else return LoadMode.NewGame;
        }

        return loadMode;
    }

    public enum SaveMode{DoNotSave, SaveGame}
    [SerializeField] private SaveMode saveMode = SaveMode.DoNotSave;
    public string saveFileName = "";

    public SaveMode GetSaveMode()
    {
        return saveMode;
    }
}