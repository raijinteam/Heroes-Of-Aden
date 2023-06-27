using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDetailsUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI txt_PlayerName;
    [SerializeField] private Image img_PlayerIcon;
    [SerializeField] private TextMeshProUGUI txt_PlayerLevel;
    [SerializeField] private TextMeshProUGUI txt_PlayerDesctiption;
    [SerializeField] private TextMeshProUGUI txt_PlayerHealth;
    [SerializeField] private TextMeshProUGUI txt_Damage;
    [SerializeField] private TextMeshProUGUI txt_Firerate;

    [SerializeField] private Button btn_Select;
    [SerializeField] private Button btn_Upgrade;

    [SerializeField] private TextMeshProUGUI txt_UpgradeButton;

    private int selectedIndex;


    private void OnEnable()
    {

    }

    public void SetSelectedPlayerData(int _selectedIndex)
    {
        int selectedPlayerLevel = PlayerDataManager.Instance.GetPlayerLevel(_selectedIndex);
        selectedIndex = _selectedIndex;

        //IF PLAYER LOCKED SHOW LOCKED IN LEVEL AND IF NOT SHOW PLAYER LEVEL
        if (PlayerDataManager.Instance.IsPlayerLocked(_selectedIndex))
        {
            btn_Select.gameObject.SetActive(false);
            txt_PlayerLevel.text = "Locked";
            Debug.Log("Plyaer Locked");
        }
        else
        {
            btn_Select.gameObject.SetActive(true);
            Debug.Log("Plyaer Unlocked");
            txt_PlayerLevel.text = "LV." + (PlayerDataManager.Instance.GetPlayerLevel(_selectedIndex)).ToString();
        }


        txt_PlayerName.text = PlayerDataManager.Instance.GetPlayerName(_selectedIndex);
        img_PlayerIcon.sprite = UIManager.Instance.ui_PlayerSelector.all_Player[_selectedIndex].img_PlayerIcon.sprite;

        txt_PlayerDesctiption.text = PlayerDataManager.Instance.all_CharchterData[_selectedIndex].description;

        txt_PlayerHealth.text = PlayerDataManager.Instance.all_CharchterData[_selectedIndex].health[selectedPlayerLevel].ToString();

        txt_Damage.text = PlayerDataManager.Instance.all_CharchterData[_selectedIndex].damage[selectedPlayerLevel].ToString();

        txt_Firerate.text = PlayerDataManager.Instance.all_CharchterData[_selectedIndex].firerate[selectedPlayerLevel].ToString();

        txt_UpgradeButton.text = PlayerDataManager.Instance.all_CharchterData[_selectedIndex].upgradeAmount[PlayerDataManager.Instance.GetPlayerLevel(_selectedIndex)].ToString();


        Debug.Log("Selected index " + _selectedIndex);
        if (PlayerDataManager.Instance.IsPlayerReachMaxLevel(_selectedIndex))
        {
            Debug.Log("Hide Btn Upgrade ");
            txt_PlayerLevel.text = "Max";
            btn_Upgrade.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Show Btn Upgrade ");
            btn_Upgrade.gameObject.SetActive(true);
        }
    }


    //BUTTON CLICK WHEN USER SELECT PLAYER
    public void OnClick_SelectPlayer()
    {
        this.gameObject.SetActive(false);
        PlayerPrefs.SetInt(PlayerPrefsData.KEY_ACTIVE_PLAYER_INDEX, selectedIndex);
        UIManager.Instance.ui_HomePanel.SetActivePlayerImage(selectedIndex);
    }

    //BUTTON EVENT WHEN USER CLICK FOR UPGRADE BUTTON 
    public void OnClick_UpgradePlayer()
    {

        if (!PlayerDataManager.Instance.hasEnoughCoinsForUpgradePlayer(selectedIndex))
        {
            UIManager.Instance.SpawnPopUpBox("Not Enough Coins");
            return;
        }



        int selectedPlayerLevel = PlayerDataManager.Instance.GetPlayerLevel(selectedIndex) + 1;

        if (PlayerDataManager.Instance.IsPlayerLocked(selectedIndex))
        {
            PlayerDataManager.Instance.SetUnlockPlayer(selectedIndex);
            btn_Select.gameObject.SetActive(true);
            PlayerDataManager.Instance.SetPlayerLevel(selectedIndex);
            UIManager.Instance.ui_PlayerSelector.SetPlayerLevelText(selectedIndex);
            UIManager.Instance.ui_PlayerSelector.CheckForPlayerUnlocked();
            txt_PlayerLevel.text = "LV." + (PlayerDataManager.Instance.GetPlayerLevel(selectedIndex)).ToString();
        }
        else
        {
            PlayerDataManager.Instance.SetPlayerLevel(selectedIndex);

            UIManager.Instance.ui_PlayerSelector.SetPlayerLevelText(selectedIndex);
            txt_PlayerLevel.text = "LV." + (PlayerDataManager.Instance.GetPlayerLevel(selectedIndex)).ToString();

            txt_PlayerHealth.text = PlayerDataManager.Instance.all_CharchterData[selectedIndex].health[selectedPlayerLevel].ToString();
            txt_Damage.text = PlayerDataManager.Instance.all_CharchterData[selectedIndex].damage[selectedPlayerLevel].ToString();
            txt_Firerate.text = PlayerDataManager.Instance.all_CharchterData[selectedIndex].firerate[selectedPlayerLevel].ToString();
            txt_UpgradeButton.text = PlayerDataManager.Instance.all_CharchterData[selectedIndex].upgradeAmount[PlayerDataManager.Instance.GetPlayerLevel(selectedIndex)].ToString();
            DataManager.Instance.SubstractCoins(PlayerDataManager.Instance.all_CharchterData[selectedIndex].upgradeAmount[selectedPlayerLevel]);
        }

        if (PlayerDataManager.Instance.IsPlayerReachMaxLevel(selectedIndex))
        {
            txt_PlayerLevel.text = "Max";
            btn_Upgrade.gameObject.SetActive(false);
            return;
        }
    }

    public void OnClick_CloseMenu()
    {
        this.gameObject.SetActive(false);
    }
}