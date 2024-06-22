using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanelSwitchButtonsManager : MonoBehaviour
{
    public void SwitchPanelToValley(){GamePanelsManager.i.SwitchPanelTo(GamePanelsManager.GamePanel.Valley);}
    public void SwitchPanelToBarn(){GamePanelsManager.i.SwitchPanelTo(GamePanelsManager.GamePanel.Barn);}
}
