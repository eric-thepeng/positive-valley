using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanelsManager : MonoBehaviour
{
    static GamePanelsManager instance;
    public static GamePanelsManager i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GamePanelsManager>();
            }
            return instance;
        }
    }
    
    public enum GamePanel {Valley, Positivity, Barn, Friends, Settings}
    public GamePanel gamePanel = GamePanel.Valley;

    public void SwitchPanelTo(GamePanel newPanel)
    {
        if(gamePanel == newPanel) return;
        
        switch (newPanel)
        {
            case GamePanel.Valley:
                GoBackToValley();
                break;
            case GamePanel.Positivity:
                break;
            case GamePanel.Barn:
                BarnPanelManager.i.OpenPanel();
                break;
            case GamePanel.Friends:
                break;
            case GamePanel.Settings:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gamePanel), gamePanel, null);
        }

        gamePanel = newPanel;
        
        if(gamePanel!=GamePanel.Valley) ShopManager.i.HideShopPanel();
    }

    public void GoBackToValley()
    {
        switch (gamePanel)
        {
            case GamePanel.Valley:
                return;
            case GamePanel.Positivity:
                break;
            case GamePanel.Barn:
                BarnPanelManager.i.ClosePanel();
                break;
            case GamePanel.Friends:
                break;
            case GamePanel.Settings:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gamePanel), gamePanel, null);
        }
        
        ShopManager.i.CloseShopPanel();
    }
}
