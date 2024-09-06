using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

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
    
    public void EnterTomatoClockUI()
    {
        TomatoClockDisplayer.i.EnterSetting();
    }
    
    public void ExitTomatoClockUI()
    {
        
    }

    public void StartClock()
    {
        PopUpUIManager.i.ExitUniversalPopUp();
        clockInProgress = true;
        StartCoroutine(CountDownClock());
    }

    public void FinishClock()
    {
        clockInProgress = false;
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
        
        FinishClock();
    }

}
