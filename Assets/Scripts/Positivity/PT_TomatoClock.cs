using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;


public class PT_TomatoClock : MonoBehaviour
{
    // SINGLETON
    static PT_TomatoClock instance;
    public static PT_TomatoClock i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PT_TomatoClock>();
            }
            return instance;
        }
    }

    private bool clockInProgress = false;
    private bool finishClockWaiting = false;
    
    public void EnterTomatoClockUI()
    {
        TomatoClockDisplayer.i.EnterSetting();
    }
    
    public void StartClock()
    {
        PopUpUIManager.i.ExitUniversalPopUp();
        clockInProgress = true;
        StartCoroutine(CountDownClock());
    }

    public void StartFinishClockWaiting()
    {
        finishClockWaiting = true;
        PopUpUIManager.i.ExitUniversalPopUp();
    }

    public void ExitFinishClockWaiting()
    {
        GetFinishClockReward();
    }

    public void GetFinishClockReward()
    {
        PlayerStat.money.ChangeValue(100);
        PopUpUIManager.i.DisplayUniversalTextPopUp("You earned 100 Sun!",ExitClock);
    }

    public void ExitClock()
    {
        if(!clockInProgress) return;
        TomatoClockDisplayer.i.Exit();
        clockInProgress = false;
        finishClockWaiting = false;
        StopAllCoroutines();
    }

    public void ExitButtonClick()
    {
        if(!clockInProgress) return;
        if (finishClockWaiting)
        {
            ExitFinishClockWaiting();
        }
        else
        {
            // Double Check To End Clock
            PopUpUIManager.i.DisplayUniversalPopUpSelect(
                "Exit Tomato Clock? \n You will lose all the rewards.",
                new List<UnityAction>(){ExitClock, PopUpUIManager.i.ExitUniversalPopUp},
                new List<string>(){"Yes","No"});
        }
    }

    IEnumerator CountDownClock()
    {
        int totalSeconds = 25 * 60;
        
        while (totalSeconds > 0)
        {
            int displayMinutes = totalSeconds / 60;
            int displaySeconds = totalSeconds % 60;

            TomatoClockDisplayer.i.UpdateCenterText(string.Format("{0:00}:{1:00}", displayMinutes, displaySeconds));

            yield return new WaitForSeconds(1f);

            totalSeconds--;
        }
        
        TomatoClockDisplayer.i.UpdateCenterText("Congrats! \n You completed a Focus Clock.");
        StartFinishClockWaiting();
        
    }
    
    // APPLICATION ACTIONS

    private void OnApplicationQuit()
    {
        ExitClock();
    }

    
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // Enter Pause
            ExitClock();
        }
        else
        {
            // Resume
        }
    }

}
