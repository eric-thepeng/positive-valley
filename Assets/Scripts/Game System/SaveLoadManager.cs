using System;
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

    public void SaveExitTime()
    {
        ES3.Save("LastExitTime", System.DateTime.Now.ToString(),saveFileName);
        PlayerPrefs.Save();
    }

    private void OnApplicationQuit()
    {
        SaveExitTime();
    }
    
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // Enter Pause
            SaveExitTime();
        }
        else
        {
            // Resume
        }
    }

    public TimeSpan GetLastExitTimeAway()
    {
        if (ES3.KeyExists("LastExitTime",loadFileName))
        {
            string lastExitTimeString = ES3.Load<string>("LastExitTime",loadFileName);
            System.DateTime lastExitTime = System.DateTime.Parse(lastExitTimeString);
            System.TimeSpan timeAway = System.DateTime.Now - lastExitTime;
            return timeAway;
        }
        
        return TimeSpan.Zero;
    }
}
