using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_BarnItemDisplayer : MonoBehaviour
{
    public Image iconImage;
    public Image rarityFrameImage;
    public SOSI_Seed displayingSeed = null;
    public int blockID;

    public void SetUp(int blockID)
    {
        this.blockID = blockID;
    }

    public void SetUp(int blockID, BarnPanelManager.BarnItem barnItem)
    {
        this.blockID = blockID;
        displayingSeed = barnItem.itemSeed;

        iconImage.sprite = barnItem.itemSeed.itemIcon;
        rarityFrameImage.color = barnItem.itemRarity.color;
        
        iconImage.gameObject.SetActive(true);
        rarityFrameImage.gameObject.SetActive(true);
    }
}
