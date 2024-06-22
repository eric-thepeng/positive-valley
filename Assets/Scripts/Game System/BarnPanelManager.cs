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
    
    public struct BarnItem
    {
        public SOSI_Seed itemSeed;
        public SO_Rarity itemRarity;

        public BarnItem(SOSI_Seed itemSeed, SO_Rarity itemRarity)
        {
            this.itemSeed = itemSeed;
            this.itemRarity = itemRarity;
        }
    }

    public GameObject panelGO;
    private List<BarnItem> allBarnItems;

    private void Awake()
    {
        allBarnItems = new List<BarnItem>();
    }
    
    public void OpenPanel()
    {
        panelGO.SetActive(true);
        RefreshDisplay();
    }

    public void ClosePanel()
    {
        panelGO.SetActive(false);
    }

    public void AddBarnItem(BarnItem newBarnItem)
    {
        allBarnItems.Add(newBarnItem);
        PopUpUIManager.i.QueDisplayCropHarvestPopUpDisappear(newBarnItem);
        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        
    }
}
