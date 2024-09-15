using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_BarnItemDisplayer : MonoBehaviour
{
    // Dependencies
    [Header("[ Dependencies ]")]
    [SerializeField] Image iconImage;
    [SerializeField] Image rarityFrameImage;
    [SerializeField] TMP_Text displayPriceTMPT;
    [SerializeField] Image countBackgroundImage;
    [SerializeField] TMP_Text countAmountTMPT;
    
    // Variables
    [Header("[ Public Variables ]")]
    public int blockID;
    public BarnItem displayingBarnItem;
    public UI_SelectableButton selectableButton;
    
    // Settings
    [Serializable]public class DisplaySetting
    {
        public enum SellPrice{Display, Hide}
        public SellPrice sellPrice;
        
        public enum StackAmount{Display, Hide}
        public StackAmount stackAmount;
        
        public DisplaySetting()
        {
            sellPrice = SellPrice.Display;
            stackAmount = StackAmount.Display;
        }

        public DisplaySetting(SellPrice sellPrice, StackAmount stackAmount)
        {
            this.sellPrice = sellPrice;
            this.stackAmount = stackAmount;
        }
    }
    
    public void SetUpAsEmpty(int blockID)
    {
        this.blockID = blockID;
        
        iconImage.gameObject.SetActive(false);
        rarityFrameImage.gameObject.SetActive(false);
        displayPriceTMPT.gameObject.SetActive(false);
        
        countBackgroundImage.gameObject.SetActive(false);
        countAmountTMPT.gameObject.SetActive(false);
        
        selectableButton.SetInteractable(false);
    }

    public void SetUp(int blockID, BarnItem barnItem, DisplaySetting displaySetting = null)
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

        // Config according to display setting
        switch (displaySetting.sellPrice)
        {
            case DisplaySetting.SellPrice.Display:
                displayPriceTMPT.gameObject.SetActive(true);
                break;
            case DisplaySetting.SellPrice.Hide:
                displayPriceTMPT.gameObject.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        switch (displaySetting.stackAmount)
        {
            case DisplaySetting.StackAmount.Display:
                countBackgroundImage.gameObject.SetActive(true);
                countAmountTMPT.gameObject.SetActive(true);
                break;
            case DisplaySetting.StackAmount.Hide:
                countBackgroundImage.gameObject.SetActive(false);
                countAmountTMPT.gameObject.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

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
