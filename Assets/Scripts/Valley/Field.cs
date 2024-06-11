using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Field : PlayerWorldInteractable
{
    // SERIALIZED PRIVATE VARIABLES
    [Header("== Dependencies ==")]
    [SerializeField] private SpriteRenderer soilSR;
    [SerializeField] private SpriteRenderer cropSR;
    [SerializeField] private SpriteRenderer canPlantIndicationSR;

    [Header("[Soil Sprites]")] 
    [SerializeField] private Sprite soilSpriteLocked;
    [SerializeField] private Sprite soilSpriteEmpty;
    [SerializeField] private Sprite soilSpritePlantedWatered;
    [SerializeField] private Sprite soilSpritePlantedDry;
    
    [Header("[Set Up]")]
    [SerializeField, Header("== View Only ==")]private FieldState initialFieldState = FieldState.Empty;
    [SerializeField] private int unlockCost = 0;

    // PRIVATE VARIABLES
    // Color Related
    private Color soilRegularColor;
    private Color soilShoopingColor;
    // Seed and Grow
    private FieldGrowth fieldGrowth = null;
    private float seedDuration = 0f;
    private int seedStage = 0;

    /// <summary>
    /// Locked: Locked and need to be purchased.
    /// Empty: Unlocked and nothing is planted.
    /// Planted: Planted with seed.
    /// </summary>
    enum FieldState {Locked, Empty, Planted, CanHarvest}
    [SerializeField, Header("== View Only ==")]private FieldState fieldState = FieldState.Empty;

    enum HoverIndicationState {Regular, Shopping, Harvest}
    [SerializeField]private HoverIndicationState hoverIndicationState = HoverIndicationState.Regular;

    private void OnEnable()
    {
        PlayerState.OnShopStatusChange.AddListener(onShoppingStateChange);
        soilRegularColor = soilSR.color;

        soilShoopingColor = new Color(soilRegularColor.r, soilRegularColor.g,
            soilRegularColor.b, soilRegularColor.a * 0.8f);
    }

    private void Start()
    {
        ChangeFieldStateTo(initialFieldState);
    }

    private void Update()
    {
        // Grow Crop
        if (fieldState == FieldState.Planted)
        {
            if (fieldGrowth.Grow(Time.deltaTime))
            {
                ChangeFieldStateTo(FieldState.CanHarvest);
            }
        }
    }

    private void ChangeFieldStateTo(FieldState newFieldState)
    {
        fieldState = newFieldState;
        switch (newFieldState)
        {
            case FieldState.Locked:
                soilSR.sprite = soilSpriteLocked;
                break;
            case FieldState.Empty:
                soilSR.sprite = soilSpriteEmpty;
                break;
            case FieldState.Planted:
                soilSR.sprite = soilSpritePlantedDry;
                break;
            case FieldState.CanHarvest:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newFieldState), newFieldState, null);
        }
    }

    private void onShoppingStateChange(PlayerState.ShopStatus newShopStatus)
    {
        switch (newShopStatus)
        {
            case PlayerState.ShopStatus.Shopping:
                DisplayShoppingHover();
                break;
            case PlayerState.ShopStatus.Open:
                DisplayRegularHover();
                break;
            case PlayerState.ShopStatus.Close:
                DisplayRegularHover();
                break;
            case PlayerState.ShopStatus.Hide:
                DisplayRegularHover();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newShopStatus), newShopStatus, null);
        }
    }

    private void DisplayRegularHover()
    {
        canPlantIndicationSR.color = Color.clear;
        hoverIndicationState = HoverIndicationState.Regular;
        print("display regular hover");
    }

    private void DisplayShoppingHover()
    {
        if(hoverIndicationState == HoverIndicationState.Shopping) return;
        if(fieldState != FieldState.Empty) return;

        canPlantIndicationSR.color = Color.white;
        
        hoverIndicationState = HoverIndicationState.Shopping;
    }

    /*
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
    }*/

    protected override void OnPlayerTouch()
    {
        print("Player Touch");
        if (PlayerState.shopStatus == PlayerState.ShopStatus.Shopping) 
        {
            //SHOPPING
            switch (fieldState)
            {
                case FieldState.Locked:
                    DisplayUnlockUI();
                    break;
                case FieldState.Empty:
                    TryToPlantSeed();
                    break;
                case FieldState.Planted:
                    break;
                case FieldState.CanHarvest:
                    HarvestCrop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else 
        {
            //BROWSING
            switch (fieldState)
            {
                case FieldState.Locked:
                    DisplayUnlockUI();
                    break;
                case FieldState.Empty:
                    break;
                case FieldState.Planted:
                    break;
                case FieldState.CanHarvest:
                    HarvestCrop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void HarvestCrop()
    {
        cropSR.sprite = null;
        
        PlayerStat.money.ChangeValue(fieldGrowth.seed.harvestMoney);
        PlayerStat.experience.ChangeValue(fieldGrowth.seed.harvestExperience);
        
        fieldGrowth = null;
        if (PlayerState.shopStatus == PlayerState.ShopStatus.Shopping) { DisplayShoppingHover(); } // put this line after reset currentSeed
        ChangeFieldStateTo(FieldState.Empty);
    }

    public void TryToPlantSeed()
    {
        if(fieldGrowth != null) return;
        if (ShopManager.holdingShopItem is SOSI_Seed)
        {
            SOSI_Seed currentSeed = (SOSI_Seed)ShopManager.holdingShopItem;
            if (PlayerStat.money.HasValue(currentSeed.buyCost))
            {
                PlayerStat.money.ChangeValue(-currentSeed.buyCost);
                print("money left: " + PlayerStat.money.GetValue());
                fieldGrowth = new FieldGrowth(currentSeed, ChangeCropSprite);
                DisplayRegularHover();
            }
            ChangeFieldStateTo(FieldState.Planted);
        }
    }

    public void ChangeCropSprite(Sprite cropSprite)
    {
        cropSR.sprite = cropSprite;
    }

    public int GetUnlockCost()
    {
        return unlockCost;
    }

    public void DisplayUnlockUI()
    {
        PopUpUIManager.i.DisplayUnlockFieldPopUp(this);
    }
    
    public void TryToUnlock()
    {
        if (PlayerStat.money.HasValue(unlockCost))
        {
            PlayerStat.money.ChangeValue(-unlockCost);
            ChangeFieldStateTo(FieldState.Empty);
            PopUpUIManager.i.ExitUnlockFieldPopUp();
        }
    }
}
