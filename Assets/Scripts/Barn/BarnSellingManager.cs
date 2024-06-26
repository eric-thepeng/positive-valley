using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarnSellingManager : MonoBehaviour
{
    static BarnSellingManager instance;
    public static BarnSellingManager i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<BarnSellingManager>();
            }
            return instance;
        }
    }

    [SerializeField] private TMP_Text descriptionTMPT;
    [SerializeField] private Button sellingButton;

    private List<UI_BarnItemDisplayer> selectedBID = new List<UI_BarnItemDisplayer>();

    private void OnEnable()
    {
        GamePanelsManager.i.OnNewPanelEnters.AddListener(OnBarnEnters);
    }

    public void OnBarnEnters(GamePanelsManager.GamePanel panel)
    {
        if(panel != GamePanelsManager.GamePanel.Barn) return;
        selectedBID = new List<UI_BarnItemDisplayer>();
        sellingButton.onClick.RemoveAllListeners();
        RefreshDisplay();
    }
    
    public void AddBID(UI_BarnItemDisplayer bid)
    {
        selectedBID.Add(bid);
        RefreshDisplay();
    }

    public void RemoveBID(UI_BarnItemDisplayer bid)
    {
        selectedBID.Remove(bid);
        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        if (selectedBID.Count > 0)
        {
            descriptionTMPT.text = "Sell " + selectedBID.Count + " items for " + GetCurrentSellPrice() + " ?";
            sellingButton.gameObject.SetActive(true);
            sellingButton.onClick.AddListener(SellAllSelected);
        }
        else
        {
            descriptionTMPT.text = "Select items to sell.";
            sellingButton.gameObject.SetActive(false);
        }
    }

    public int GetCurrentSellPrice()
    {
        if (selectedBID.Count == 0) return 0;
        int price = 0;

        foreach (var VARIABLE in selectedBID)
        {
            price += VARIABLE.displayingBarnItem.GetSellPrice();
        }

        return price;
    }

    public void SellAllSelected()
    {
        int price = GetCurrentSellPrice();
        PlayerStat.money.ChangeValue(price);
        PopUpUIManager.i.DisplaySimpleTextPopUpDisappear("Get " +  price + " Sun");
        BarnPanelManager.i.SellBarnItems(selectedBID);
        selectedBID = new List<UI_BarnItemDisplayer>();
    }
}
