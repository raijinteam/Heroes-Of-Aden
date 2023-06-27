using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PassiveUpgradeSummaryUI : MonoBehaviour
{

    [SerializeField] private Image img_UpgradeIcon;
    [SerializeField] private TextMeshProUGUI txt_UpgradeName;
    [SerializeField] private TextMeshProUGUI txt_UpgradeLevel;
    [SerializeField] private TextMeshProUGUI txt_UpgradeDescription;
    [SerializeField] private TextMeshProUGUI txt_UpgradeCurrentValue;
    [SerializeField] private TextMeshProUGUI txt_UpgradedValue;

    private void OnEnable()
    {
        UIManager.Instance.canChangeMenus = true;
    }

    public void SetPassiveUpgradeRewardInfo(int _selectedUpgradeIndex)
    {
        img_UpgradeIcon.sprite = UIManager.Instance.ui_PassiveUpgrade.ui_PassiveUpgradeSelection.all_UpgradeIcons[_selectedUpgradeIndex].sprite;
        PassiveUpgradeManager.Instance.IncreasePassiveUpgradeCurrentLevel(_selectedUpgradeIndex);
        txt_UpgradeLevel.text = "Level " + PassiveUpgradeManager.Instance.GetCurrentPassivesLevel(_selectedUpgradeIndex).ToString();
        txt_UpgradeCurrentValue.text = PassiveUpgradeManager.Instance.GetCurrentUpgradeValue(_selectedUpgradeIndex).ToString() + "%";
        txt_UpgradedValue.text = PassiveUpgradeManager.Instance.GetUpgradedValue(_selectedUpgradeIndex).ToString() + "%";
        txt_UpgradeName.text = PassiveUpgradeManager.Instance.GetCurrentPassiveUpgradeName(_selectedUpgradeIndex);
        txt_UpgradeDescription.text = PassiveUpgradeManager.Instance.GetCurrentPassiveUpgradeDesc(_selectedUpgradeIndex);
    }


    public void OnClick_Continue()
    {
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
