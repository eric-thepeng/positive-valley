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
    [SerializeField] private UI_PopUpAndSelect UnlockFieldPopUpSelectUI;
    [SerializeField] private UI_PopUp FieldGrowInfoPopUpUI;
    [SerializeField] private UI_PopUpAndDisappear CropHarvestUI;
    
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
            displayString += "Harvests Left: " + tarFG.GetAmountHarvestLeft()  + " / "+ tarFG.seed.cropAmount + "\n";
            displayString += "Crop Grow Time: " + tarFG.seed.cropGrowTime + "\n";
            displayString += "Harvest Remaining Time:" + "\n" + tarFG.GetCurrentCropRemainingGrowTimeString();
        }
        FieldGrowInfoPopUpUI.SetUpAndDisplay(displayString);
    }

    public void DisplayCropHarvestPopUpDisappear(SOSI_Seed seed)
    {
        GameObject newPUD = Instantiate(CropHarvestUI.gameObject, this.transform);
        newPUD.SetActive(true);
        newPUD.GetComponent<UI_PopUpAndDisappear>()
            .SetUpAndDisplay(seed.itemIcon, "Harvest 1 " + seed.itemName, 0.6f, 0.4f);
    }

    public void ExitUnlockFieldPopUp()
    {
        UnlockFieldPopUpSelectUI.ExitUI();
    }
}
