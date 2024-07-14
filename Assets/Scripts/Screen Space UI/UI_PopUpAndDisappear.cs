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
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image backgroundFrameImage;
    

    public void SetUpAndDisplay(Sprite displaySprite, string descriptionString, float stayTime, float disappearTime, bool punchScale = true, bool moveUpWhenFadeOut = true)
    {
        gameObject.SetActive(true);
        descriptionTMPT.text = descriptionString;
        displayImage.sprite = displaySprite;
        StartCoroutine(Disappear(stayTime, disappearTime, punchScale, moveUpWhenFadeOut));
    }

    public void ExitUI()
    {
        gameObject.SetActive(false);
    }

    IEnumerator Disappear(float stayTime, float disappearTime, bool punchScale, bool moveUpWhenFadeOut )
    {
        if(punchScale)transform.DOPunchScale(new Vector3(0.05f,0.05f,0.05f), stayTime * 0.9f);
        yield return new WaitForSeconds(stayTime);
        if(moveUpWhenFadeOut)transform.DOLocalMoveY(300, disappearTime);
        canvasGroup.DOFade(0, disappearTime);
        yield return new WaitForSeconds(disappearTime);
        Destroy(gameObject);
    }

    public void SetBackgroundFrameColor(Color newColor)
    {
        backgroundFrameImage.color = newColor;
    }

}