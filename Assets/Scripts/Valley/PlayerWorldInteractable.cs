using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldInteractable : MonoBehaviour
{
    protected virtual void OnPlayerTouchAsButton() { }

    protected virtual void OnPlayerTouchEnter() { }
    
    private void OnMouseUpAsButton()
    {
        if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) OnPlayerTouchAsButton();
    }

    private void OnMouseEnter()
    {
        if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) OnPlayerTouchEnter();
    }
}
