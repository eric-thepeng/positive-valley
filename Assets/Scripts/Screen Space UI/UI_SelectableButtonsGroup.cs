using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_SelectableButtonsGroup : MonoBehaviour
{
    public List<UI_SelectableButton> allButtons = new List<UI_SelectableButton>();
    public List<UI_SelectableButton> selectedButtons = new List<UI_SelectableButton>();
    public UnityEvent<List<UI_SelectableButton>> OnSelectedButtonsChanges = new UnityEvent<List<UI_SelectableButton>>();
    
    public void Register(UI_SelectableButton selectableButton)
    {
        allButtons.Add(selectableButton);
        selectableButton.OnSelected.AddListener(OnButtonSelected);
        selectableButton.OnSelected.AddListener(OnButtonSelected);
    }
    
    public void Unregister(UI_SelectableButton selectableButton)
    {
        allButtons.Remove(selectableButton);
        selectableButton.OnSelected.RemoveListener(OnButtonSelected);
        selectableButton.OnSelected.RemoveListener(OnButtonSelected);
    }

    public void OnButtonSelected(UI_SelectableButton button)
    {
        selectedButtons.Add(button);
        OnSelectedButtonsChanges.Invoke(selectedButtons);        
    }

    public void OnButtonUnselected(UI_SelectableButton button)
    {
        selectedButtons.Remove(button);
        OnSelectedButtonsChanges.Invoke(selectedButtons);
    }
}
