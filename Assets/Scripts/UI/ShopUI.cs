using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class ShopUI : MonoBehaviour
{

    [SerializeField] private Image img_SpecialItemPlayer;
    [SerializeField] private RectTransform scrollviewContext;
    [SerializeField] private float flt_ScrollAnimationDuration = 0.2f;
    public GameObject specialItemSection;

    [Space]
    [Header("Special Item Reward")]
    public int playerIndex;
    public int specialItemGemsReward;
    public int specialItemCoinReward;
    [SerializeField] private TextMeshProUGUI txt_GemsAmount;
    [SerializeField] private TextMeshProUGUI txt_CoinAmount;
    public Image[] all_SpecialItemRewardIcons;
    public List<Sprite> list_SpecialItemIcons;
    public List<int> list_SpecialItemRewardAmount;


    [Space]
    [Header("Coins Section")]
    public int[] all_CoinsAmount;
    [SerializeField] private Image[] all_CoinsIcons;
    [SerializeField] private TextMeshProUGUI[] all_RewaredCoinAmount;
    [SerializeField] private TextMeshProUGUI[] all_RequiredResourceForReward;

    [Space]
    [Header("Gems Section")]
    public int[] all_GemsAmount;
    [SerializeField] private Image[] all_GemsIcons;
    [SerializeField] private TextMeshProUGUI[] all_RewardGemsAmount;


    [Space]
    [Header("Energy Section")]
    public int[] all_EnergyAmount;
    [SerializeField] private Image[] all_EnergiesIcons;
    [SerializeField] private TextMeshProUGUI[] all_RewardEnergiesAmount;
    [SerializeField] private TextMeshProUGUI[] all_RequiredResourceForRewardEnergy;


    private void OnEnable()
    {
        specialItemSection.SetActive(true);
        if (DataManager.Instance.isSpecialItemPurchase)
        {
            specialItemSection.SetActive(false);
        }


        SetAllrewardAmountToArray();
    }




    private void Start()
    {
        img_SpecialItemPlayer.sprite = PlayerDataManager.Instance.all_CharchterData[playerIndex].playerIcon;

        


        for (int i = 0; i < all_SpecialItemRewardIcons.Length; i++)
        {
            list_SpecialItemIcons.Add(all_SpecialItemRewardIcons[i].sprite);
        }
        list_SpecialItemRewardAmount.Add(specialItemGemsReward);
        list_SpecialItemRewardAmount.Add(specialItemCoinReward);
    }


    private void OnDisable()
    {
        scrollviewContext.anchoredPosition = Vector2.zero;
    }


    private void SetAllrewardAmountToArray()
    {
        //Set Coins Reward text
        for (int i = 0; i < all_RewaredCoinAmount.Length; i++)
        {
            all_RewaredCoinAmount[i].text = all_CoinsAmount[i].ToString();
        }
        for (int i = 0; i < all_RewardGemsAmount.Length; i++)
        {
            all_RewardGemsAmount[i].text = all_GemsAmount[i].ToString();
        }
        for (int i = 0; i < all_RewardEnergiesAmount.Length; i++)
        {
            all_RewardEnergiesAmount[i].text = all_EnergyAmount[i].ToString();
        }
    }

    public void ScrollDownAnimation(Vector2 position)
    {
        if (this.gameObject.activeInHierarchy)
        {
            scrollviewContext.DOAnchorPos(position, flt_ScrollAnimationDuration);
        }
    }


    public void OnClick_PurchaseCoins(int index)
    {
        if(index == 0)
        {
            AdsManager.Instance.rewarsState = RewardState.coinReward;
            AdsManager.Instance.ShowRewardedAd();
        }
        else
        {
            if (int.Parse(all_RequiredResourceForReward[index].text) >= DataManager.Instance.totalGems)
            {
                UIManager.Instance.SpawnPopUpBox("Not Enough Gems");
                return;
            }

            UIManager.Instance.ui_RewardSummary.SetRewardSummarySingleData(all_CoinsIcons[index].sprite, int.Parse(all_RewaredCoinAmount[index].text));
            UIManager.Instance.ui_RewardSummary.gameObject.SetActive(true);
            DataManager.Instance.SubstractGames(int.Parse(all_RequiredResourceForReward[index].text));
            DataManager.Instance.IncreaseCoins(int.Parse(all_RewaredCoinAmount[index].text));
        }

      
    }

    public void OnClick_PurchaseEnergy(int index)
    {

        if(index == 0)
        {
            AdsManager.Instance.rewarsState = RewardState.energyReward;
            AdsManager.Instance.ShowRewardedAd();
        }
        else
        {
            if (int.Parse(all_RequiredResourceForRewardEnergy[index].text) >= DataManager.Instance.totalGems)
            {
                UIManager.Instance.SpawnPopUpBox("Not Enough Gems");
                return;
            }

            UIManager.Instance.ui_RewardSummary.SetRewardSummarySingleData(all_EnergiesIcons[index].sprite, int.Parse(all_RewardEnergiesAmount[index].text));
            UIManager.Instance.ui_RewardSummary.gameObject.SetActive(true);
            DataManager.Instance.SubstractGames(int.Parse(all_RequiredResourceForRewardEnergy[index].text));
            DataManager.Instance.IncreaseEnergy(int.Parse(all_RewardEnergiesAmount[index].text));
        }

       
    }

    public void OnClick_PurchaseGems(int index)
    {
        UIManager.Instance.ui_RewardSummary.SetRewardSummarySingleData(all_GemsIcons[index].sprite, int.Parse(all_RewardGemsAmount[index].text));
        UIManager.Instance.ui_RewardSummary.gameObject.SetActive(true);
    }

    public void OnClick_BuyProduct(int index)
    {
        FindObjectOfType<IAPManager>().BuyConsumable(index);
    }
}
