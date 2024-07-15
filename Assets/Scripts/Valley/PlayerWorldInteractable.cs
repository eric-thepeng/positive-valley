using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerWorldInteractable : MonoBehaviour
{
    protected virtual void OnPlayerTouchAsButton() { }

    protected virtual void OnPlayerTouchEnter() { }
    
    private void OnMouseUpAsButton()
    {
#if UNITY_EDITOR
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            OnPlayerTouchAsButton();
        }
#else
        if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            OnPlayerTouchAsButton();
        }
#endif
    }

    private void OnMouseEnter()
    {
#if UNITY_EDITOR
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            OnPlayerTouchEnter();
        }
#else
        if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            OnPlayerTouchEnter();
        }
#endif
    }
}
