using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Sunflower : PlayerWorldInteractable
{
    [SerializeField] private float clickableSunSpawnInterval;
    //[SerializeField] private float clickableSunGrowDuration;
    //[SerializeField] private float clickableSunAliveDuration;
    [SerializeField] private Transform clickableSunSpawnTF;
    [SerializeField] private GameObject clickableSunGameObject;
    private void Start()
    {
        StartCoroutine(SpawnClickableSun());
    }

    IEnumerator SpawnClickableSun()
    {
        yield return new WaitForSeconds(Random.Range(0,clickableSunSpawnInterval/2));
        while (true)
        {
            Transform newCS = Instantiate(clickableSunGameObject,clickableSunSpawnTF).transform;
            newCS.transform.localPosition = new Vector3(0, 0, 0);
            newCS.GetComponent<PWI_ClickableSun>().SetUp(1,new Vector3(0.6f,0.6f,1),15,60);
            yield return new WaitForSeconds(clickableSunSpawnInterval + Random.Range(0,0.5f));
        }
    }
}
