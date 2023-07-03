using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SpecialOfferUI : MonoBehaviour
{
    
    [SerializeField] private Image img_SpecialItemPlayer;
    [SerializeField] private TextMeshProUGUI txt_FirstItem;
    [SerializeField] private TextMeshProUGUI txt_SecondItem;

    private void OnEnable()
    {
        img_SpecialItemPlayer.sprite = PlayerDataManager.Instance.all_CharchterData[UIManager.Instance.ui_Shop.playerIndex].playerIcon;
        txt_FirstItem.text = "x" + UIManager.Instance.ui_Shop.specialItemGemsReward.ToString();
        txt_SecondItem.text = "x" + UIManager.Instance.ui_Shop.specialItemCoinReward.ToString();
    }

    public void OnClick_BuyItem()
    {
        IAPManager.Instance.BuyConsumable(1);
    }

    public void OnClick_Close()
    {
        this.gameObject.SetActive(false);
        DataManager.Instance.gameCountForShowSpecialItem = 0; 
    }
}
