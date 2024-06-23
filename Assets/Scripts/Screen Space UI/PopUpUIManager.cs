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
    
    // PRIVATE VARIABLES
    private Queue<KeyValuePair<SOSI_Seed, SO_Rarity>> popUpDisappearInfoQue;

    private void Start()
    {
        popUpDisappearInfoQue = new Queue<KeyValuePair<SOSI_Seed, SO_Rarity>>();
        StartCoroutine(DisplayPopUpDisappearQue());
    }

    public void DisplayUnlockFieldPopUpSelect(PWI_Field tarField)
    {
        UnlockFieldPopUpSelectUI.SetUpAndDisplay(
            "It takes $$" + tarField.GetUnlockCost() + " to unlock.", 
            new List<UnityAction>(){tarField.TryToUnlock}, 
            new List<string>(){"Unlock"});
    }

    public void DisplaySellBarnItemPopUpSelect(UI_BarnItemDisplayer barnItemDisplayer)
    {
        int sellPrice = barnItemDisplayer.displayingBarnItem.GetSellPrice();
        UnlockFieldPopUpSelectUI.SetUpAndDisplay(
            "Sell [" + barnItemDisplayer.displayingBarnItem.itemRarity.rarityName +"] " + barnItemDisplayer.displayingBarnItem.itemSeed.itemName + " for " + sellPrice + " ?",
            new List<UnityAction>(){barnItemDisplayer.SellItem}, 
            new List<string>(){"Sell"});
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

    private void DisplayCropHarvestPopUpDisappear(SOSI_Seed seed, SO_Rarity rarity)
    {
        GameObject newPUD = Instantiate(CropHarvestUI.gameObject, this.transform);
        newPUD.SetActive(true);
        UI_PopUpAndDisappear puad = newPUD.GetComponent<UI_PopUpAndDisappear>();
        //"[Legendary] Eggplant +1"
        string descriptionString = "[" + rarity.rarityName +"] " + seed.itemName + " +1";
        puad.SetUpAndDisplay(seed.itemIcon, descriptionString, 0.4f, 0.4f);
        puad.SetBackgroundFrameColor(rarity.color);
    }

    public void DisplaySimpleTextPopUpDisappear(string text)
    {
        GameObject newPUD = Instantiate(CropHarvestUI.gameObject, this.transform);
        newPUD.SetActive(true);
        UI_PopUpAndDisappear puad = newPUD.GetComponent<UI_PopUpAndDisappear>();
        puad.SetUpAndDisplay(null, text, 0.4f, 0.4f);
    }

    public void QueDisplayCropHarvestPopUpDisappear(BarnPanelManager.BarnItem barnItem)
    {
        popUpDisappearInfoQue.Enqueue(new KeyValuePair<SOSI_Seed, SO_Rarity>(barnItem.itemSeed, barnItem.itemRarity));
    }

    public void ExitUnlockFieldPopUp()
    {
        UnlockFieldPopUpSelectUI.ExitUI();
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
