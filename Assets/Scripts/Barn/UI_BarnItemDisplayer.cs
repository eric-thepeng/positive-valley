using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BarnItemDisplayer : MonoBehaviour
{
    public Image iconImage;
    public Image rarityFrameImage;

    public void SetUp()
    {
        
    }

    public void SetUp(Sprite iconSprite, Color rarityColor)
    {
        iconImage.sprite = iconSprite;
        rarityFrameImage.color = rarityColor;
        
        iconImage.gameObject.SetActive(true);
        rarityFrameImage.gameObject.SetActive(true);
    }
}
