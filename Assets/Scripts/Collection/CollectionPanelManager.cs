using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject panelGO;
    [SerializeField] private GameObject collectionItemDisplayerContainer;
    [SerializeField] private UI_CollectionItemDisplayer collectionItemDisplayerTemplate;

    [SerializeField] private float collectionItemYDelta;
    
    [SerializeField] private List<SOSI_Seed> seedsToUnlock;
    
    private void OnEnable()
    {
        GamePanelsManager.i.OnNewPanelEnters.AddListener(OpenPanel);
        GamePanelsManager.i.OnPanelCloses.AddListener(ClosePanel);
    }
    
    public void OpenPanel(GamePanelsManager.GamePanel panel)
    {
        if(panel != GamePanelsManager.GamePanel.Collection) return;
        panelGO.SetActive(true);
        RefreshDisplay();
    }
    
    public void ClosePanel(GamePanelsManager.GamePanel panel)
    {
        if(panel != GamePanelsManager.GamePanel.Collection) return;
        panelGO.SetActive(false);
    }

    public void RefreshDisplay()
    {
        // Destroy Origional
        for (int i = collectionItemDisplayerContainer.transform.childCount - 1; i >= 0; i--)
        {
            if(collectionItemDisplayerContainer.transform.GetChild(i) == collectionItemDisplayerTemplate.transform) continue;
            Destroy(collectionItemDisplayerContainer.transform.GetChild(i).gameObject);
        }
        
        // Generate New
        collectionItemDisplayerTemplate.gameObject.SetActive(true);
        for (int i = 0; i < seedsToUnlock.Count; i++)
        {
            GameObject newGO = Instantiate(collectionItemDisplayerTemplate.gameObject,
                collectionItemDisplayerContainer.transform);
            newGO.transform.localPosition += new Vector3(0, i * collectionItemYDelta, 0);

            newGO.GetComponent<UI_CollectionItemDisplayer>().SetUp(seedsToUnlock[i]);
        }
    }
}