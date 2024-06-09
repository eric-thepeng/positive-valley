using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemTemplate : MonoBehaviour
{
    [Header("== Dependencies ==")]
    [SerializeField] private TMP_Text nameTMPT;
    [SerializeField] private TMP_Text priceTMPT;
    [SerializeField] private Image iconImage;
    [SerializeField] private Button button;

    [Header("== Variables ==")] [SerializeField]
    private Color pressedColor;
    
    public SO_ShopItem sosi;

    public void SetUp(SO_ShopItem sosi)
    {
        nameTMPT.text = sosi.itemName;
        priceTMPT.text = "$ " + sosi.buyCost;
        iconImage.sprite = sosi.itemIcon;
        this.sosi = sosi;
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
