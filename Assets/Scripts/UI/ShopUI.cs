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
    [SerializeField] private Image[] all_CoinsIcons;
    [SerializeField] private TextMeshProUGUI[] all_CoinsAmount;


    [Space]
    [Header("Gems Section")]
    [SerializeField] private Image[] all_GemsIcons;
    [SerializeField] private TextMeshProUGUI[] all_GemsAmount;


    [Space]
    [Header("Energy Section")]
    [SerializeField] private Image[] all_EnergiesIcons;
    [SerializeField] private TextMeshProUGUI[] all_EnergiesAmount;


    private void OnEnable()
    {
        specialItemSection.SetActive(true);
        if (DataManager.Instance.isSpecialItemPurchase)
        {
            specialItemSection.SetActive(false);
        }
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


    public void ScrollDownAnimation(Vector2 position)
    {
        if (this.gameObject.activeInHierarchy)
        {
            scrollviewContext.DOAnchorPos(position, flt_ScrollAnimationDuration);
        }
    }


    public void OnClick_PurchaseCoins(int index)
    {
        UIManager.Instance.ui_RewardSummary.SetRewardSummarySingleData(all_CoinsIcons[index].sprite, int.Parse( all_CoinsAmount[index].text));
        UIManager.Instance.ui_RewardSummary.gameObject.SetActive(true);
        DataManager.Instance.IncreaseCoins(int.Parse(all_CoinsAmount[index].text));
    }

    public void OnClick_PurchaseEnergy(int index)
    {
        UIManager.Instance.ui_RewardSummary.SetRewardSummarySingleData(all_EnergiesIcons[index].sprite, int.Parse(all_EnergiesAmount[index].text));
        UIManager.Instance.ui_RewardSummary.gameObject.SetActive(true);
        DataManager.Instance.IncreaseEnergy(int.Parse(all_EnergiesAmount[index].text));
    }

    public void OnClick_PurchaseGems(int index)
    {
        UIManager.Instance.ui_RewardSummary.SetRewardSummarySingleData(all_GemsIcons[index].sprite, int.Parse(all_GemsAmount[index].text));
        UIManager.Instance.ui_RewardSummary.gameObject.SetActive(true);
    }

    public void OnClick_BuyProduct(int index)
    {
        FindObjectOfType<IAPManager>().BuyConsumable(index);
    }
}
