using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopUpUIManager : MonoBehaviour
{
    // SINGLETON
    static PopUpUIManager instance;
    public static PopUpUIManager i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PopUpUIManager>();
            }
            return instance;
        }
    }
    // ----------

    // SERIALIZED PRIVATE VARIABLES
    [SerializeField]private UI_PopUpAndSelect UnlockFieldPopUpSelectUI;
    [SerializeField] private UI_PopUp FieldGrowInfoPopUpUI;

    public void DisplayUnlockFieldPopUpSelect(PWI_Field tarField)
    {
        UnlockFieldPopUpSelectUI.SetUpAndDisplay(
            "It takes $$" + tarField.GetUnlockCost() + " to unlock.", 
            new List<UnityAction>(){tarField.TryToUnlock}, 
            new List<string>(){"Unlock"});
    }

    public void DisplayFieldGrowInfoPopUp(PWI_Field tarField)
    {
        string displayString = "";
        if (tarField == null)
        {
            displayString = "No Information On Current Field's Crop";
        }
        else
        {
            FieldGrowth tarFG = tarField.GetFieldGrowth();
            displayString += "Seed: " + tarFG.seed.itemName + "\n";
            displayString += "Growing Phase: " + (tarFG.currentPhase + 1) +"/" + (tarFG.seed.totalPhasesAmount+1) + "\n";
            displayString += "Total Remaining Time: " + tarFG.GetTotalRemainingGrowTimeString();
        }
        FieldGrowInfoPopUpUI.SetUpAndDisplay(displayString);
    }

    public void ExitUnlockFieldPopUp()
    {
        UnlockFieldPopUpSelectUI.ExitUI();
    }
}
