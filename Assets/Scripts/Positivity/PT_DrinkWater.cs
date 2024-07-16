using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PT_DrinkWater : PositivityTask
{
    // CLASSES
    public class CurrentState
    {
        public DateTime lastChangedDateTime;
        public int currentDayCount;
        public int maxCountPerDay = 8;

        public CurrentState()
        {
            lastChangedDateTime = DateTime.Now;
            currentDayCount = 0;
        }

        public bool IsFullToday()
        {
            return maxCountPerDay <= currentDayCount;
        }

        public void IncreaseCount()
        {
            if(IsFullToday()) return;
            currentDayCount++;
        }
    }

    // PUBLIC VARIABLES
    public CurrentState currentState;
    
    // PRIVATE VARIABLES
    private PT_DrinkWaterDisplayer dwDisplayer;
    

    private void OnEnable()
    {
        if (currentState == null) { currentState = new CurrentState(); }
        if(dwDisplayer == null) dwDisplayer = GetComponent<PT_DrinkWaterDisplayer>();
        RefreshDisplay();
    }
    
    public void DrinkWaterButton()
    {
        if (currentState.IsFullToday())
        {
            //PopUpUIManager.i.DisplaySimpleTextPopUpDisappear("Today's Water Goal Reached Already!",3,0.5f,false, false);
            PopUpUIManager.i.DisplaySimpleTextPopUp("Today's water goal already reached!");
        }
        else
        {
            string descriptionString = "Drink a cup of water!";
            List<UnityAction> buttonActions = new List<UnityAction>(){DrinkWater,ExitUI};
            List<string> buttonStrings = new List<string>() { "DONE !" ,"Later ~"};
            PopUpUIManager.i.DisplayDrinkWaterPopUpSelect(descriptionString, buttonActions, buttonStrings);
        }
    }

    public void DrinkWater()
    {
        ExitUI();
        currentState.IncreaseCount();
        AddDrinkWaterBonus();
        RefreshDisplay(currentState.currentDayCount-1);
    }

    public void ExitUI()
    {
        PopUpUIManager.i.ExitDrinkWaterPopUp();
    }

    public void AddDrinkWaterBonus()
    {
        PlayerStat.money.ChangeValue(50);
        PopUpUIManager.i.DisplaySimpleTextPopUp("Obtained Reward: 50 Suns!");
    }

    public void RefreshDisplay(int animationBlockIndex = -1)
    {
        dwDisplayer.Display(currentState, animationBlockIndex);
    }
    
}
