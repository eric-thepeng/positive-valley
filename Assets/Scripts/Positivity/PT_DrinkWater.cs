using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PT_DrinkWater : PositivityTask
{
    public class CurrentState
    {
        public DateTime lastChangedDateTime;
        public int countInDay;
        public int maxCountPerDay = 2;

        public CurrentState()
        {
            lastChangedDateTime = DateTime.Now;
            countInDay = 0;
        }

        public bool IsFullToday()
        {
            return maxCountPerDay <= countInDay;
        }

        public void IncreaseCount()
        {
            if(IsFullToday()) return;
            countInDay++;
        }
    }

    public CurrentState currentState;

    private void Start()
    {
        if (currentState == null)
        {
            currentState = new CurrentState();
        }
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
        RefreshDisplay();
    }

    public void ExitUI()
    {
        PopUpUIManager.i.ExitDrinkWaterPopUp();
    }

    public void AddDrinkWaterBonus()
    {
        //TODO
        PlayerStat.money.ChangeValue(50);
    }

    public void RefreshDisplay()
    {
        //TODO
    }
}
