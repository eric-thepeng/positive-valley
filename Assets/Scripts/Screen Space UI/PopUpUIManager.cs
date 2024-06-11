using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopUpUIManager : MonoBehaviour
{
    // SINGLETON
    static PopUpUIManager instance;
    public static PopUpUIManager i
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PopUpUIManager>();
            }
            return instance;
        }
    }
    // ----------

    // SERIALIZED PRIVATE VARIABLES
    [SerializeField]private UI_PopUpAndSelect UnlockFieldPopUpUI;

    public void DisplayUnlockFieldPopUp(Field tarField)
    {
        UnlockFieldPopUpUI.SetUpAndDisplay("It takes $$" + tarField.GetUnlockCost() + " to unlock.", 
            new List<UnityAction>(){tarField.TryToUnlock}, 
            new List<string>(){"Unlock"});
    }

    public void ExitUnlockFieldPopUp()
    {
        UnlockFieldPopUpUI.ExitUI();
    }
}
