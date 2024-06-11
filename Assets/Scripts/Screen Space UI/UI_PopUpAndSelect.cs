using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_PopUpAndSelect : MonoBehaviour
{
    [SerializeField] private TMP_Text descriptionTMPT;
    [SerializeField] private List<Button> selectionButtons;

    public void SetUpAndDisplay(string descriptionString, List<UnityAction> selectionActions, List<String> selectionButtonsText)
    {
        gameObject.SetActive(true);
        descriptionTMPT.text = descriptionString;
        for (int i = 0; i < selectionButtons.Count; i++)
        {
            selectionButtons[i].onClick.RemoveAllListeners();
            selectionButtons[i].onClick.AddListener(selectionActions[i]);
            selectionButtons[i].GetComponentInChildren<TMP_Text>().text = selectionButtonsText[i];
        }
    }

    public void ExitUI()
    {
        gameObject.SetActive(false);
    }
}
