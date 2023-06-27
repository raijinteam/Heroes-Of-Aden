using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PassiveUpgradeSelectonUI : MonoBehaviour
{
    [SerializeField] private Button btn_Upgrade;

    public Image[] all_UpgradeGlowBG;
    public Image[] all_UpgradeIcons;
    public TextMeshProUGUI[] all_PassiveUpgradeLevel;

    public int selectedPowerUpIndex;
    [SerializeField] private float flt_UpgradeButtonAnimationDuration = 0.5f; // ANIMATION DURATION FOR UPGRADE BUTTON
    [SerializeField] private float flt_PassiveUpgradeRewardedAnimationDuration = .2f; // ANIMATION TIME FOR REWARD
    [SerializeField] private List<int> list_MaxLevelPassiveUpgrade; // LIST FOR UPGRADE REACH MAX LEVEL



    private void OnEnable()
    {
        for (int i = 0; i < all_UpgradeGlowBG.Length; i++)
        {
            all_UpgradeGlowBG[i].gameObject.SetActive(false);
        }

        SetPassiveUpgradeLevelText();

        CheckForUpgradeMaxLevel();

        //ALL UPGRADE REACH MAX LEVEL 
        if (list_MaxLevelPassiveUpgrade.Count == all_UpgradeGlowBG.Length)
        {
            btn_Upgrade.interactable = false;
        }

    }


    //RANDOM SELECTION EFFECT 
    private IEnumerator SetRandomPassiveUpgradeSelectionEffect()
    {
        int randomLoopCount = Random.Range(10, 15);

        int previousIndex = 0;

        UIManager.Instance.canChangeMenus = false;
        
        for (int i = 0; i < randomLoopCount; i++)
        {
            selectedPowerUpIndex = Random.Range(0, all_UpgradeGlowBG.Length);
            //DO NOT SELECT WHEN UPGRADE REACH MAX LEVEL
            if (list_MaxLevelPassiveUpgrade.Contains(selectedPowerUpIndex))
            {
                selectedPowerUpIndex++;
                continue;
            }
            else
            {
                all_UpgradeGlowBG[previousIndex].gameObject.SetActive(false);
                all_UpgradeGlowBG[selectedPowerUpIndex].gameObject.SetActive(true);
            }

            previousIndex = selectedPowerUpIndex;
            yield return new WaitForSeconds(0.1f);
        }
        //SET DATA IN INFO PANEL
        UIManager.Instance.ui_PassiveUpgrade.ui_PassiveUpgradeSummary.SetPassiveUpgradeRewardInfo(selectedPowerUpIndex);
        list_MaxLevelPassiveUpgrade.Clear();
        Sequence seq = DOTween.Sequence();
        seq.Append(all_UpgradeGlowBG[selectedPowerUpIndex].transform.DOScale(1.2f, flt_PassiveUpgradeRewardedAnimationDuration).SetEase(Ease.Linear))
                    .Append(all_UpgradeGlowBG[selectedPowerUpIndex].transform.DOScale(1, flt_PassiveUpgradeRewardedAnimationDuration).SetEase(Ease.Linear)).SetLoops(5).OnComplete(ShowRewardPanel);
    }

    //CHECK IF PASSIVE UPGRADE IS UP MAX PASSIVE LEVEL THEN SKIP THAT UPGRADE
    private void CheckForUpgradeMaxLevel()
    {
        for (int i = 0; i < all_UpgradeGlowBG.Length; i++)
        {
            //IF UPGRADE LEVEL REACH MAX LEVEL THEN ADD THIS LIST
            if (PassiveUpgradeManager.Instance.GetCurrentPassivesLevel(i) >= PassiveUpgradeManager.Instance.maxPassiveUpgradeLevel)
            {
                list_MaxLevelPassiveUpgrade.Add(i);
            }
        }

    }

    //SET LEVEL TEXT IN PASSIVE UPGRADE MENU 
    public void SetPassiveUpgradeLevelText()
    {
        for (int i = 0; i < all_PassiveUpgradeLevel.Length; i++)
        {
            all_PassiveUpgradeLevel[i].text = PlayerPrefs.GetInt(PlayerPrefsData.KEY_UPGRADE_LEVEL + i).ToString();
        }
    }

    //ACTIVE REWARDED PANEL
    private void ShowRewardPanel()
    {
        UIManager.Instance.canChangeMenus = false;
        this.gameObject.SetActive(false);
        UIManager.Instance.ui_PassiveUpgrade.btn_Upgrade.SetActive(false);
        UIManager.Instance.ui_PassiveUpgrade.ui_PassiveUpgradeSummary.gameObject.SetActive(true);
        btn_Upgrade.interactable = true;
    }


    public void OnClick_UpgradeSelectionStart()
    {

        if (!PassiveUpgradeManager.Instance.hasEnoughCoinsForUpgrade())
        {
            UIManager.Instance.SpawnPopUpBox("Not Enough Coins");
            return;
        }
        btn_Upgrade.interactable = false;

        StartCoroutine(SetRandomPassiveUpgradeSelectionEffect());
        DataManager.Instance.SubstractCoins(PassiveUpgradeManager.Instance.coinsForUpgrade);
        Sequence seq = DOTween.Sequence();
        seq.Append(btn_Upgrade.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), flt_UpgradeButtonAnimationDuration).SetEase(Ease.OutSine)).
                    Append(btn_Upgrade.transform.DOScale(new Vector3(1f, 1f, 1f), flt_UpgradeButtonAnimationDuration).SetEase(Ease.InSine));
    }

}
