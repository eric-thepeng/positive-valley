using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    public GameObject barnItemDisplayContainer;
    public UI_BarnItemDisplayer barnItemDisplayerTemplate;

    public Vector2 barnItemDelta;
    public Vector2Int barnItemDimension;
    
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
    }

    public void RefreshDisplay()
    {
        // Destroy Origional
        for (int i = barnItemDisplayContainer.transform.childCount - 1; i >= 0; i--)
        {
            if(barnItemDisplayContainer.transform.GetChild(i) == barnItemDisplayerTemplate.transform) continue;
            Destroy(barnItemDisplayContainer.transform.GetChild(i).gameObject);
        }
        
        // Generate New (col, row) -> (0,0) to (0,n)
        barnItemDisplayerTemplate.gameObject.SetActive(true);
        int totalCount = 0;
        for (int row = 0; row < barnItemDimension.y; row++)
        {
            for (int col = 0; col < barnItemDimension.x; col++)
            {
                GameObject newGO = Instantiate(barnItemDisplayerTemplate.gameObject, barnItemDisplayContainer.transform);
                newGO.transform.localPosition += new Vector3(col * barnItemDelta.x, - row * barnItemDelta.y, 0);

                if (totalCount < allBarnItems.Count) // generate block
                {
                    newGO.GetComponent<UI_BarnItemDisplayer>().SetUp(totalCount, allBarnItems[totalCount]);
                }
                else // generate empty block
                {
                    newGO.GetComponent<UI_BarnItemDisplayer>().SetUp(totalCount);
                }
                
                totalCount++;
            }
        }
        barnItemDisplayerTemplate.gameObject.SetActive(false);
    }
}
