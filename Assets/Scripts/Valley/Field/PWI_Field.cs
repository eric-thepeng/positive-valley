using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class PWI_Field : PlayerWorldInteractable
{
    // IDENTIFIER
    static private int totalFields = 0;
    private int fieldID = 0;

    // SERIALIZED PRIVATE VARIABLES
    [Header("== Dependencies ==")]
    [SerializeField] private SpriteRenderer soilSR;
    [SerializeField] private SpriteRenderer cropSRTemplate;
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
    
    // Crop Effect
    private Vector3 cropSROrigionalLocalPosition;
    private bool cropGrowingShaking = false;
    private bool cropCanHarvestShaking = false;
    private List<SpriteRenderer> allCropSR = null;



    /// <summary>
    /// Locked: Locked and need to be purchased.
    /// Empty: Unlocked and nothing is planted.
    /// Planted: Planted with seed.
    /// </summary>
    public enum FieldState {Locked, Empty, Planted}
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
        totalFields++;
        fieldID = totalFields;
        ChangeFieldStateTo(initialFieldState);
        LoadGameFile();
    }

    private void OnApplicationQuit()
    {
        SaveGameFile();
    }

    /*
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
    }*/

    private void SaveGameFile()
    {
        if(SaveLoadManager.i.GetSaveMode() == SaveLoadManager.SaveMode.DoNotSave) return;
        ES3.Save("pwiField_" + fieldID + "_fieldState", fieldState, SaveLoadManager.i.saveFileName);
        ES3.Save("pwiField_" + fieldID + "_fieldGrowth", fieldGrowth, SaveLoadManager.i.saveFileName);
    }
    
    private void LoadGameFile()
    {
        if(SaveLoadManager.i.GetLoadMode() == SaveLoadManager.LoadMode.NewGame) return;
        ChangeFieldStateTo(ES3.Load<FieldState>("pwiField_" + fieldID + "_fieldState", SaveLoadManager.i.loadFileName));
        AssignFieldGrowth(ES3.Load<FieldGrowth>("pwiField_" + fieldID + "_fieldGrowth", SaveLoadManager.i.loadFileName));
        
        TimeSpan timeAway = SaveLoadManager.i.GetLastExitTimeAway();
        if (timeAway != TimeSpan.Zero)
        {
            GrowCrop((float)timeAway.TotalSeconds);
        }
    }

    private void Update()
    {
        // Grow Crop
        GrowCrop(Time.deltaTime);
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

    private void GrowCrop(float passTime)
    {
        if (fieldState == FieldState.Planted)
        {
            fieldGrowth.Grow(passTime);
        }
    }

    protected override void OnPlayerTouchAsButton()
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
                    //TryToPlantSeed();
                    break;
                case FieldState.Planted:
                    //TryHarvestCrop();
                    DisplayPlantedInfoPopUp();
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
                    //TryHarvestCrop();
                    DisplayPlantedInfoPopUp();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    protected override void OnPlayerTouchEnter()
    {
        print("Player Touch");
        if (PlayerState.shopStatus == PlayerState.ShopStatus.Shopping) 
        {
            //SHOPPING
            switch (fieldState)
            {
                case FieldState.Locked:
                    break;
                case FieldState.Empty:
                    TryToPlantSeed();
                    break;
                case FieldState.Planted:
                    TryHarvestCrop();
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
                    break;
                case FieldState.Empty:
                    break;
                case FieldState.Planted:
                    TryHarvestCrop();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public void DisplayPlantedInfoPopUp()
    {
        PopUpUIManager.i.DisplayFieldGrowInfoPopUp(this);
    }

    public bool TryHarvestCrop() 
    {
        fieldGrowth.TryToHarvest(out bool partialHarvest, out bool totalHarvest);

        if (totalHarvest)
        {
            AssignFieldGrowth(null);
            ChangeFieldStateTo(FieldState.Empty);
            if (PlayerState.shopStatus == PlayerState.ShopStatus.Shopping) { DisplayShoppingHover(); } // put this line after reset currentSeed
        }

        return partialHarvest;
    }

    public void TryToPlantSeed()
    {
        if(fieldGrowth != null) return;
        if (ShopManager.holdingShopItem is SOSI_Seed)
        {
            SOSI_Seed currentSeed = (SOSI_Seed)ShopManager.holdingShopItem;
            if (PlayerStat.money.HasValue(currentSeed.buyCost))
            {
                // Plant Seed
                
                PlayerStat.money.ChangeValue(-currentSeed.buyCost);
                print("money left: " + PlayerStat.money.GetValue());
                AssignFieldGrowth(new FieldGrowth(currentSeed, OnFieldGrowthPhaseChange));
                DisplayRegularHover();
                ChangeFieldStateTo(FieldState.Planted);

                // Start Shaking
                //cropSROrigionalLocalPosition = cropSR.transform.localPosition;
                //StartCoroutine(ShakeCropGrowing());
            }
        }
    }

    /*
    private IEnumerator ShakeCropGrowing()
    {
        cropGrowingShaking = true;
        cropSR.transform.localPosition += new Vector3(UnityEngine.Random.Range(-0.005f,0.005f), 0, 0); 
        
        yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 5f));

        while (fieldState == FieldState.Planted)
        {
            if (cropSR.transform.localPosition.x < cropSROrigionalLocalPosition.x)
            {
                cropSR.transform.DOLocalMoveX(cropSROrigionalLocalPosition.x +
                                              UnityEngine.Random.Range(0.01f, 0.02f),0.3f);
            }
            else
            {
                cropSR.transform.DOLocalMoveX(cropSROrigionalLocalPosition.x -
                                              UnityEngine.Random.Range(0.01f, 0.02f),0.3f);
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 10f));

        }
        
        cropGrowingShaking = false;
    }

    private IEnumerator ShakeCropCanHarvest()
    {
        cropCanHarvestShaking = true;
        cropSR.transform.localPosition = cropSROrigionalLocalPosition;

        while (fieldState == FieldState.CanHarvest)
        {
            cropSR.gameObject.transform.DOShakeRotation(0.5f,40f);
            yield return new WaitForSeconds(1f);
        }
        
        cropCanHarvestShaking = false;
    }*/

    public void AssignFieldGrowth(FieldGrowth tarFieldGrowth)
    {
        fieldGrowth = tarFieldGrowth;
        if (fieldGrowth == null) return; 
        
        fieldGrowth.ReassignOnPhaseChange(OnFieldGrowthPhaseChange);

        /*
        // Assign Shake
        if (fieldGrowth.CanHarvest()) //harvest
        {
            if (!cropCanHarvestShaking)
            {
                StartCoroutine(ShakeCropCanHarvest());
            }
        }
        else //grow
        {
            if (!cropGrowingShaking)
            {
                StartCoroutine(ShakeCropGrowing());
            }
        }*/
    }

    public void OnFieldGrowthPhaseChange(FieldGrowth tarFieldGrowth)
    {
        UpdateCropDisplay(tarFieldGrowth);
    }

    public void UpdateCropDisplay(FieldGrowth tarFieldGrowth)
    {
        if (allCropSR == null || tarFieldGrowth.allCropsPhases.Count != allCropSR.Count)
        {
            ClearAllCropSR();
        
            for (int i = 0; i < tarFieldGrowth.allCropsPhases.Count; i++)
            {
                GameObject newSRGO = Instantiate(cropSRTemplate.gameObject, transform);
                float localX = UnityEngine.Random.Range(-0.2f, 0.2f);
                float localY = UnityEngine.Random.Range(-0.2f + (0.4f / (tarFieldGrowth.allCropsPhases.Count) * i)
                                                        , -0.2f + (0.4f / (tarFieldGrowth.allCropsPhases.Count) * (i+1)));

                newSRGO.transform.localPosition = new Vector3(localX,localY,localY);
                allCropSR.Add(newSRGO.GetComponent<SpriteRenderer>());
            } 
        }

        for (int i = 0; i < allCropSR.Count; i++)
        {
            allCropSR[i].sprite = tarFieldGrowth.GetCropSprite(i);
        }
        
    }
    
    public void ClearAllCropSR()
    {
        if (allCropSR == null)
        {
            allCropSR = new List<SpriteRenderer>();
            return;
        }
        
        for (int i = allCropSR.Count-1; i >= 0; i--)
        {
            Destroy(allCropSR[i].gameObject);
        }

        allCropSR = new List<SpriteRenderer>();
    }

    public int GetUnlockCost()
    {
        return unlockCost;
    }

    public FieldGrowth GetFieldGrowth()
    {
        return fieldGrowth;
    }

    public void DisplayUnlockUI()
    {
        PopUpUIManager.i.DisplayUnlockFieldPopUpSelect(this);
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
