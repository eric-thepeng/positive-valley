using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class PositivityPanelManager : MonoBehaviour
{
    // SINGLETON
    static PositivityPanelManager instance;
    public static PositivityPanelManager i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PositivityPanelManager>();
            }
            return instance;
        }
    }
    
    // SERIALIZED PRIVATE VARIABLES
    // dependencies
    [SerializeField] private GameObject panelGO;
    
    private void OnEnable()
    {
        GamePanelsManager.i.OnNewPanelEnters.AddListener(OpenPanel);
        GamePanelsManager.i.OnPanelCloses.AddListener(ClosePanel);
    }
    
    public void OpenPanel(GamePanelsManager.GamePanel panel)
    {
        if(panel != GamePanelsManager.GamePanel.Positivity) return;
        panelGO.SetActive(true);
        RefreshDisplay();
    }

    public void ClosePanel(GamePanelsManager.GamePanel panel)
    {
        if(panel != GamePanelsManager.GamePanel.Positivity) return;
        panelGO.SetActive(false);
    }

    public void RefreshDisplay()
    {
        
    }
}
