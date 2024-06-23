using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_BarnItemDisplayer : MonoBehaviour
{
    public Image iconImage;
    public Image rarityFrameImage;
    public int blockID;
    public BarnPanelManager.BarnItem displayingBarnItem;

    private Toggle myToggle;

    public void SetUp(int blockID)
    {
        this.blockID = blockID;
        myToggle = GetComponent<Toggle>();
        myToggle.interactable = false;
    }

    public void SetUp(int blockID, BarnPanelManager.BarnItem barnItem)
    {
        this.blockID = blockID;
        myToggle = GetComponent<Toggle>();
        myToggle.interactable = true;

        myToggle.onValueChanged.AddListener(OnToggleValueChange);
        displayingBarnItem = barnItem;
        
        iconImage.sprite = barnItem.itemSeed.itemIcon;
        rarityFrameImage.color = barnItem.itemRarity.color;
        
        iconImage.gameObject.SetActive(true);
        rarityFrameImage.gameObject.SetActive(true);
    }

    public void OnToggleValueChange(bool Select)
    {
        PopUpUIManager.i.DisplaySellBarnItemPopUpSelect(this);
    }
    

    public void SellItem()
    {
        PlayerStat.money.ChangeValue(displayingBarnItem.GetSellPrice());
        PopUpUIManager.i.ExitUnlockFieldPopUp();
        PopUpUIManager.i.DisplaySimpleTextPopUpDisappear("Get " +  displayingBarnItem.GetSellPrice() + " Sun");
        BarnPanelManager.i.SellBarnItem(blockID);
    }
    
}
