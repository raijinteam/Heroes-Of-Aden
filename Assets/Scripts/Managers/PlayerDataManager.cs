using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{

    public static PlayerDataManager Instance;

    public GameObject[] all_PlayerPrefabs;
    public CharchterData[] all_CharchterData;
    public int playerMaxLevel = 10;



    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        CheckIsPlayerUnlocked();


        for(int i =0; i < all_CharchterData.Length; i++)
        {
            all_CharchterData[i].level = PlayerPrefs.GetInt(PlayerPrefsData.KEY_PLAYER_LEVEL + i);
        }
    }


    //CHECK FOR PLAYER UNLOCK THAT METHOD USE ONLY WHEN THIS SCRIPT ENABLE
    public void CheckIsPlayerUnlocked()
    {
        for (int i = 0; i < all_CharchterData.Length; i++)
        {
            if (PlayerPrefs.GetInt(PlayerPrefsData.KEY_UNLOCK_PLAYER + i) == 0)
            {
                all_CharchterData[i].isLocked = true;
            }
            else
            {
                all_CharchterData[i].isLocked = false;
            }
        }
    }


    //CHECK FOR PLAYER IS LOCKED OR NOT
    public bool IsPlayerLocked(int _playerIndex)
    {
        if (PlayerPrefs.GetInt(PlayerPrefsData.KEY_UNLOCK_PLAYER + _playerIndex) == 0)
        {
            return true;
        }
        return false;
    }


    //CHECK FOR PLAYER HAS ENOUCH COINS TO UPGRADE PLAYER
    public bool hasEnoughCoinsForUpgradePlayer(int _playerIndex)
    {
        if (ServiceManager.Instance.dataManager.totalCoins > all_CharchterData[_playerIndex].upgradeAmount[all_CharchterData[_playerIndex].level])
        {

            return true;
        }
        return false;
    }

    public bool HasEnoughGemsForUpgradePlayer(int _playerIndex)
    {
        /*if(DataManager.Instance.totalGems >= all_CharchterData[_playerIndex].upgradeAmount[all_CharchterData[_playerIndex].level])
        {
            return true;
        }
        return false;*/

        return ServiceManager.Instance.dataManager.totalGems > all_CharchterData[_playerIndex].upgradeAmount[all_CharchterData[_playerIndex].level] ? true : false;
    }



    //CHECK IF ANY PLAYER REACH MAX LEVEL
    public bool IsPlayerReachMaxLevel(int _playerIndex)
    {
        if (PlayerPrefs.GetInt(PlayerPrefsData.KEY_PLAYER_LEVEL + _playerIndex) >= playerMaxLevel)
        {
            return true;
        }
        return false;
    }



    #region ALL GET SET FUNCTIONS

    public string GetPlayerName(int _playerIndex)
    {
        return all_CharchterData[_playerIndex].name;
    }

    public void SetPlayerLevel(int _playerIndex)
    {
        all_CharchterData[_playerIndex].level = PlayerPrefs.GetInt(PlayerPrefsData.KEY_PLAYER_LEVEL + _playerIndex);
        all_CharchterData[_playerIndex].level += 1;
        PlayerPrefs.SetInt(PlayerPrefsData.KEY_PLAYER_LEVEL + _playerIndex, all_CharchterData[_playerIndex].level);
    }


    public int GetPlayerLevel(int _playerIndex)
    {
        return PlayerPrefs.GetInt(PlayerPrefsData.KEY_PLAYER_LEVEL + _playerIndex);
    }

    public void SetUnlockPlayer(int _playerIndex)
    {
        all_CharchterData[_playerIndex].isLocked = false;
        PlayerPrefs.SetInt(PlayerPrefsData.KEY_UNLOCK_PLAYER + _playerIndex, 1);
        Debug.Log(PlayerPrefs.GetInt(PlayerPrefsData.KEY_UNLOCK_PLAYER + _playerIndex));
    }


    #endregion


}
