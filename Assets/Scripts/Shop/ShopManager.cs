using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShopManager : MonoBehaviour
{
    // SERIALIZED PRIVATE VARIABLES
    [Header("Panel Related")]
    [SerializeField] private RectTransform shopPanel;
    [SerializeField] private float shopPanelOpenY;
    [SerializeField] private float shopPanelCloseY;
    [SerializeField] private float shopPanelHideY;

    [Header("Shop Item Related")] 
    [SerializeField] private ShopItemTemplate shopItemTemplate;
    [SerializeField] private RectTransform shopItemContainer;
    [SerializeField] private Vector2Int shopGridCount;
    [SerializeField] private Vector2 shopGridDelta;
    [SerializeField] private List<SO_ShopItem> sosiToDisplay;

    // PRIVATE VARIABLES
    private bool isPanelOpen = false;

    private void Start()
    {
        SetUpShopItemsDisplay();
    }

    private void SetUpShopItemsDisplay()
    {
        //Clear Current Display
        foreach (RectTransform rt in shopItemContainer)
        {
            //if(rt!=shopItemContainer) Destroy(rt.gameObject);
        }
        
        //Display New Content
        shopItemTemplate.gameObject.SetActive(true);
        for (int i = 0; i < sosiToDisplay.Count; i++)
        {
            ShopItemTemplate sit = Instantiate(shopItemTemplate, shopItemContainer);
            sit.SetUp(sosiToDisplay[i]);
            sit.transform.localPosition +=
                new Vector3(i % shopGridCount.x * shopGridDelta.x, 
                    ((int)(i / shopGridCount.x))*shopGridDelta.y, 
                    0);
        }
        shopItemTemplate.gameObject.SetActive(false);
    }

    public void OpenClosePanel()
    {
        if (isPanelOpen)
        {
            CloseShopPanel();
        }
        else
        {
            OpenShopPanel();
        }
    }

    public bool IsPanelOpen()
    {
        return isPanelOpen;
    }

    public void OpenShopPanel()
    {
        shopPanel.transform.DOLocalMoveY(shopPanelOpenY,0.5f);
        isPanelOpen = true;
    }

    public void CloseShopPanel()
    {
        shopPanel.transform.DOLocalMoveY(shopPanelCloseY,0.5f);
        isPanelOpen = false;
    }
}
