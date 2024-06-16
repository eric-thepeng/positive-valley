using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class ClickCancelGround : PlayerWorldInteractable, IClickCancel
{
    // SINGLETON
    static ClickCancelGround instance;
    public static ClickCancelGround i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<ClickCancelGround>();
            }
            return instance;
        }
    }
    // ----------
    
    public UnityEvent OnPlayerClickCancel = new UnityEvent();
    protected override void OnPlayerTouchAsButton()
    {
        print("Player Cancel");

        OnPlayerClickCancel.Invoke();
    }

    public UnityEvent GetClickCancelEvent()
    {
        return OnPlayerClickCancel;
    }
}

public interface IClickCancel
{
    public UnityEvent GetClickCancelEvent();
}

public class ClickCancelAction
{
    private UnityAction assignedCancelAction;
    private IClickCancel iClickCancel;
    
    public ClickCancelAction(IClickCancel tarClickCancel, UnityAction cancelAction)
    {
        assignedCancelAction = cancelAction + Unsubscribe;
        iClickCancel = tarClickCancel;
        tarClickCancel.GetClickCancelEvent().AddListener(assignedCancelAction);
    }

    private void Unsubscribe()
    {
        iClickCancel.GetClickCancelEvent().RemoveListener(assignedCancelAction);
    }

    public void Cancel()
    {
        iClickCancel.GetClickCancelEvent().RemoveListener(assignedCancelAction);
    }
}
