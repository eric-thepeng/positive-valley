using System.Collections;
using System.Collections.Generic;
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
    
    public enum LoadMode{NewGame, LoadGame}
    public LoadMode loadMode = LoadMode.NewGame;
    public string loadFileName = "";
    
    public enum SaveMode{DoNotSave, SaveGame}
    public SaveMode saveMode = SaveMode.DoNotSave;
    public string saveFileName = "";

}
