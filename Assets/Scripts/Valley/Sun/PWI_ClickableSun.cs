using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class PWI_ClickableSun : PlayerWorldInteractable
{
    // PRIVATE VARIABLES TO SET UP
    private int clickGain = 1;
    private Vector3 matureScale = new Vector3(1, 1, 1);
    private float growTime = 3f;
    private float dieOutTime = 5f;
    
    // PRIVATE VARIABLES
    private bool canClick = false;


    private void Start()
    {
        StartCoroutine(StartMovingSequence());
    }

    public void SetUp(int clickGain, Vector3 matureScale, float growTime, float dieOutTime)
    {
        this.clickGain = clickGain;
        this.matureScale = matureScale;
        this.growTime = growTime;
        this.dieOutTime = dieOutTime;
    }

    IEnumerator StartMovingSequence()
    {
        // Set Up
        transform.localScale = 0.1f * matureScale;
        
        // Growing
        transform.DOScale(matureScale, growTime);
        yield return new WaitForSeconds(growTime);
        canClick = true;
        
        // Moving
        transform.DOLocalMoveY(transform.localPosition.y + 0.2f,0.5f);
        yield return new WaitForSeconds(0.5f);
        transform.DOLocalMove( new Vector3(transform.localPosition.x + Random.Range(-0.3f,0.3f),transform.localPosition.y - 0.6f + Random.Range(-0.2f,0.1f), transform.localPosition.z),0.7f);
        yield return new WaitForSeconds(0.7f);
        
        // WAITING TO DIE
        yield return new WaitForSeconds(dieOutTime);
        StartCoroutine(DisappearSequence());
    }

    IEnumerator DisappearSequence()
    {
        canClick = false;
        transform.DOScale(matureScale * 0.1f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    protected override void OnPlayerTouch()
    {
        if(!canClick) return;
        PlayerStat.money.ChangeValue(clickGain);
        StartCoroutine(DisappearSequence());
    }
}