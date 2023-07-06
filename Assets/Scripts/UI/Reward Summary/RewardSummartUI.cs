using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;



public class RewardSummartUI : MonoBehaviour
{

    [SerializeField] private RewardSummaryData[] all_Rewards;

    [SerializeField] private float flt_ScaleDuration = 0.3f;


    private void OnDisable()
    {
        for(int i = 0; i < all_Rewards.Length; i++)
        {
            all_Rewards[i].transform.DOScale(0, 0.1f);
        }
    }

    public void SetRewardSummarySingleData(Sprite _rewardIcon , int _rewardAmount)
    {
        all_Rewards[0].gameObject.SetActive(true);
        all_Rewards[0].img_RewardIcon.sprite = _rewardIcon;
        all_Rewards[0].txt_RewardAmount.text = "x"+_rewardAmount.ToString();
        all_Rewards[0].transform.DOScale(1, flt_ScaleDuration);
    }

    public void SetRewardSummaryData(List<Sprite> _rewardIcons , List<int> _rewardAmounts)
    {

        this.gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence();

        /*seq.Append(all_Rewards[0].transform.DOScale(1, flt_ScaleDuration)).
            Append(all_Rewards[1].transform.DOScale(1, flt_ScaleDuration)).
            Append(all_Rewards[2].transform.DOScale(1, flt_ScaleDuration));
*/
        for (int i = 0; i < _rewardIcons.Count; i++)
        {
            Debug.Log("In For Loop");
            all_Rewards[i].gameObject.SetActive(true);
            all_Rewards[i].transform.DOScale(1, flt_ScaleDuration);
            all_Rewards[i].img_RewardIcon.sprite = _rewardIcons[i];
            if(i != _rewardIcons.Count - 1)
            {
                all_Rewards[i].txt_RewardAmount.text = "x" + _rewardAmounts[i].ToString();
            }
            else
            {
                all_Rewards[i].txt_RewardAmount.gameObject.SetActive(false);
            }
        }
        
    }

    public void OnClick_Continue()
    {
        for(int i = 0; i < all_Rewards.Length; i++)
        {
            all_Rewards[i].gameObject.SetActive(false);
        }
        UIManager.Instance.ui_Shop.list_SpecialItemIcons.Clear();
        UIManager.Instance.ui_Shop.list_SpecialItemRewardAmount.Clear();
        this.gameObject.SetActive(false);
    }

}
