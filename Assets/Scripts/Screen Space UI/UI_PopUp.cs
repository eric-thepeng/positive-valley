using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PopUp : MonoBehaviour
{
    [SerializeField] private TMP_Text descriptionTMPT;
    
    public void SetUpAndDisplay(string descriptionString)
    {
        gameObject.SetActive(true);
        descriptionTMPT.text = descriptionString;
    }
    
    public void ExitUI()
    {
        gameObject.SetActive(false);
    }

}
