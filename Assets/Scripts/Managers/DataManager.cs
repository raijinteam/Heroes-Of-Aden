using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
	public static DataManager Instance;

	

	[Header("Player Data")]
	public int totalCoins; // TOTAL COINS IN GAME
	public int totalGems; // TOTAL GAMES IN GAME
	public int totalEnergy; // TOTAL ENERGY IN GAME
	public int collectedCoinsInGame; // collected coins when player plays game
	public int killCountIngame; //enemy kill amount when player plays game
	public float activeGameTime;
	public bool isNOAdPurchase;
	private bool isSFXOn;

	[HideInInspector]
	public int activePlayerIndex; //ACTIVE PLAYER INDEX 

	
	


	private void Awake()
	{
		if(Instance == null)
        {
			Instance = this;
        }
        else
        {
			Destroy(gameObject);
        }
		DontDestroyOnLoad(this.gameObject);


		
	}


    private void Start()
    {
		if (PlayerPrefs.HasKey(PlayerPrefsData.KEY_COINS))
		{
			GetAllData(); //SET DATA WHEN GAME ALREADY LOAD 
		}
		else
		{
			SetFirstTimeData(); //SET DATA WHEN GAME FIRST TIME LOAD
		}
	}

    private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			ClearAllPlayerPrefsData();
		}
	}

	public void SetFirstTimeData()
	{
		Debug.Log("Set Player all Data");

		PlayerPrefs.SetInt(PlayerPrefsData.KEY_COINS, totalCoins);
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_GAMES, totalGems);
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_ENERGY, totalEnergy);
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_MUSIC, 1);
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_SFX, 1);
		PlayerPrefs.SetFloat(PlayerPrefsData.KEY_BESTTIME, 0.0f);
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_ACTIVE_PLAYER_INDEX, 0);

		//Set game start time to null
		PlayerPrefs.SetString(PlayerPrefsData.KEY_GAME_CURRENT_TIME, "");
		PlayerPrefs.SetFloat(PlayerPrefsData.KEY_GAME_ACTIVE_TIME, 0);


		PlayerPrefs.SetInt(PlayerPrefsData.KEY_NOAD, 0);
		isNOAdPurchase = false;


		//Set By Default Player 1 Unlock And Level is 1
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_UNLOCK_PLAYER + 0, 1);
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_PLAYER_LEVEL + 0, 1);



		activePlayerIndex = PlayerPrefs.GetInt(PlayerPrefsData.KEY_ACTIVE_PLAYER_INDEX);


		//Set PassiveUpgrade  Levels
		SetAllPassiveUpgradeLevels();

		//Set Default Active PLayer 1
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_ACTIVE_PLAYER_INDEX, 0);

	}


	public void GetAllData()
	{
		Debug.Log("Load Already Created  Data");

		totalCoins = PlayerPrefs.GetInt(PlayerPrefsData.KEY_COINS);
		totalGems = PlayerPrefs.GetInt(PlayerPrefsData.KEY_GAMES);
		totalEnergy = PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY);

		activePlayerIndex = PlayerPrefs.GetInt(PlayerPrefsData.KEY_ACTIVE_PLAYER_INDEX);
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_ACTIVE_PLAYER_INDEX, activePlayerIndex);

		PlayerPrefs.GetInt(PlayerPrefsData.KEY_MUSIC);
		PlayerPrefs.GetInt(PlayerPrefsData.KEY_SFX);



		CheckForNoAdPurchase();
	}


	private void SetAllPassiveUpgradeLevels()
	{
		for (int i = 0; i < 7; i++)
		{
			PlayerPrefs.SetInt(PlayerPrefsData.KEY_UPGRADE_LEVEL + i, 1);
		}
	}


	public void NoAds()
    {
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_NOAD, 1);
		CheckForNoAdPurchase();
    }

	public void CheckForNoAdPurchase()
	{
		if (PlayerPrefs.GetInt(PlayerPrefsData.KEY_NOAD) == 0)
        {
			isNOAdPurchase = false;
        }
        else
        {
			isNOAdPurchase = true;
		}
    }

	public void CollectedCoinInGame()
	{
		totalCoins += 1;
	}

	public void IncreaseCoins(int _addAmount)
    {
		totalCoins += _addAmount;
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_COINS, totalCoins);	
	}

	public void IncreaseGems(int _addAmount)
	{
		totalGems += _addAmount;
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_GAMES, totalGems);	
	}

	public void IncreaseEnergy(int _addAmount)
	{
		totalEnergy += _addAmount;
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_ENERGY, totalEnergy);
		Debug.Log(PlayerPrefs.GetInt(PlayerPrefsData.KEY_ENERGY));
	
	}

	public void IncreaseEnergyOnStart(int _addAmount)
    {
		totalEnergy += _addAmount; 
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_ENERGY, totalEnergy);
	}

	public void SubstractCoins(int _spendAmount)
	{
		totalCoins -= _spendAmount;
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_COINS, totalCoins);
		
	}

	public void SubstractGames(int _spandAmount)
	{
		totalGems -= _spandAmount;
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_GAMES, totalGems);
		
	}

	public void SubstractEnergy(int _spandAmount)
	{
		totalEnergy -= _spandAmount;
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_ENERGY, totalEnergy);
	}


	public bool IsSFXON()
    {
		//Debug.Log("For Sfx : " + PlayerPrefs.GetInt(PlayerPrefsData.KEY_SFX));
		//return (PlayerPrefs.GetInt(PlayerPrefsData.KEY_SFX) == 0) ? false : true;
		return (UIManager.Instance.ui_Setting.isSFXOn) ? true : false;
	}

	public void ClearAllPlayerPrefsData()
	{
		PlayerPrefs.DeleteAll();
		Debug.Log("Player Prefs Data Clear");
	}

}
