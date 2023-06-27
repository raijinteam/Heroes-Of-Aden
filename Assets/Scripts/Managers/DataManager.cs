using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
	public static DataManager Instance;

	

	[Header("Player Data")]
	public int totalCoins; // TOTAL COINS IN GAME
	public int totalGames; // TOTAL GAMES IN GAME
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
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_COINS, totalCoins);
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_GAMES, totalGames);
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_ENERGY, totalEnergy);
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_MUSIC, 1);
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_SFX, 1);
		PlayerPrefs.SetFloat(PlayerPrefsData.KEY_BESTTIME, 0.0f);
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_ACTIVE_PLAYER_INDEX, 0);

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
		totalCoins = PlayerPrefs.GetInt(PlayerPrefsData.KEY_COINS);
		totalGames = PlayerPrefs.GetInt(PlayerPrefsData.KEY_GAMES);
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
		UIManager.Instance.ui_useableResource.CalcCoins();
	}

	public void IncreaseGems(int _addAmount)
	{
		totalGames += _addAmount;
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_GAMES, totalGames);
		UIManager.Instance.ui_useableResource.CalcGames();
	}

	public void IncreaseEnergy(int _addAmount)
	{
		totalEnergy += _addAmount;
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_ENERGY, totalEnergy);
		UIManager.Instance.ui_useableResource.CalcEnergy();
	}

	public void SubstractCoins(int _spendAmount)
	{
		totalCoins -= _spendAmount;
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_COINS, totalCoins);
		UIManager.Instance.ui_useableResource.CalcCoins();
	}

	public void SubstractGames(int _spandAmount)
	{
		totalGames -= _spandAmount;
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_GAMES, totalGames);
		UIManager.Instance.ui_useableResource.CalcGames();
	}

	public void SubstractEnergy(int _spandAmount)
	{
		totalEnergy -= _spandAmount;
		PlayerPrefs.SetInt(PlayerPrefsData.KEY_ENERGY, totalEnergy);
		UIManager.Instance.ui_useableResource.CalcEnergy();
	}

	

	public void ResetRoundDataWhenGameover()
    {
		collectedCoinsInGame = 0;
		GameManager.Instance.currentPlayerPoints = 0;
		GameManager.Instance.currentPlayerLevel = 0;
		killCountIngame = 0;
		activeGameTime = 0;
		GameManager.Instance.list_ActivePowerUpSelectedIndexes.Clear();
		GameManager.Instance.list_PassivePowerUpSelectedIndexes.Clear();
		GameManager.Instance.timeInThisRound = 0;
		GameManager.Instance.player.gameObject.SetActive(false);
    }

	public bool IsSFXON()
    {
		Debug.Log("For Sfx : " + PlayerPrefs.GetInt(PlayerPrefsData.KEY_SFX));
		//return (PlayerPrefs.GetInt(PlayerPrefsData.KEY_SFX) == 0) ? false : true;
		return (UIManager.Instance.ui_Setting.isSFXOn) ? true : false;
	}

	public void ClearAllPlayerPrefsData()
	{
		PlayerPrefs.DeleteAll();
		Debug.Log("Player Prefs Data Clear");
	}

}
