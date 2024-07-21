using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerWorldInteractable : MonoBehaviour
{
    protected virtual void OnPlayerTouchDown()
    {
        isDragging = true;
        lastMousePosition = Input.mousePosition;
    }

    protected virtual void OnPlayerTouchEnter()
    {
        
    }

    protected virtual void OnPlayerTouchDrag(Vector2 deltaDrag)
    {
        
    }

    protected virtual void OnPlayerTouchExit()
    {
        isDragging = false;
    }
    
    protected virtual void OnPlayerTouchUp()
    {
        isDragging = false;
    }

    protected virtual void OnPlayerTouchAsButton()
    {
        
    }

    // PRIVATE VARIABLES
    private bool isDragging = false;
    private Vector2 lastMousePosition;
    
    protected virtual void Update()
    {
        if(!isDragging) return;

        Vector2 currentMousePosition = Input.mousePosition;
        Vector2 deltaDrag = currentMousePosition - lastMousePosition;
        OnPlayerTouchDrag(deltaDrag);
        lastMousePosition = currentMousePosition;
    }
    
    // INPUT PROCESSING ONLY
    private void OnMouseDown()
    {
#if UNITY_EDITOR
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            OnPlayerTouchDown();
        }
#else
        if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            OnPlayerTouchDown();
        }
#endif
    }

    private void OnMouseEnter()
    {
#if UNITY_EDITOR
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton(0))
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
    
    private void OnMouseExit()
    {
        OnPlayerTouchExit();
    }

    private void OnMouseUp()
    {
        OnPlayerTouchExit();
    }
    
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
}
