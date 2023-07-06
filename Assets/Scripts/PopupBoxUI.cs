using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PopupBoxUI : MonoBehaviour
{
    public TextMeshProUGUI txt_Value;
    public float flt_TimeForShow = 1f;
    public float flt_TimeForAnimation = 0.2f;


    private float screenWidth;
    private float screenHeight;

    private void OnEnable()
    {
        StartCoroutine(PopUpBoxShow());
    }

    public IEnumerator PopUpBoxShow()
    {
        screenHeight = Screen.height;

        this.gameObject.transform.DOMoveY(screenHeight / 2, flt_TimeForAnimation);
        yield return new WaitForSeconds(flt_TimeForShow);
        this.gameObject.transform.DOMoveY(screenHeight * 2, flt_TimeForAnimation);
        UIManager.Instance.canPopupBoxSpawn = true;
        Destroy(gameObject , 0.5f);
    }
}
