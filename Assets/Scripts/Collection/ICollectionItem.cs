using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectionItem
{
    public int GetUnlockLevel();
    public BarnItemSet GetUnlockBarnItemSet();
}
