using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveUpgradeManager : MonoBehaviour
{
    public static PassiveUpgradeManager Instance;

    public PassiveUpgradeData[] all_PassiveData;
    public int coinsForUpgrade = 500;
    public int maxPassiveUpgradeLevel = 10;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }



    

    //CHECK IF PLAYER HAS ENOUGH COINS FOR UPGRADE 
    public bool hasEnoughCoinsForUpgrade()
    {
        if (ServiceManager.Instance.dataManager.totalCoins >= coinsForUpgrade)
        {
            return true;
        }
        return false;
    }

    //CHECK IF ANY UPGRADE REACH ITS MAX LEVEL
    public bool IsUpgradeReachMaxLevel(int _upgradeIndex)
    {
        if (all_PassiveData[_upgradeIndex].currentLevel == maxPassiveUpgradeLevel)
        {
            return true;
        }

        return false;
    }

    //INCREASE UPGRADE LEVEL AND SET IN PLAYERPREFS
    public void IncreasePassiveUpgradeCurrentLevel(int _passiveIndex)
    {
        all_PassiveData[_passiveIndex].currentLevel++;
        SetPassiveUpgradeLevelsPlayerPrefs();
    }

    //SET UPGRADE LEVEL IN PLAYERPREFS
    public void SetPassiveUpgradeLevelsPlayerPrefs()
    {
        for (int i = 0; i < all_PassiveData.Length; i++)
        {
            PlayerPrefs.SetInt(PlayerPrefsData.KEY_UPGRADE_LEVEL + i, all_PassiveData[i].currentLevel);
        }
    }



    #region ALL GET FUNCTIONS


    public int GetCurrentPassivesLevel(int _passiveIndex)
    {
        return all_PassiveData[_passiveIndex].currentLevel;
    }

    public float GetCurrentUpgradeValue(int _passiveIndex)
    {
        return all_PassiveData[_passiveIndex].all_Values[all_PassiveData[_passiveIndex].currentLevel - 2];
    }

    public float GetUpgradedValue(int _passiveIndex)
    {
        return all_PassiveData[_passiveIndex].all_Values[all_PassiveData[_passiveIndex].currentLevel - 1];
    }


    public string GetCurrentPassiveUpgradeName(int _passiveIndex)
    {
        return all_PassiveData[_passiveIndex].upgradeName;
    }

    public string GetCurrentPassiveUpgradeDesc(int _passiveIndex)
    {
        return all_PassiveData[_passiveIndex].description;
    }


    #endregion
}
