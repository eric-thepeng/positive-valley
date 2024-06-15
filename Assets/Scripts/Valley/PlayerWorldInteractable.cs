using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWorldInteractable : MonoBehaviour
{
    protected virtual void OnPlayerTouchBegin()
    {
        throw new NotImplementedException();
    }

    protected virtual void OnPlayerTouchEnter()
    {
        
    }
    
    private void OnMouseUpAsButton()
    {
        if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) OnPlayerTouchBegin();
    }

    private void OnMouseEnter()
    {
        if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) OnPlayerTouchEnter();
    }
}
