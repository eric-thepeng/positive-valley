using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValleyInteractionManager : MonoBehaviour
{
    static PlayerValleyInteractionManager instance;
    public static PlayerValleyInteractionManager i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PlayerValleyInteractionManager>();
            }
            return instance;
        }
    }
    
    public enum ValleyInteraction {None, MovingValley, PlantingSeeds, InspectingField}
    private ValleyInteraction currentValleyInteraction = ValleyInteraction.None;

    public bool CheckCurrentInteractionNone(ValleyInteraction valleyInteractionToCheck)
    {
        return currentValleyInteraction == valleyInteractionToCheck;
    }

    public void SetCurrentInteraction(ValleyInteraction valleyInteractionToSet)
    {
        currentValleyInteraction = valleyInteractionToSet;
    }
}
