using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using TMPro;
using UnityEngine.Events;

/*
[Serializable]public class ScreenAction
{
    public GameObject targetGO;
    public enum SAAction {Move, Scale}

    public SAAction saAction = SAAction.Move;
    public Vector3 targetVector = new Vector3(0, 0, 0);

    public float enterTime = 0.6f;
    public float exitTime = 0.3f;
    
    
    private Vector3 orgVector = new Vector3(0, 0, 0);
    
    public void Enter()
    {
        if (saAction == SAAction.Move)
        {
            orgVector = targetGO.transform.localPosition;
        }
        else if (saAction == SAAction.Scale)
        {
            orgVector = targetGO.transform.localScale;
        }
        DoAction(enterTime);
    }

    public void Exit()
    {
        ReverseAction(exitTime);
    }

    private void DoAction(float actionTime)
    {
        if (saAction == SAAction.Move)
        {
            targetGO.transform.DOMove(targetVector, actionTime);
        }
        else if (saAction == SAAction.Scale)
        {
            targetGO.transform.DOScale(targetVector, actionTime);
        }
    }

    private void ReverseAction(float actionTime)
    {
        if (saAction == SAAction.Move)
        {
            targetGO.transform.DOMove(orgVector, actionTime);
        }
        else if (saAction == SAAction.Scale)
        {
            targetGO.transform.DOScale(orgVector, actionTime);
        }
    }
}
*/

public class TomatoClockDisplayer : MonoBehaviour
{
    // SINGLETON
    static TomatoClockDisplayer instance;
    public static TomatoClockDisplayer i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<TomatoClockDisplayer>();
            }
            return instance;
        }
    }

    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text centerTextTMPT;

    public void EnterSetting()
    {
        panel.SetActive(true);
        PopUpUIManager.i.DisplayUniversalPopUpSelect(
            "Start a 25 minutes tomato clock?",
            new List<UnityAction>(){PT_TomatoClock.i.StartClock, Exit},
                        new List<string>(){"Yes","No"});
    }

    public void Exit()
    {
        PopUpUIManager.i.ExitUniversalPopUp();
        panel.SetActive(false);
    }

    public void UpdateCenterText(string newText)
    {
        centerTextTMPT.text = newText;
    }
    
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Exit();
        }
    }*/
}
