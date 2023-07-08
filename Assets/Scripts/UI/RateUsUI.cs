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
        ServiceManager.Instance.soundManager.PlayButtonClickSound();

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.armageddonstudio.heroesofadenn");

        this.gameObject.SetActive(false);
        ServiceManager.Instance.dataManager.HideRateUSBox();
        ServiceManager.Instance.dataManager.CheckForRateusShow();
    }

    public void OnClick_Close()
    {
        ServiceManager.Instance.soundManager.PlayButtonClickSound();

        this.gameObject.SetActive(false);
        ServiceManager.Instance.dataManager.gameCountForShowRateusBox = 0;
        ServiceManager.Instance.dataManager.IncreaseRateusGameCount();
    }
}
