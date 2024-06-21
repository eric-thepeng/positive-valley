using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PopUpAndDisappear : MonoBehaviour
{
    [SerializeField] private Image displayImage;
    [SerializeField] private TMP_Text descriptionTMPT;
    [SerializeField] private CanvasGroup canvasGroup;
    

    public void SetUpAndDisplay(Sprite displaySprite, string descriptionString, float stayTime, float disappearTime)
    {
        gameObject.SetActive(true);
        descriptionTMPT.text = descriptionString;
        displayImage.sprite = displaySprite;
        StartCoroutine(Disappear(stayTime, disappearTime));
    }
    
    public void ExitUI()
    {
        gameObject.SetActive(false);
    }

    IEnumerator Disappear(float stayTime, float disappearTime)
    {
        yield return new WaitForSeconds(stayTime);
        transform.DOLocalMoveY(300, disappearTime);
        canvasGroup.DOFade(0, disappearTime);
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
    }

}