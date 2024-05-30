using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopItemTemplate : MonoBehaviour
{
    [SerializeField] private TMP_Text nameTMPT;
    [SerializeField] private TMP_Text priceTMPT;

    public void SetUp(SO_ShopItem sosi)
    {
        nameTMPT.text = sosi.itemName;
        priceTMPT.text = "$ " + sosi.buyCost;
    }
}
