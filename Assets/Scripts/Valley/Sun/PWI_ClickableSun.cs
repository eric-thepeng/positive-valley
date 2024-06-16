using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PWI_ClickableSun : PlayerWorldInteractable
{
    // PRIVATE VARIABLES TO SET UP
    private int clickGain = 1;
    private Vector3 matureScale = new Vector3(1, 1, 1);
    private float growTime = 3f;
    private float aliveDuration = 5f;
    private bool goToFinalPosImmediately = false;

    // PRIVATE VARIABLES
    private bool canClick = false;

    private void Start()
    {
        StartCoroutine(StartMovingSequence());
    }

    public void SetUp(int clickGain, Vector3 matureScale, float growTime, float aliveDuration, bool goToFinalPosImmediately=false)
    {
        this.clickGain = clickGain;
        this.matureScale = matureScale;
        this.growTime = growTime;
        this.aliveDuration = aliveDuration;
        this.goToFinalPosImmediately = goToFinalPosImmediately;
    }

    IEnumerator StartMovingSequence()
    {
        // Set Up
        transform.localScale = 0.05f * matureScale;
        
        // Growing
        transform.DOScale(0.7f * matureScale, goToFinalPosImmediately ? 0 : growTime);
        yield return new WaitForSeconds(goToFinalPosImmediately ? 0 : growTime);
        
        // Finish Grow
        transform.localScale = matureScale;
        canClick = true;

        // Moving
        transform.DOLocalMoveY(transform.localPosition.y + 0.2f,goToFinalPosImmediately ? 0 : 0.5f);
        yield return new WaitForSeconds(goToFinalPosImmediately ? 0 : 0.5f);
        transform.DOLocalMove( new Vector3(transform.localPosition.x + Random.Range(-0.3f,0.3f),transform.localPosition.y - 0.6f + Random.Range(-0.2f,0.1f), transform.localPosition.z),goToFinalPosImmediately ? 0 : 0.7f);
        yield return new WaitForSeconds(goToFinalPosImmediately ? 0 : 0.7f);
        
        // WAITING TO DIE
        yield return new WaitForSeconds(aliveDuration);
        StartCoroutine(DisappearSequence());
    }

    IEnumerator DisappearSequence()
    {
        canClick = false;
        //transform.DOScale(matureScale * 0.1f, 0.2f);
        float disappearTime = 0.75f;
        transform.DOLocalMoveY(transform.localPosition.y + 0.3f, disappearTime);
        GetComponentInChildren<SpriteRenderer>().DOColor(Color.clear, disappearTime);
        GetComponentInChildren<TMP_Text>().DOColor(Color.clear, disappearTime);
        
        
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    public void TryTriggerGainSun()
    {
        if(!canClick) return;
        PlayerStat.money.ChangeValue(clickGain);
        StartCoroutine(DisappearSequence());
    }

    protected override void OnPlayerTouchEnter()
    {
        TryTriggerGainSun();
    }
}
