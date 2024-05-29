using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{

    [SerializeField] private RectTransform shopPanel;
    [SerializeField] private float shopPanelOpenY;
    [SerializeField] private float shopPanelCloseY;
    [SerializeField] private float shopPanelHideY;

    private bool isPanelOpen = false;

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
        
    }

    public void CloseShopPanel()
    {
        
    }
}
