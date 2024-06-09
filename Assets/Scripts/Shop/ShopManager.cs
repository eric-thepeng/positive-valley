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
    private List<ShopItemTemplate> allShopItemTemplates;

    // PUBLIC STATIC VARIABLES
    public static SO_ShopItem holdingShopItem = null;
    
    // SINGLETON
    static ShopManager instance;
    public static ShopManager i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ShopManager>();
            }
            return instance;
        }
    }
    // ----------
    
    private void Start()
    {
        SetUpShopItemsDisplay();
        PlayerState.OnShopStatusChange.AddListener(onShopStatusChange);
    }

    public void SelectShopItem(ShopItemTemplate sit)
    {
        PlayerState.ChangeShopStatus(PlayerState.ShopStatus.Shopping);
        holdingShopItem = sit.sosi;
        foreach (var VARIABLE in allShopItemTemplates)
        {
            VARIABLE.SetDisplayToRegular();
        }
        sit.SetDisplayToSelected();
    }

    private void SetUpShopItemsDisplay()
    {
        allShopItemTemplates = new List<ShopItemTemplate>();
        
        //Display New Content
        shopItemTemplate.gameObject.SetActive(true);
        for (int i = 0; i < sosiToDisplay.Count; i++)
        {
            ShopItemTemplate sit = Instantiate(shopItemTemplate, shopItemContainer);
            allShopItemTemplates.Add(sit);
            sit.SetUp(sosiToDisplay[i]);
            sit.transform.localPosition +=
                new Vector3(i % shopGridCount.x * shopGridDelta.x, 
                    ((int)(i / shopGridCount.x))*shopGridDelta.y, 
                    0);
        }
        shopItemTemplate.gameObject.SetActive(false);
    }

    public void onShopStatusChange(PlayerState.ShopStatus newShopStatus)
    {
        if (newShopStatus != PlayerState.ShopStatus.Shopping)
        {
            holdingShopItem = null;
        }
    }

    public void OpenClosePanel()
    {
        switch (PlayerState.shopStatus)
        {
            case PlayerState.ShopStatus.Shopping:
                CloseShopPanel();
                break;
            case PlayerState.ShopStatus.Open:
                CloseShopPanel();
                break;
            case PlayerState.ShopStatus.Close:
                OpenShopPanel();
                break;
            case PlayerState.ShopStatus.Hide:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void OpenShopPanel()
    {
        shopPanel.transform.DOLocalMoveY(shopPanelOpenY,0.5f);
        PlayerState.ChangeShopStatus(PlayerState.ShopStatus.Open);
    }

    public void CloseShopPanel()
    {
        shopPanel.transform.DOLocalMoveY(shopPanelCloseY,0.5f);
        PlayerState.ChangeShopStatus(PlayerState.ShopStatus.Close);
        
    }
}
