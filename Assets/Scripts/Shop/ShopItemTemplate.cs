using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemTemplate : MonoBehaviour
{
    [Header("== Lock ==")] 
    [SerializeField] private GameObject lockedStateGameObject;
    [SerializeField] private GameObject unlockedStateGameObject;

    [Header("== Dependencies ==")] 
    [SerializeField] private TMP_Text unlockLevelTMPT;
    [SerializeField] private TMP_Text nameTMPT;
    [SerializeField] private TMP_Text priceTMPT;
    [SerializeField] private Image iconImage;
    [SerializeField] private Button button;

    [Header("== Variables ==")] [SerializeField]
    private Color pressedColor;
    
    public SO_ShopItem sosi;

    public void SetUp(SO_ShopItem sosi)
    {
        unlockLevelTMPT.text = "Unlock at\nLv. " + sosi.unlockLevel;
        nameTMPT.text = sosi.itemName;
        priceTMPT.text = "$ " + sosi.buyCost;
        iconImage.sprite = sosi.itemIcon;
        this.sosi = sosi;

        if (sosi.unlockLevel <= PlayerStat.level.GetValue()) //start in unlocked state
        {
            unlockedStateGameObject.SetActive(true);
            lockedStateGameObject.SetActive(false);
        }
        else //start in locked state
        {
            unlockedStateGameObject.SetActive(false);
            lockedStateGameObject.SetActive(true);
            PlayerStat.level.SubscribeChangeValue(UnlockCheck);
        }
    }

    public void UnlockCheck(int currentLevel)
    {
        if (PlayerStat.level.GetValue() >= sosi.unlockLevel)
        {
            unlockedStateGameObject.SetActive(true);
            lockedStateGameObject.SetActive(false);
            PlayerStat.level.UnsubscribeChangeValue(UnlockCheck);
        }
    }

    public void SelectShopItem()
    {
        ShopManager.i.SelectShopItem(this);
    }

    public void SetDisplayToRegular()
    {
        button.GetComponent<Image>().color = Color.white;
    }

    public void SetDisplayToSelected()
    {
        button.GetComponent<Image>().color = pressedColor;
    }
}
