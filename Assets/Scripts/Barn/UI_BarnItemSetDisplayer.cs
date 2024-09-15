using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BarnItemSetDisplayer : MonoBehaviour
{
    public GameObject barnItemDisplayContainer;
    public UI_BarnItemDisplayer barnItemDisplayerTemplate;
    public Vector2Int barnItemDimension;
    public Vector2 barnItemDelta;
    public bool generateEmptyBlocks = false;

    public UI_BarnItemDisplayer.DisplaySetting displaySetting;

    public void Display(BarnItemSet bis)
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

                List<BarnItem> allBarnItems = bis.GetDataInfoList();
                
                if (totalCount < allBarnItems.Count) // generate block
                {
                    newGO.GetComponent<UI_BarnItemDisplayer>().SetUp(totalCount, allBarnItems[totalCount],displaySetting);
                }
                else // generate empty block
                {
                    if(generateEmptyBlocks)newGO.GetComponent<UI_BarnItemDisplayer>().SetUpAsEmpty(totalCount);
                    else newGO.SetActive(false);
                }
                
                totalCount++;
            }
        }
        barnItemDisplayerTemplate.gameObject.SetActive(false);
    }
}
