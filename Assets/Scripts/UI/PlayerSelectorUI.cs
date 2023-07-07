using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSelectorUI : MonoBehaviour
{

    [SerializeField] private PlayerDetailsUI selectPlayerUI;

    [SerializeField] private Image[] all_PlayerIcons;
    public PlayerUI[] all_Player;

    private int activePlayerIndex;

    private int selectedIndex = 0;
    private void OnEnable()
    {
        CheckForPlayerUnlocked();

        activePlayerIndex = PlayerPrefs.GetInt(PlayerPrefsData.KEY_ACTIVE_PLAYER_INDEX);

        for (int i = 0; i < all_PlayerIcons.Length; i++)
        {
            if (i == activePlayerIndex)
            {
                all_PlayerIcons[i].gameObject.SetActive(true);
            }
            else
            {
                all_PlayerIcons[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < all_Player.Length; i++)
        {
            all_Player[i].img_SelectedBG.gameObject.SetActive(false);
            if (i == activePlayerIndex)
            {
                all_Player[i].img_SelectedBG.gameObject.SetActive(true);
            }
        }
        selectedIndex = activePlayerIndex;

        for (int i = 0; i < all_Player.Length; i++)
        {
            SetPlayerLevelText(i);
        }
    }

    public void CheckForPlayerUnlocked()
    {

        for(int i = 0; i < PlayerDataManager.Instance.all_CharchterData.Length; i++)
        {
            if(PlayerDataManager.Instance.IsPlayerLocked(i) == true)
            {
                all_Player[i].img_Lock.gameObject.SetActive(true);
                all_Player[i].txt_PlayerLevel.gameObject.SetActive(false);
                PlayerDataManager.Instance.all_CharchterData[i].isLocked = true;
            }
            else
            {
                all_Player[i].img_Lock.gameObject.SetActive(false);
                all_Player[i].txt_PlayerLevel.gameObject.SetActive(true); 
                PlayerDataManager.Instance.all_CharchterData[i].isLocked = false;
            }
        }

    }

    public void SetPlayerLevelText(int _selectedIndex)
    {
        all_Player[_selectedIndex].txt_PlayerLevel.text = PlayerPrefs.GetInt(PlayerPrefsData.KEY_PLAYER_LEVEL + _selectedIndex).ToString(); ;
    }

    public void OnClick_UpgradePlayer(int _playerIndex)
    {

        ServiceManager.Instance.soundManager.PlayButtonClickSound();

        for (int i = 0; i < all_Player.Length; i++)
        {
            all_Player[i].img_SelectedBG.gameObject.SetActive(false);
            if (i == _playerIndex)
            {
                all_Player[i].img_SelectedBG.gameObject.SetActive(true);
            }
        }
        selectedIndex = _playerIndex;

        for(int i = 0; i < all_PlayerIcons.Length; i++)
        {
            all_PlayerIcons[i].gameObject.SetActive(false);
            if(i == _playerIndex)
            {
                all_PlayerIcons[i].gameObject.SetActive(true);
            }
        }
    }


    public void OnClick_ShowPlayerDetails()
    {
        ServiceManager.Instance.soundManager.PlayButtonClickSound();

        selectPlayerUI.gameObject.SetActive(true);

        selectPlayerUI.SetSelectedPlayerData(selectedIndex);
    }
}
