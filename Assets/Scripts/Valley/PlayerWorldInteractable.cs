using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerWorldInteractable : MonoBehaviour
{
        protected virtual void OnPlayerTouchAsButton() { }

    protected virtual void OnPlayerTouchEnter() { }

    protected virtual void OnPlayerTouchDrag(Vector2 deltaDrag) { }
    
    private bool isDragging = false;
    private Vector2 lastMousePosition;
    
    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            lastMousePosition = Input.mousePosition;
            isDragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
        if (isDragging)
        {
            Vector2 currentMousePosition = Input.mousePosition;
            Vector2 deltaDrag = currentMousePosition - lastMousePosition;
            OnPlayerTouchDrag(deltaDrag);
            lastMousePosition = currentMousePosition;
        }
#else
        if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                lastMousePosition = touch.position;
                isDragging = true;
            }
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
            if (isDragging && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
            {
                Vector2 currentTouchPosition = touch.position;
                Vector2 deltaDrag = currentTouchPosition - lastMousePosition;
                OnPlayerTouchDrag(deltaDrag);
                lastMousePosition = currentTouchPosition;
            }
        }
#endif
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
