using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BarnPanelManager : MonoBehaviour
{
    // SINGLETON
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

    // DEPENDENCIES
    public GameObject panelGO;
    public GameObject barnItemDisplayContainer;
    public UI_BarnItemDisplayer barnItemDisplayerTemplate;

    public Vector2 barnItemDelta;
    public Vector2Int barnItemDimension;
    
    // PRIVATE VARIABLES
    // private List<BarnItem> allBarnItems;
    private BarnItemSet barnItemSetStorage;
    private UI_BarnItemSetDisplayer barnItemSetDisplayer;

    private void Awake()
    {
        barnItemSetStorage = new BarnItemSet();
        barnItemSetDisplayer = GetComponent<UI_BarnItemSetDisplayer>();
    }
    
    private void OnEnable()
    {
        GamePanelsManager.i.OnNewPanelEnters.AddListener(OpenPanel);
        GamePanelsManager.i.OnPanelCloses.AddListener(ClosePanel);
    }
    
    private void Start()
    {
        LoadGameFile();
    }

    private void OnApplicationQuit()
    {
        SaveGameFile();
    }
    
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // Enter Pause
            SaveGameFile();
        }
        else
        {
            // Resume
            LoadGameFile();
        }
    }

    private void SaveGameFile()
    {
        if(SaveLoadManager.i.GetSaveMode() == SaveLoadManager.SaveMode.DoNotSave) return;
        ES3.Save("BarnPanelManager_barnItemSetStorage", barnItemSetStorage, SaveLoadManager.i.saveFileName);
    }
    
    private void LoadGameFile()
    {
        if(SaveLoadManager.i.GetLoadMode() == SaveLoadManager.LoadMode.NewGame) return;
        barnItemSetStorage = ES3.Load<BarnItemSet>("BarnPanelManager_barnItemSetStorage", SaveLoadManager.i.loadFileName);
    }

    
    public void OpenPanel(GamePanelsManager.GamePanel panel)
    {
        if(panel != GamePanelsManager.GamePanel.Barn) return;
        panelGO.SetActive(true);
        RefreshDisplay();
    }

    public void ClosePanel(GamePanelsManager.GamePanel panel)
    {
        if(panel != GamePanelsManager.GamePanel.Barn) return;
        panelGO.SetActive(false);
    }

    public void AddBarnItem(BarnItem newBarnItem)
    {
        barnItemSetStorage.AddBarnItem(newBarnItem);
        PopUpUIManager.i.QueDisplayCropHarvestPopUpDisappear(newBarnItem);
    }

    public void AddBarnItems(List<BarnItem> newBarnItems)
    {
        barnItemSetStorage.AddBarnItem(newBarnItems);
        PopUpUIManager.i.DisplayCropHarvestMultipleAndDisappear(newBarnItems);
    }

    public void RefreshDisplay()
    {
        barnItemSetDisplayer.Display(barnItemSetStorage);
    }

    public bool SpendBarnItems(Dictionary<BarnItem,int> toSpend)
    {
        return barnItemSetStorage.SpendBarnItems(toSpend);
    }

    public void DeleteBarnItems(List<UI_BarnItemDisplayer> items)
    {

        List<int> removingIndex = new List<int>();

        foreach (var VARIABLE in items)
        {
            removingIndex.Add(VARIABLE.blockID);
        }
        
        removingIndex.Sort();

        for (int i = 0; i < removingIndex.Count; i++)
        {
            barnItemSetStorage.RemoveBarnItemAtIndex(removingIndex[i] - i);
        }
        
        /*
        foreach (var VARIABLE in items)
        {
            allBarnItems.Remove(VARIABLE.displayingBarnItem);
        }*/
        
        RefreshDisplay();
        
    }

}
