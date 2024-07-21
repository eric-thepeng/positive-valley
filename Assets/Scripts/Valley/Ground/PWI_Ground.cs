using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PWI_Ground : PlayerWorldInteractable
{
    protected override void OnPlayerTouchDrag(Vector2 deltaDrag)
    {
        base.OnPlayerTouchDrag(deltaDrag);
        CameraMovement.i.MoveCamera(deltaDrag);
    }
}