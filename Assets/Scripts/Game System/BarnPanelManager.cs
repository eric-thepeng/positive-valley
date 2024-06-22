using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarnPanelManager : MonoBehaviour
{
    static BarnPanelManager instance;
    public static BarnPanelManager i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<BarnPanelManager>();
            }
            return instance;
        }
    }

    public GameObject panelGO;

    public void OpenPanel()
    {
        panelGO.SetActive(true);
        RefreshDisplay();
    }

    public void ClosePanel()
    {
        panelGO.SetActive(false);
    }

    public void RefreshDisplay()
    {
        
    }
}
