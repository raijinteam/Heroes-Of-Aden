using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingFadeBGUI : MonoBehaviour
{

    public Image img_FadeBG;
    [SerializeField] private float flt_AnimationDuration = 0.5f;


    public void StartFadeAnimation()
    {
        img_FadeBG.DOFade(1, flt_AnimationDuration);
    }

}
