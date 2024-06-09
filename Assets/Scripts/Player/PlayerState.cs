using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class PlayerState
{
    public enum PlayerPanel{Collection, Positivity, Valley, Friends, Settings}
    public static PlayerPanel playerPanel = PlayerPanel.Valley;
    public static UnityEvent<PlayerPanel> OnPlayerPanelChange = new UnityEvent<PlayerPanel>();
    
    public static void ChangePlayerPanel(PlayerPanel targetPlayerPanel)
    {
        playerPanel = targetPlayerPanel;
        OnPlayerPanelChange.Invoke(playerPanel);
    }

    public enum ShopStatus{Shopping, Open, Close, Hide}
    public static ShopStatus shopStatus = ShopStatus.Close;
    public static UnityEvent<ShopStatus> OnShopStatusChange = new UnityEvent<ShopStatus>();

    public static void ChangeShopStatus(ShopStatus newShopStatus)
    {
        shopStatus = newShopStatus;
        OnShopStatusChange.Invoke(shopStatus);
    }
    

}
