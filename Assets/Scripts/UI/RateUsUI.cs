using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RateUsUI : MonoBehaviour
{
    [SerializeField] private Image[] all_Stars;

    [SerializeField] private Sprite sprite_YellowStar;
    [SerializeField] private Sprite sprite_WhiteStar;


    private void OnEnable()
    {
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        for(int i = 0; i < all_Stars.Length; i++)
        {
            all_Stars[i].sprite = sprite_YellowStar;
            yield return new WaitForSeconds(0.5f);

            if(i == all_Stars.Length - 1)
            {
                SetAllStartWhite();
            }
        }
    }

    private void SetAllStartWhite()
    {
        for(int i = 0; i < all_Stars.Length; i++)
        {
            all_Stars[i].sprite = sprite_WhiteStar;
        }

        StartCoroutine(StartAnimation());
    }

    public void OnClick_Continue()
    {
        this.gameObject.SetActive(false);
        DataManager.Instance.HideRateUSBox();
        DataManager.Instance.CheckForRateusShow();
    }

    public void OnClick_Close()
    {
        this.gameObject.SetActive(false);
        DataManager.Instance.gameCountForShowRateusBox = 0;
        DataManager.Instance.IncreaseRateusGameCount();
    }
}
