using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ICollectionItem
{
    public int GetUnlockLevel();
    public BarnItemSet GetUnlockBarnItemSet();
}
