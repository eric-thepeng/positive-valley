using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PT_DrinkWaterDisplayer : MonoBehaviour
{
    [SerializeField] private List<Image> drinkCountFillingBlocks;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color drinkColor;
    [SerializeField] private Vector3 defaultScale;
    [SerializeField] private Vector3 drinkScale;
    [SerializeField] private float transitionTime;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="currentStateInfo"></param>
    /// <param name="animationAtBlockIndex">Which block to play animation, index start with 0. Leave as -1 if no animation needed.</param>
    public void Display(PT_DrinkWater.CurrentState currentStateInfo, int animationAtBlockIndex = -1)
    {
        RestoreToDefault();
        for (int i = 0; i < currentStateInfo.currentDayCount; i++)
        {
            if (i == animationAtBlockIndex)
            {
                StartCoroutine(BlockTransitionEffect(animationAtBlockIndex));
            }
            else
            {
                drinkCountFillingBlocks[i].transform.localScale = drinkScale;
                drinkCountFillingBlocks[i].color = drinkColor;
            }
        }
    }

    public void RestoreToDefault()
    {
        foreach (var VARIABLE in drinkCountFillingBlocks)
        {
            VARIABLE.transform.localScale = defaultScale;
            VARIABLE.color = defaultColor;
        }
    }

    public IEnumerator BlockTransitionEffect(int index)
    {
        Image tarGO = drinkCountFillingBlocks[index];

        tarGO.DOColor(drinkColor, transitionTime);
        
        tarGO.transform.DOScale(drinkScale * 1.2f, transitionTime/4);
        yield return new WaitForSeconds(transitionTime / 4);
        
        tarGO.transform.DOScale(drinkScale * .9f, transitionTime/4);
        yield return new WaitForSeconds(transitionTime / 4);
        
        tarGO.transform.DOScale(drinkScale * 1.1f, transitionTime/4);
        yield return new WaitForSeconds(transitionTime / 4);
        
        tarGO.transform.DOScale(drinkScale * 1f, transitionTime/4);
    }
}
