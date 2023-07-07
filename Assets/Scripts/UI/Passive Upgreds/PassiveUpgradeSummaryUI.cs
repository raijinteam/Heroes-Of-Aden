using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PassiveUpgradeSummaryUI : MonoBehaviour
{

    [SerializeField] private Image img_UpgradeIcon;
    [SerializeField] private Image img_UpgradeIconBG;
    [SerializeField] private TextMeshProUGUI txt_UpgradeName;
    [SerializeField] private TextMeshProUGUI txt_UpgradeLevel;
    [SerializeField] private TextMeshProUGUI txt_UpgradeCurrentValue;
    [SerializeField] private TextMeshProUGUI txt_UpgradedValue;
    [SerializeField] private Button btn_Continue;


    [SerializeField] private float flt_ScaleUpAnimationDuration;

    private void OnEnable()
    {
        UIManager.Instance.canChangeMenus = true;
        StartAnimation();
    }

    private void OnDisable()
    {
        ResetAnimation();
    }

    private void StartAnimation()
    {
        Sequence seq = DOTween.Sequence();


        seq.Append(img_UpgradeIconBG.transform.DOScale(Vector3.one , flt_ScaleUpAnimationDuration)).
            Append(txt_UpgradeName.transform.DOScale(Vector3.one , flt_ScaleUpAnimationDuration)).
            Append(txt_UpgradeLevel.transform.DOScale(Vector3.one, flt_ScaleUpAnimationDuration)).
            Append(txt_UpgradeCurrentValue.transform.DOScale(Vector3.one, flt_ScaleUpAnimationDuration)).
            Append(txt_UpgradedValue.transform.DOScale(Vector3.one, flt_ScaleUpAnimationDuration)).
            Append(btn_Continue.transform.DOScale(Vector3.one, flt_ScaleUpAnimationDuration));
    }


    private void ResetAnimation()
    {
        img_UpgradeIconBG.transform.DOScale(Vector3.zero, 0.1f);
        txt_UpgradeName.transform.DOScale(Vector3.zero, .1f);
        txt_UpgradeLevel.transform.DOScale(Vector3.zero, .1f);
        txt_UpgradeCurrentValue.transform.DOScale(Vector3.zero, .1f);
        txt_UpgradedValue.transform.DOScale(Vector3.zero, .1f);
        btn_Continue.transform.DOScale(Vector3.zero, 0.1f);
    }

    public void SetPassiveUpgradeRewardInfo(int _selectedUpgradeIndex)
    {
        img_UpgradeIcon.sprite = UIManager.Instance.ui_PassiveUpgrade.ui_PassiveUpgradeSelection.all_UpgradeIcons[_selectedUpgradeIndex].sprite;
        PassiveUpgradeManager.Instance.IncreasePassiveUpgradeCurrentLevel(_selectedUpgradeIndex);
        txt_UpgradeLevel.text = "Level " + PassiveUpgradeManager.Instance.GetCurrentPassivesLevel(_selectedUpgradeIndex).ToString();
        txt_UpgradeCurrentValue.text = "<color=white> Value  </color>" + PassiveUpgradeManager.Instance.GetCurrentUpgradeValue(_selectedUpgradeIndex).ToString() + "%";
        txt_UpgradedValue.text = "<color=white>>>  </color>" + PassiveUpgradeManager.Instance.GetUpgradedValue(_selectedUpgradeIndex).ToString() + "%";
        txt_UpgradeName.text = PassiveUpgradeManager.Instance.GetCurrentPassiveUpgradeName(_selectedUpgradeIndex);
        //txt_UpgradeDescription.text = PassiveUpgradeManager.Instance.GetCurrentPassiveUpgradeDesc(_selectedUpgradeIndex);
    }


    public void OnClick_Continue()
    {

        ServiceManager.Instance.soundManager.PlayButtonClickSound();

        for (int i = 0; i < UIManager.Instance.ui_PassiveUpgrade.ui_PassiveUpgradeSelection.all_UpgradeGlowBG.Length; i++)
        {
            UIManager.Instance.ui_PassiveUpgrade.ui_PassiveUpgradeSelection.all_UpgradeGlowBG[i].gameObject.SetActive(false);
        }
        UIManager.Instance.ui_PassiveUpgrade.ui_PassiveUpgradeSelection.SetPassiveUpgradeLevelText();
        UIManager.Instance.ui_PassiveUpgrade.ui_PassiveUpgradeSelection.gameObject.SetActive(true);
        UIManager.Instance.canChangeMenus = true;
        UIManager.Instance.ui_PassiveUpgrade.btn_Upgrade.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
