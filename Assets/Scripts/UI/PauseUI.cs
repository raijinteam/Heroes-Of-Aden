using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private UpgradeSlotsUI[] all_ActiveUpgradeSlots;
    [SerializeField] private UpgradeSlotsUI[] all_PassiveUpgradeSlots;



    private void OnEnable()
    {
        SetActiveUpgradeSlotsData();
        SetPassiveUpgradeSlotsData();
    }

    public void SetActiveUpgradeSlotsData()
    {
        for(int i = 0; i < GameManager.Instance.list_ActivePowerUpSelectedIndexes.Count; i++)
        {
            all_ActiveUpgradeSlots[i].img_Icon.sprite = GameManager.Instance.player.all_Powerups[GameManager.Instance.list_ActivePowerUpSelectedIndexes[i]].GetMyIcon();
            all_ActiveUpgradeSlots[i].isSlotHasData = true;
            all_ActiveUpgradeSlots[i].txt_level.text = GameManager.Instance.player.GetMyPowerupLevel(i).ToString();
        }

        for (int i = 0; i < all_ActiveUpgradeSlots.Length; i++)
        {
            if (all_ActiveUpgradeSlots[i].isSlotHasData == false)
            {
                all_ActiveUpgradeSlots[i].txt_level.gameObject.SetActive(false);
            }
        }
    }


    public void SetPassiveUpgradeSlotsData()
    {
        for(int i =0; i < GameManager.Instance.list_PassivePowerUpSelectedIndexes.Count; i++)
        {
            all_PassiveUpgradeSlots[i].img_Icon.sprite = GameManager.Instance.player.all_Powerups[GameManager.Instance.list_PassivePowerUpSelectedIndexes[i]].GetMyIcon();
            all_PassiveUpgradeSlots[i].txt_level.text = GameManager.Instance.player.GetMyPowerupLevel(i).ToString();
        }

        for (int i = 0; i < all_PassiveUpgradeSlots.Length; i++)
        {
            if (all_PassiveUpgradeSlots[i].isSlotHasData == false)
            {
                all_PassiveUpgradeSlots[i].txt_level.gameObject.SetActive(false);
            }
        }
    }

    public void OnCLick_Resume()
    {
        Time.timeScale = 1;
        UIManager.Instance.ui_Gameplay.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OnClick_Home()
    {
        this.gameObject.SetActive(false);
        GameManager.Instance.PlayerDied();
        UIManager.Instance.ui_HomePanel.gameObject.SetActive(true);
        UIManager.Instance.ui_Navigation.gameObject.SetActive(true);
        UIManager.Instance.ui_useableResource.gameObject.SetActive(true);
        UIManager.Instance.ui_Gameplay.gameObject.SetActive(false);
    }
}
