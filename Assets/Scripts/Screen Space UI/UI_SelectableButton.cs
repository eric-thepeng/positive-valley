using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UI_SelectableButton : MonoBehaviour
{
    private Button button;
    public UnityEvent OnSelected = new UnityEvent();
    public UnityEvent OnUnselected = new UnityEvent();
    private bool selected = false;
    private bool interactable = true;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }

    public void ButtonClicked()
    {
        if(!interactable) return;
        if(selected)
        {
            selected = false;
            OnUnselected.Invoke();
            button.targetGraphic.DOColor(button.colors.normalColor,0.1f);
        }
        else
        {
            selected = true;
            OnSelected.Invoke();
            button.targetGraphic.DOColor(button.colors.selectedColor,0.1f);
        }
    }

    public bool IsSelected()
    {
        return selected;
    }

    public bool IsInteractable()
    {
        return interactable;
    }

    public void SetInteractable(bool interactable)
    {
        this.interactable = interactable;
    }

}
