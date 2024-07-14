using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerWorldInteractable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    protected virtual void OnPlayerTouchAsButton() { }

    protected virtual void OnPlayerTouchEnter() { }
    
    // HANDLING CLICK
    private void OnMouseUpAsButton()
    {
        if(!EventSystem.current.IsPointerOverGameObject()) OnPlayerTouchAsButton();
    }

    private void OnMouseEnter()
    {
        if(!EventSystem.current.IsPointerOverGameObject()) OnPlayerTouchEnter();
    }
    
    // HANDLING TOUCH
    public void OnPointerClick(PointerEventData eventData)
    {
        // 检查是否点击在UI元素上
        if (EventSystem.current.IsPointerOverGameObject(eventData.pointerId))
        {
            return;
        }

        // 处理点击事件
        OnPlayerTouchAsButton();
    }

    // 处理鼠标进入事件
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 检查是否进入在UI元素上
        if (EventSystem.current.IsPointerOverGameObject(eventData.pointerId))
        {
            return;
        }

        // 处理鼠标进入事件
        OnPlayerTouchEnter();
    }
}
