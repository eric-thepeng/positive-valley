using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PopUpCropHarvest : MonoBehaviour
{
    [SerializeField] private UI_PopUpAndDisappear puadTemplate;

    public void SetUpAndDisplay(List<BarnItem> barnItems)
    {
        int amount = barnItems.Count;

        for (int i = 0; i < amount; i++)
        {
            BarnItem bi = barnItems[i];
            GameObject newPUD = Instantiate(puadTemplate.gameObject, this.transform);
            newPUD.transform.localPosition += new Vector3(0, 130, 0) * i;
            newPUD.SetActive(true);
            UI_PopUpAndDisappear puad = newPUD.GetComponent<UI_PopUpAndDisappear>();
            //"[Legendary] Eggplant +1"
            string descriptionString = "[" + bi.itemRarity.rarityName +"] " + bi.itemSeed.itemName + " +1";
            puad.SetUpAndDisplay(bi.itemSeed.itemIcon, descriptionString, .6f, 0.6f, false, false);
            puad.SetBackgroundFrameColor(bi.itemRarity.color);
        }
    }
}
