using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BroadcastStat<T>
{
    protected T value;
    protected UnityEvent<T> OnAmountChangeDelta = new UnityEvent<T>();
    protected UnityEvent<T> OnAmountChangeValue = new UnityEvent<T>();
    
    public BroadcastStat(T newValue)
    {
        value = newValue;
    }
    
    public T GetValue()
    {
        return value;
    }
    
    public void SubscribeChangeDelta(UnityAction<T> unityAction)
    {
        OnAmountChangeDelta.AddListener(unityAction);
    }

    public void UnsubscribeChangeDelta(UnityAction<T> unityAction)
    {
        OnAmountChangeDelta.RemoveListener(unityAction);
    }

    public void SubscribeChangeValue(UnityAction<T> unityAction)
    {
        OnAmountChangeValue.AddListener(unityAction);
    }

    public void UnsubscribeChangeValue(UnityAction<T> unityAction)
    {
        OnAmountChangeValue.RemoveListener(unityAction);
    }

    //Override this
    public virtual T ChangeValue(T delta)
    {
        throw new Exception("Change Value Not Implemented");
    }

    //Override this
    public virtual T SetValue(T newValue)
    {
        throw new Exception("Set Value Not Implemented");
    }
    
}

public class BroadcastStatInt : BroadcastStat<int>
{
    public BroadcastStatInt(int newValue) : base(newValue)
    {
        
    }

    public BroadcastStatInt() : base(0)
    {
        
    }

    public override int SetValue(int newValue)
    {
        OnAmountChangeDelta.Invoke(newValue - value);
        OnAmountChangeValue.Invoke(newValue);
        value = newValue;
        return value;
    }

    public override int ChangeValue(int delta)
    {
        OnAmountChangeDelta.Invoke(delta);
        OnAmountChangeValue.Invoke(value + delta);
        value += delta;
        return value;
    }
}
