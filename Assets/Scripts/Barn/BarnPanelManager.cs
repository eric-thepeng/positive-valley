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

    // DEPENDENCIES
    public GameObject panelGO;
    public GameObject barnItemDisplayContainer;
    public UI_BarnItemDisplayer barnItemDisplayerTemplate;

    public Vector2 barnItemDelta;
    public Vector2Int barnItemDimension;
    
    // PRIVATE VARIABLES
    private List<BarnItem> allBarnItems;

    private void Awake()
    {
        allBarnItems = new List<BarnItem>();
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
        ES3.Save("BarnPanelManager_allBarnItems", allBarnItems, SaveLoadManager.i.saveFileName);
    }
    
    private void LoadGameFile()
    {
        if(SaveLoadManager.i.GetLoadMode() == SaveLoadManager.LoadMode.NewGame) return;
        allBarnItems = ES3.Load<List<BarnItem>>("BarnPanelManager_allBarnItems", SaveLoadManager.i.loadFileName);
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

    public bool HasBarnItemSet(BarnItemSet bis)
    {
        return true;
    }

    public bool SpendBarnItemSet(BarnItemSet bis)
    {
        return true;
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
            allBarnItems.RemoveAt(removingIndex[i] - i);
        }
        
        /*
        foreach (var VARIABLE in items)
        {
            allBarnItems.Remove(VARIABLE.displayingBarnItem);
        }*/
        
        RefreshDisplay();
        
    }

    public void DeleteBarnItems(List<BarnItem> items)
    {
        foreach (var VARIABLE in items)
        {
            allBarnItems.Remove(VARIABLE);
        }
        
        RefreshDisplay();
    }
    
}
