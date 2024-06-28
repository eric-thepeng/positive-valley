using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    
    public enum GamePanel {Valley, Positivity, Barn, Collection, Friends, Settings}
    public GamePanel gamePanel = GamePanel.Valley;

    public UnityEvent<GamePanel> OnNewPanelEnters = new UnityEvent<GamePanel>();
    public UnityEvent<GamePanel> OnPanelCloses = new UnityEvent<GamePanel>();

    public void SwitchPanelTo(GamePanel newPanel)
    {
        if(gamePanel == newPanel) return;

        OnPanelCloses.Invoke(gamePanel);
        gamePanel = newPanel;
        OnNewPanelEnters.Invoke(gamePanel);
    }
}
