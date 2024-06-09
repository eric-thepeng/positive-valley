using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Field : PlayerWorldInteractable
{
    [Header("== Dependencies ==")]
    [SerializeField] private SpriteRenderer soilSR;
    [SerializeField] private SpriteRenderer cropSR;
    [SerializeField] private Animator canPlantIndicationAnimator;

    private Color soilRegularColor;
    private Color soilShoopingColor;

    /// <summary>
    /// Locked: Locked and need to be purchased.
    /// Empty: Unlocked and nothing is planted.
    /// Planted: Planted with seed.
    /// </summary>
    enum FieldState {Locked, Empty, PlantedWatered, PlantedDry}
    [SerializeField, Header("== View Only ==")]private FieldState fieldState = FieldState.Empty;

    enum FieldDisplayState {Regular, Shopping }
    [SerializeField]private FieldDisplayState fieldDisplayState = FieldDisplayState.Regular;

    [Header("[Soil Sprites]")] 
    [SerializeField] private Sprite soilSpriteLocked;
    [SerializeField] private Sprite soilSpriteEmpty;
    [SerializeField] private Sprite soilSpritePlantedWatered;
    [SerializeField] private Sprite soilSpritePlantedDry;
    
    [Header("[Set Up]")]
    [SerializeField, Header("== View Only ==")]private FieldState initialFieldState = FieldState.Empty;


    private void OnEnable()
    {
        PlayerState.OnShopStatusChange.AddListener(onShoppingStateChange);
        soilRegularColor = soilSR.color;

        soilShoopingColor = new Color(soilRegularColor.r, soilRegularColor.g,
            soilRegularColor.b, soilRegularColor.a * 0.8f);
    }

    private void Start()
    {
        switch (initialFieldState)
        {
            case FieldState.Locked:
                break;
            case FieldState.Empty:
                break;
            case FieldState.PlantedWatered:
                break;
            case FieldState.PlantedDry:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void onShoppingStateChange(PlayerState.ShopStatus newShopStatus)
    {
        switch (newShopStatus)
        {
            case PlayerState.ShopStatus.Shopping:
                DisplayShoppingState();
                break;
            case PlayerState.ShopStatus.Open:
                DisplayRegularState();
                break;
            case PlayerState.ShopStatus.Close:
                DisplayRegularState();
                break;
            case PlayerState.ShopStatus.Hide:
                DisplayRegularState();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newShopStatus), newShopStatus, null);
        }
    }

    private void DisplayRegularState()
    {
        if(fieldDisplayState == FieldDisplayState.Regular) return;
        
        canPlantIndicationAnimator.gameObject.SetActive(false);

        
        fieldDisplayState = FieldDisplayState.Regular;
    }

    private void DisplayShoppingState()
    {
        if(fieldDisplayState == FieldDisplayState.Shopping) return;

        canPlantIndicationAnimator.gameObject.SetActive(true);
        
        fieldDisplayState = FieldDisplayState.Shopping;
    }

    private IEnumerator shakeShoppingField()
    {
        while (true)
        {
            //shoppingStateGO.transform.DOShakeScale(0.5f, new Vector3(.15f, .15f, 0),15,90,true,ShakeRandomnessMode.Harmonic);
            soilSR.DOColor(soilShoopingColor, 0.6f);
            yield return new WaitForSeconds(0.6f);
            soilSR.DOColor(soilRegularColor, 0.6f);
            yield return new WaitForSeconds(.6f);
        }
    }

    protected override void OnPlayerTouch()
    {
        print("Player Touch");
        if (PlayerState.shopStatus == PlayerState.ShopStatus.Shopping) //SHOPPING
        {
            switch (fieldState)
            {
                case FieldState.Locked:
                    break;
                case FieldState.Empty:
                    TryToPlantSeed();
                    break;
                case FieldState.PlantedWatered:
                    break;
                case FieldState.PlantedDry:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else //BROWSING
        {
            switch (fieldState)
            {
                case FieldState.Locked:
                    DisplayUnlockUI();
                    break;
                case FieldState.Empty:
                    break;
                case FieldState.PlantedWatered:
                    break;
                case FieldState.PlantedDry:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void DisplayUnlockUI()
    {
        
    }

    public void TryToPlantSeed()
    {
        
    }
}
