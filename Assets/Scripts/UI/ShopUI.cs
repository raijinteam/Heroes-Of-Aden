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

    private void OnEnable()
    {
        img_SpecialItemPlayer.sprite = PlayerDataManager.Instance.all_CharchterData[playerIndex].playerIcon;

        specialItemSection.SetActive(true);
        if (DataManager.Instance.isSpecialItemPurchase)
        {
            specialItemSection.SetActive(false);
        }
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

    public void OnClick_BuyProduct(int index)
    {
        FindObjectOfType<IAPManager>().BuyConsumable(index);

    }
}
