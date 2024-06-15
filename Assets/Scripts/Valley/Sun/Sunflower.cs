using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sunflower : PlayerWorldInteractable
{
    //[SerializeField] private float clickableSunSpawnInterval;
    [SerializeField] private float clickableSunGrowTime;
    [SerializeField] private float clickableSunAliveDuration;
    [SerializeField] private Transform clickableSunSpawnTF;
    [SerializeField] private GameObject clickableSunGameObject;
    private void Start()
    {
        StartCoroutine(SpawnClickableSun()); 
        LoadGameFile();
    }
    
    private void OnApplicationQuit()
    {
        SaveGameFile();
    }

    private void SaveGameFile()
    {
        if(SaveLoadManager.i.GetSaveMode() == SaveLoadManager.SaveMode.DoNotSave) return;
    }
    
    private void LoadGameFile()
    {
        if(SaveLoadManager.i.GetLoadMode() == SaveLoadManager.LoadMode.NewGame) return;
        TimeSpan timeAway = SaveLoadManager.i.GetLastExitTimeAway();
        print("Time Away " + timeAway.TotalSeconds);
        if (timeAway != TimeSpan.Zero)
        {
            for (int i = 0; 
                 i < Math.Min((int)(clickableSunAliveDuration / clickableSunGrowTime + 1), timeAway.TotalSeconds / (clickableSunGrowTime)); 
                 i++)
            {
                Transform newCS = Instantiate(clickableSunGameObject,clickableSunSpawnTF).transform;
                newCS.transform.localPosition = new Vector3(0, 0, 0);
                newCS.GetComponent<PWI_ClickableSun>().SetUp(1,new Vector3(0.6f,0.6f,1),clickableSunGrowTime,clickableSunAliveDuration, true);
            }
            
        }
    }

    IEnumerator SpawnClickableSun()
    {
        yield return new WaitForSeconds(Random.Range(0,2));
        while (true)
        {
            Transform newCS = Instantiate(clickableSunGameObject,clickableSunSpawnTF).transform;
            newCS.transform.localPosition = new Vector3(0, 0, 0);
            newCS.GetComponent<PWI_ClickableSun>().SetUp(1,new Vector3(0.6f,0.6f,1),clickableSunGrowTime,clickableSunAliveDuration);
            yield return new WaitForSeconds(clickableSunGrowTime + 3);
        }
    }
    
    
}
