using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UI_PopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text descriptionTMPT;
    [SerializeField] private CanvasGroup canvasGroup;

    private UnityAction actionAtExit = null; 
    
    public void SetUpAndDisplay(string descriptionString, UnityAction actionAtExit = null)
    {
        gameObject.SetActive(true);
        descriptionTMPT.text = descriptionString;
        this.actionAtExit = actionAtExit;
    }
    
    public void ExitUI()
    {
        gameObject.SetActive(false);
        actionAtExit?.Invoke();
    }

}
