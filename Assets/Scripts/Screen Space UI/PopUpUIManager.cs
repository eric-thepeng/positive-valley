using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    // SERIALIZED PRIVATE VARIABLES
    [SerializeField] private UI_PopUpAndSelect UnlockFieldPopUpSelectUI;
    [SerializeField] private UI_PopUpAndSelect DrinkWaterPopUpSelectUI;
    [SerializeField] private UI_PopUpAndSelect UniversalPopUpSelectUI;
    [SerializeField] private UI_PopUp FieldGrowInfoPopUpUI;
    [SerializeField] private UI_PopUpAndDisappear commonPopUpAndDisappearUI;
    [SerializeField] private UI_PopUpCropHarvest cropHarvestUI;
    
    // PRIVATE VARIABLES
    private Queue<KeyValuePair<SOSI_Seed, SO_Rarity>> popUpDisappearInfoQue;

    private void Start()
    {
        popUpDisappearInfoQue = new Queue<KeyValuePair<SOSI_Seed, SO_Rarity>>();
        StartCoroutine(DisplayPopUpDisappearQue());
    }

    public UnityAction DisplayUnlockFieldPopUpSelect(PWI_Field tarField)
    {
        return UnlockFieldPopUpSelectUI.SetUpAndDisplay(
            "It takes $$" + tarField.GetUnlockCost() + " to unlock.", 
            new List<UnityAction>(){tarField.TryToUnlock}, 
            new List<string>(){"Unlock"});
    }

    public UnityAction DisplayUnlockFieldPopUpSelect(string descriptionString, UnityAction selectionActions,
        String selectionButtonsText)
    {
        return UnlockFieldPopUpSelectUI.SetUpAndDisplay(
            descriptionString, 
            new List<UnityAction>() {selectionActions }, 
            new List<string>(){selectionButtonsText});
    }
    
    public UnityAction DisplayDrinkWaterPopUpSelect(string descriptionString, List<UnityAction> selectionActions,
        List<String> selectionButtonsText)
    {
        return DrinkWaterPopUpSelectUI.SetUpAndDisplay(
            descriptionString, 
            selectionActions, 
            selectionButtonsText);
    }
    
    public UnityAction DisplayUniversalPopUpSelect(string descriptionString, List<UnityAction> selectionActions,
        List<String> selectionButtonsText)
    {
        return UniversalPopUpSelectUI.SetUpAndDisplay(
            descriptionString, 
            selectionActions, 
            selectionButtonsText);
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

    public void DisplayUniversalTextPopUp(string text, UnityAction actionAtExit = null)
    {
        FieldGrowInfoPopUpUI.SetUpAndDisplay(text, actionAtExit);
    }

    private void DisplayCropHarvestPopUpDisappear(SOSI_Seed seed, SO_Rarity rarity)
    {
        GameObject newPUD = Instantiate(commonPopUpAndDisappearUI.gameObject, this.transform);
        newPUD.SetActive(true);
        UI_PopUpAndDisappear puad = newPUD.GetComponent<UI_PopUpAndDisappear>();
        //"[Legendary] Eggplant +1"
        string descriptionString = "[" + rarity.rarityName +"] " + seed.itemName + " +1";
        puad.SetUpAndDisplay(seed.itemIcon, descriptionString, 0.4f, 0.4f);
        puad.SetBackgroundFrameColor(rarity.color);
    }

    public void DisplaySimpleTextPopUpDisappear(string text, float stayTime = 0.4f, float disappearTime = 0.4f, bool punchScale = false, bool move = true)
    {
        GameObject newPUD = Instantiate(commonPopUpAndDisappearUI.gameObject, this.transform);
        newPUD.SetActive(true);
        UI_PopUpAndDisappear puad = newPUD.GetComponent<UI_PopUpAndDisappear>();
        puad.SetUpAndDisplay(null, text, 0.4f, 0.4f, punchScale, move);
    }
    
    public void QueDisplayCropHarvestPopUpDisappear(BarnItem barnItem)
    {
        popUpDisappearInfoQue.Enqueue(new KeyValuePair<SOSI_Seed, SO_Rarity>(barnItem.itemSeed, barnItem.itemRarity));
    }

    public void DisplayCropHarvestMultipleAndDisappear(List<BarnItem> barnItems)
    {
        cropHarvestUI.SetUpAndDisplay(barnItems);
    }

    public void ExitUnlockFieldPopUp()
    {
        UnlockFieldPopUpSelectUI.ExitUI();
    }

    public void ExitDrinkWaterPopUp()
    {
        DrinkWaterPopUpSelectUI.ExitUI();
    }
    
    public void ExitUniversalPopUp()
    {
        UniversalPopUpSelectUI.ExitUI();
    }

    IEnumerator DisplayPopUpDisappearQue()
    {
        while (true)
        {
            if (popUpDisappearInfoQue.Count != 0)
            {
                KeyValuePair<SOSI_Seed, SO_Rarity> pair = popUpDisappearInfoQue.Dequeue();
                DisplayCropHarvestPopUpDisappear(pair.Key, pair.Value);
                yield return new WaitForSeconds(.5f);
            }
            else
            {
                yield return new WaitForSeconds(0);
            }
        }
    }
}
