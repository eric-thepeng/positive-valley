using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldInteractable : MonoBehaviour
{
    protected virtual void OnPlayerTouch()
    {
        throw new NotImplementedException();
    }
    
    private void OnMouseUpAsButton()
    {
        if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) OnPlayerTouch();
    }
}
