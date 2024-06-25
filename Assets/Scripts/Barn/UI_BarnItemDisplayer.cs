using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_BarnItemDisplayer : MonoBehaviour
{
    // Dependencies
    public Image iconImage;
    public Image rarityFrameImage;
    public TMP_Text displayPriceTMPT;
    
    // Variables
    public int blockID;
    public BarnPanelManager.BarnItem displayingBarnItem;
    public UI_SelectableButton selectableButton;
    
    public void SetUp(int blockID)
    {
        this.blockID = blockID;
        
        iconImage.gameObject.SetActive(false);
        rarityFrameImage.gameObject.SetActive(false);
        displayPriceTMPT.gameObject.SetActive(false);
        
        selectableButton.SetInteractable(false);
    }

    public void SetUp(int blockID, BarnPanelManager.BarnItem barnItem)
    {
        this.blockID = blockID;
        displayingBarnItem = barnItem;
        
        iconImage.sprite = barnItem.itemSeed.itemIcon;
        rarityFrameImage.color = barnItem.itemRarity.color;
        displayPriceTMPT.text = "$$ " + barnItem.GetSellPrice();
        
        iconImage.gameObject.SetActive(true);
        rarityFrameImage.gameObject.SetActive(true);
        displayPriceTMPT.gameObject.SetActive(true);
        
        selectableButton.OnSelected.AddListener(OnSelect);
        selectableButton.OnUnselected.AddListener(OnUnselect);

    }

    public void OnSelect(UI_SelectableButton sb)
    {
        BarnSellingManager.i.AddBID(this);
    }

    public void OnUnselect(UI_SelectableButton sb)
    {
        BarnSellingManager.i.RemoveBID(this);
    }
}
