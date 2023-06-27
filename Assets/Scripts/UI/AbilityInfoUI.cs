using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityInfoUI : MonoBehaviour
{
    [SerializeField] private int panelIndex;
    [SerializeField] private int abiliyIndex;
    [SerializeField] private TextMeshProUGUI txt_Header;
    [SerializeField] private TextMeshProUGUI txt_Level;
    [SerializeField] private Image img_AbilityIcon;
    [SerializeField] private GameObject panel_AbilityInfoLocked;
    [SerializeField] private TextMeshProUGUI txt_AbilityInfo;
    [SerializeField] private GameObject panel_AbilityInfoUnlocked;
    [SerializeField] private GameObject[] all_AbilityUpgradePanels;
    [SerializeField] private TextMeshProUGUI[] all_AbilityUpgradesHeader;
    [SerializeField] private TextMeshProUGUI[] all_AbilityUpgradesOldValue;
    [SerializeField] private TextMeshProUGUI[] all_AbilityUpgradesNewValue;

	public void SetMyPanel(int _powerIndex)
	{
        abiliyIndex = _powerIndex;
        string powerName = GameManager.Instance.player.all_Powerups[_powerIndex].GetMyPowerName();
        int levelNumber = GameManager.Instance.player.all_Powerups[_powerIndex].GetMyCurrentLevel();
        Sprite powerIcon = GameManager.Instance.player.all_Powerups[_powerIndex].GetMyIcon();

        levelNumber += 1;

        txt_Header.text = powerName;
        txt_Level.text = "LV." + levelNumber;
        img_AbilityIcon.sprite = powerIcon;

        
		if (GameManager.Instance.player.all_Powerups[_powerIndex].gameObject.activeSelf)
		{
            // power up is unlocked, show unlock panels
            panel_AbilityInfoLocked.SetActive(false);
            panel_AbilityInfoUnlocked.SetActive(true);

            for(int i = 0; i < all_AbilityUpgradePanels.Length; i++)
			{
                all_AbilityUpgradePanels[i].SetActive(false);
			}

            GameManager.Instance.player.all_Powerups[_powerIndex].SetUpdateInfoPanel(panelIndex);
        }
		else
		{
            // Power up is locked, show lock info
            panel_AbilityInfoLocked.SetActive(true);
            panel_AbilityInfoUnlocked.SetActive(false);
            txt_AbilityInfo.text = GameManager.Instance.player.all_Powerups[_powerIndex].GetMyPowerInfo();
        }

        gameObject.SetActive(true);
	}

    public void SetMyUpdatePanel(int _index, string _header, string _oldValue, string _newValue)
	{
        all_AbilityUpgradePanels[_index].SetActive(true);
        all_AbilityUpgradesHeader[_index].text = _header;
        all_AbilityUpgradesOldValue[_index].text = _oldValue;
        all_AbilityUpgradesNewValue[_index].text = _newValue;
    }

    public void OnClick_AbilitySelect()
	{
        GameManager.Instance.PlayerSelectedAbility(abiliyIndex);
	}
}
