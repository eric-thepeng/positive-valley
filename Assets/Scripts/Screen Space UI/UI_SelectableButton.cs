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
    public UnityEvent<UI_SelectableButton> OnSelected = new UnityEvent<UI_SelectableButton>();
    public UnityEvent<UI_SelectableButton> OnUnselected = new UnityEvent<UI_SelectableButton>();
    
    [SerializeField] private UI_SelectableButtonsGroup group;
    
    private bool selected = false;
    private bool interactable = true;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonClicked);
    }

    private void OnEnable()
    {
        group?.Register(this);
    }

    private void OnDisable()
    {
        group?.Unregister(this);
    }

    public void ButtonClicked()
    {
        if(!interactable) return;
        if(selected)
        {
            selected = false;
            OnUnselected.Invoke(this);
            button.targetGraphic.DOColor(button.colors.normalColor,0.1f);
        }
        else
        {
            selected = true;
            OnSelected.Invoke(this);
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
