using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	private void Awake()
	{
		Instance = this;
	}


	[Header("Game Data")]
	public List<Transform> list_ActiveEnemies;
	public bool isGameRunning = false;
	public float currentPlayerPoints;
	public float pointsRequiredToLevelUp;
	public int currentPlayerLevel = 0;
	private float percentageOfPointsIncreaseForNextLevel = 25;
	public float timeInThisRound;
	public int killCountInThisRound;
	public int coinsCollectedInThisRound;
	public bool isPlayerTakeRevive;
	public CinemachineVirtualCamera virtual_Camera;

	[Header("Game Abilities Selection Handler")]
	public List<int> list_PowerUpIndexesToChooseFrom = new List<int>();
	public List<int> list_PowerUpIndexesChosen = new List<int>();
	public int activePowerUpSelectedCount = 0;
	public int passivePowerUpSelectedCount = 0;
	private int maxPowerUpSelectCount = 4;
	public List<int> list_ActivePowerUpSelectedIndexes = new List<int>();
	public List<int> list_PassivePowerUpSelectedIndexes = new List<int>();

	[Header("Points Spawn")]
	[SerializeField] private Points point;
	public Transform pointSpawnParent;

	[Header("Boss Handling")]
	[SerializeField] private int bossSpawnEnemyCount;
	[SerializeField] private float bossSpawnEnemyCountIncreaseByPercentage;
	[SerializeField] private bool shouldSpawnBoss = false;
	private int numberOfBossesSpawned = 0;
	public GameObject bossBoundary;
	private string tag_BossPowers = "BossPowers";
	public int bossKilledCount = 0;


	[Header("Enemy Spawn")]
	public int enemyKilled = 0;
	public int enemySpawnCount = 0;
	public string[] all_SortingLayerName;
	[SerializeField] private NormalEnemySpawning normalEnemy;
	[SerializeField] private NormalShootingEnemySpawning shootingEnemy;
	[SerializeField] private GoblinSurgeSpawn goblinEnemy;

	[Header("Pickups")]
	[SerializeField] private Transform pickUpSpawnParent;
	public Transform coinSpawnParent;
	[SerializeField] private GameObject healthPickUp;
	[SerializeField] private GameObject coinPickUp;
	[SerializeField] private GameObject magnetPickup;
	[SerializeField] private float healthPickUpSpawnChance;
	[SerializeField] private float magnetPickupSpawnChance;
	[SerializeField] private float coinPickUpSpawnChance;
	
	[Header("Player")]
	public PlayerData player;
	private string tag_PlayerPowers = "PlayerPower";
	public Transform playerBulletSpawnParent;

	[Header("Feel Tools")]
	public GameObject FeelTextSpawner;


    private void Start()
	{

		//StartGame();
		//ShowRandomPowerUps();

		if(ServiceManager.Instance.dataManager.gameCountForShowRateusBox == 2  && !ServiceManager.Instance.dataManager.isRateusShow)
        {
			UIManager.Instance.ui_RateUs.gameObject.SetActive(true);
        }

		if(ServiceManager.Instance.dataManager.gameCountForShowSpecialItem >= 1 && !ServiceManager.Instance.dataManager.isSpecialItemPurchase)
        {
			UIManager.Instance.ui_SpecialItem.gameObject.SetActive(true);
		}

		Invoke("SpawnText", 1f);
	}

	private void SpawnText()
	{
		FeelTextSpawner.SetActive(true);
	}


	private void Update()
	{

		if (!isGameRunning)
		{
			return;
		}

       
		//calculate time running for active round
		if (isGameRunning)
        {
			timeInThisRound += Time.deltaTime;
			int minutes = Mathf.FloorToInt(timeInThisRound / 60f);
			int seconds = Mathf.FloorToInt(timeInThisRound % 60f);
			UIManager.Instance.ui_Gameplay.GetGameplayTimer().text = string.Format("{0:00}:{1:00}", minutes, seconds);
		}

		

		if (!shouldSpawnBoss)
		{
			normalEnemy.NormalEnemySpawnHandling();
			shootingEnemy.ShootingEnemySpawnHandling();
			goblinEnemy.GoblinSurgeHandling();
		}

	}

	public void StartGame()
	{
		isGameRunning = true; 
		UIManager.Instance.ui_Navigation.gameObject.SetActive(false);
		UIManager.Instance.ui_HomePanel.gameObject.SetActive(false);
		UIManager.Instance.ui_Gameplay.gameObject.SetActive(true);
		UIManager.Instance.ui_useableResource.gameObject.SetActive(false);
		UIManager.Instance.ui_Gameplay.SetLevelSlider(pointsRequiredToLevelUp, currentPlayerPoints, currentPlayerLevel);
		//player.gameObject.SetActive(true);
	}

	private void OnApplicationQuit()
	{
		DateTime timeQuit = DateTime.Now;
		PlayerPrefs.SetString(PlayerPrefsData.KEY_QUIT_TIME, timeQuit.ToString());
		//Debug.Log("Game Quit time : " + timeQuit.ToString());
	}

	public void InstantiateActivePlayer()
    {
		GameObject activePlayer = PlayerDataManager.Instance.all_PlayerPrefabs[PlayerPrefs.GetInt(PlayerPrefsData.KEY_ACTIVE_PLAYER_INDEX)];
		GameObject _player = Instantiate(activePlayer, transform.position, Quaternion.identity);
		virtual_Camera.Follow = _player.transform;
		player = _player.GetComponent<PlayerData>();
    }


	public void AddEnemyToActiveList(Transform _enemy)
	{
		Debug.Log("Enemy Spawned : "+ _enemy.gameObject.name);

		enemySpawnCount += 1;
		list_ActiveEnemies.Add(_enemy);

		if(enemySpawnCount == bossSpawnEnemyCount)
		{
			shouldSpawnBoss = true;
			goblinEnemy.StopGoblinSurgeIfRunning();
		}
	}

	public void GiveAllPointsAndCoinOnRevive()
    {
		foreach(Transform point in pointSpawnParent)
        {
			Destroy(point.gameObject);
        }

		ServiceManager.Instance.dataManager.IncreaseCoins(coinSpawnParent.childCount);

    }

	public void DestoryAllEnemies()
    {
		foreach(Transform enemy in EnemyManager.Instance.enemySpawnParent)
        {
			killCountInThisRound += EnemyManager.Instance.enemySpawnParent.childCount;
			enemy.GetComponent<EnemyHealth>().ForceDie();
			//Destroy(enemy.gameObject);
			list_ActiveEnemies.Clear();
        }
    }
    
	public void DestoryAllPlayerBullet()
    {
		foreach(Transform bullet in playerBulletSpawnParent)
        {
			Destroy(bullet.gameObject);
        }
    }

    public void EnemyDied(Transform _enemy, int _pointValue)
	{
		Debug.Log("Enemy Killed : " + _enemy.gameObject.name);

		//enemyKilled += 1;
		//Changes 
		UIManager.Instance.ui_Gameplay.IncreaseGameplayKillCount();
		//over change
		list_ActiveEnemies.Remove(_enemy);
		SpawnOnePointWhereEnemyDied(_enemy.position, _pointValue);
		
		if(enemyKilled == bossSpawnEnemyCount && shouldSpawnBoss)
		{
			SpawnTheBoss();
		}

		float randomIndex = Random.Range(0f, 100f);
		if(randomIndex <= healthPickUpSpawnChance)
		{
			SpawnHealthPickUp();
		}

		if(randomIndex <= magnetPickupSpawnChance)
        {
			SpawnMagnetPickUp();
        }

		randomIndex = Random.Range(0f, 100f);
		if (randomIndex <= coinPickUpSpawnChance)
		{
			SpawnCoinPickUp();
		}
	}


	private void SpawnTheBoss()
	{
		Vector3 randomPosition = GetPlayerCurrentPosition();

		float randomXPos = Random.Range(3f, 6f);
		float randomYPos = Random.Range(3f, 6f);

		int randomSideIndex = Random.Range(0, 2);
		if (randomSideIndex == 1)
		{
			// spawn x negative
			randomXPos = -randomXPos;
		}

		randomSideIndex = Random.Range(0, 2);
		if (randomSideIndex == 1)
		{
			// spawn y negative
			randomYPos = -randomYPos;
		}

		randomPosition.x += randomXPos;
		randomPosition.y += randomYPos;
		GameObject enemy;
		if (numberOfBossesSpawned < EnemyManager.Instance.all_Bosses.Length)
		{
			enemy = Instantiate(EnemyManager.Instance.all_Bosses[numberOfBossesSpawned], randomPosition, Quaternion.identity, EnemyManager.Instance.enemySpawnParent);
		}
		else
		{
			int bossIndex = Random.Range(0, EnemyManager.Instance.all_Bosses.Length);
			enemy = Instantiate(EnemyManager.Instance.all_Bosses[bossIndex], randomPosition, Quaternion.identity, EnemyManager.Instance.enemySpawnParent);
		}

		bossBoundary.transform.position = randomPosition;
		bossBoundary.SetActive(true);

		numberOfBossesSpawned += 1;
		AddEnemyToActiveList(enemy.transform);
	}

	public void BossDied(Vector3 _position)
	{
		bossKilledCount += 1;
	
		int countIncrease = (int)(bossSpawnEnemyCount * (bossSpawnEnemyCountIncreaseByPercentage / 100));
		bossSpawnEnemyCount = bossSpawnEnemyCount + (bossSpawnEnemyCount + countIncrease); // new enemy death count
		
		shouldSpawnBoss = false;
		bossBoundary.SetActive(false);

		normalEnemy.IncreaseEnemyLevel();
		shootingEnemy.IncreaseEnemyLevel();

		GameObject[] leftOutObjects = GameObject.FindGameObjectsWithTag(tag_BossPowers);
		foreach(GameObject leftOut in leftOutObjects)
		{
			Destroy(leftOut);
		}

		SpawnHealthWhereBossDied(_position);
		SpawnCoinPickUp();
	}

	private void SpawnHealthPickUp()
	{
		Vector3 randomPosition = GetPlayerCurrentPosition();

		float randomXPos = Random.Range(8f, 20f);
		float randomYPos = Random.Range(8f, 20f);

		int randomSideIndex = Random.Range(0, 2);
		if (randomSideIndex == 1)
		{
			// spawn x negative
			randomXPos = -randomXPos;
		}

		randomSideIndex = Random.Range(0, 2);
		if (randomSideIndex == 1)
		{
			// spawn y negative
			randomYPos = -randomYPos;
		}

		randomPosition.x += randomXPos;
		randomPosition.y += randomYPos;

		Instantiate(healthPickUp, randomPosition, healthPickUp.transform.rotation, pickUpSpawnParent);
	}

	private void SpawnMagnetPickUp()
	{
		Vector3 randomPosition = GetPlayerCurrentPosition();

		float randomXPos = Random.Range(8f, 20f);
		float randomYPos = Random.Range(8f, 20f);

		int randomSideIndex = Random.Range(0, 2);
		if (randomSideIndex == 1)
		{
			// spawn x negative
			randomXPos = -randomXPos;
		}

		randomSideIndex = Random.Range(0, 2);
		if (randomSideIndex == 1)
		{
			// spawn y negative
			randomYPos = -randomYPos;
		}

		randomPosition.x += randomXPos;
		randomPosition.y += randomYPos;

		Instantiate(magnetPickup, randomPosition, magnetPickup.transform.rotation, pickUpSpawnParent);
	}

	private void SpawnCoinPickUp()
	{
		Vector3 randomPosition = GetPlayerCurrentPosition();

		float randomXPos = Random.Range(8f, 20f);
		float randomYPos = Random.Range(8f, 20f);

		int randomSideIndex = Random.Range(0, 2);
		if (randomSideIndex == 1)
		{
			// spawn x negative
			randomXPos = -randomXPos;
		}

		randomSideIndex = Random.Range(0, 2);
		if (randomSideIndex == 1)
		{
			// spawn y negative
			randomYPos = -randomYPos;
		}

		randomPosition.x += randomXPos;
		randomPosition.y += randomYPos;

		Instantiate(coinPickUp, randomPosition, healthPickUp.transform.rotation, coinSpawnParent);
	}

	public void SpawnOnePointWhereEnemyDied(Vector3 _spawnPosition, int _pointValue)
	{
		float xPos = Random.Range(-0.5f, 0.5f);
		float yPos = Random.Range(-0.5f, 0.5f);

		_spawnPosition.x += xPos;
		_spawnPosition.y += yPos;

		Points pnt = Instantiate(point, _spawnPosition, Quaternion.identity, pointSpawnParent);
		pnt.pointsValue = _pointValue;
	}

	public void SpawnHealthWhereBossDied(Vector3 _spawnPosition)
	{
		float xPos = Random.Range(-1f, 1f);
		float yPos = Random.Range(-1f, 1f);

		_spawnPosition.x += xPos;
		_spawnPosition.y += yPos;

		Instantiate(healthPickUp, _spawnPosition, healthPickUp.transform.rotation, pickUpSpawnParent);
	}

	public void PlayerCollectedPoint(float _pointValue)
	{
		if (!isGameRunning)
		{
			return;
		}

		currentPlayerPoints += _pointValue;

		//Play Point Collected Sound
		ServiceManager.Instance.soundManager.PlayPointCollectSound();

		if(currentPlayerPoints >= pointsRequiredToLevelUp)
		{
			// level up

			// calculate points left to move over in next level
			currentPlayerPoints = pointsRequiredToLevelUp - currentPlayerPoints;

			// increases total points for next level
			pointsRequiredToLevelUp = pointsRequiredToLevelUp + (pointsRequiredToLevelUp * (percentageOfPointsIncreaseForNextLevel / 100));
			currentPlayerLevel += 1;

			UIManager.Instance.ui_Gameplay.SetLevelSlider(pointsRequiredToLevelUp, currentPlayerPoints, currentPlayerLevel);

			ShowRandomPowerUps();
		}
		else
		{
			UIManager.Instance.ui_Gameplay.UpdateLevelSlider(currentPlayerPoints);
		}
	}

	public void ShowRandomPowerUps()
	{
			
		if (list_PowerUpIndexesToChooseFrom.Count > 0)
		{
			list_PowerUpIndexesToChooseFrom.Clear();
		}

		if (list_PowerUpIndexesChosen.Count > 0)
		{
			list_PowerUpIndexesChosen.Clear();
		}

		// add available powerups and then show random.
		AddFromActivePowerList();
		AddFromPassivePowerList();

		RandomizeSelection();
		SetChosenPowerUpInfo();
	}

	private void AddFromActivePowerList()
	{
		if(activePowerUpSelectedCount == maxPowerUpSelectCount)
		{
			for (int i = 0; i < list_ActivePowerUpSelectedIndexes.Count; i++)
			{

				if(player.all_Powerups[list_ActivePowerUpSelectedIndexes[i]].currentLevel != AbilityManager.Instance.abilityMaxLevel)
				{
					list_PowerUpIndexesToChooseFrom.Add(list_ActivePowerUpSelectedIndexes[i]);
				}
			}

			return; // already selected max powerups
		}

		for(int i = 0; i < player.all_Powerups.Length; i++)
		{
			if (player.all_Powerups[i].isPassivePowerUp)
			{
				continue; // do not take this into consideration as this is passive power
			}

			if(player.all_Powerups[i].currentLevel == AbilityManager.Instance.abilityMaxLevel)
			{
				continue; // Do not take this into consideration as this is already max level
			}

			list_PowerUpIndexesToChooseFrom.Add(i);
		}
	}

	private void AddFromPassivePowerList()
	{
		if(passivePowerUpSelectedCount == maxPowerUpSelectCount)
		{

			for (int i = 0; i < list_PassivePowerUpSelectedIndexes.Count; i++)
			{
				if (player.all_Powerups[list_PassivePowerUpSelectedIndexes[i]].currentLevel != AbilityManager.Instance.abilityMaxLevel)
				{
					list_PowerUpIndexesToChooseFrom.Add(list_PassivePowerUpSelectedIndexes[i]);
				}
			}

			return; // already selected max powerups
		}

		for (int i = 0; i < player.all_Powerups.Length; i++)
		{
			if (!player.all_Powerups[i].isPassivePowerUp)
			{
				continue; // do not take this into consideration as this is active power
			}

			if (player.all_Powerups[i].currentLevel == AbilityManager.Instance.abilityMaxLevel)
			{
				continue; // Do not take this into consideration as this is already max level
			}

			list_PowerUpIndexesToChooseFrom.Add(i);
		}
	}

	private void RandomizeSelection()
	{
		if(list_PowerUpIndexesToChooseFrom.Count > 0)
		{
			Time.timeScale = 0;

			if (list_PowerUpIndexesToChooseFrom.Count > 3)
			{
				// select 3 

				for (int i = 0; i < 3; i++)
				{
					int randomIndex = Random.Range(0, list_PowerUpIndexesToChooseFrom.Count);

					list_PowerUpIndexesChosen.Add(list_PowerUpIndexesToChooseFrom[randomIndex]);

					list_PowerUpIndexesToChooseFrom.RemoveAt(randomIndex);
				}

			}
			else
			{
				for (int i = 0; i < list_PowerUpIndexesToChooseFrom.Count; i++)
				{
					list_PowerUpIndexesChosen.Add(list_PowerUpIndexesToChooseFrom[i]);
				}
			}
		}	
	}

	private void SetChosenPowerUpInfo()
	{
		for(int i = 0; i < list_PowerUpIndexesChosen.Count; i++)
		{
			UIManager.Instance.ui_Gameplay.panel_AbilitySelection.SetActive(true);

			UIManager.Instance.ui_Gameplay.all_AbilityInfo[i].SetMyPanel(list_PowerUpIndexesChosen[i]);
		}
	}

	public void PlayerSelectedAbility(int _abilityIndex)
	{
		ResetAbilitySelectionPanel();
		if (player.all_Powerups[_abilityIndex].gameObject.activeSelf)
		{
			// is already unlocked. Level up now
			player.all_Powerups[_abilityIndex].LevelUp();
		}
		else
		{
			// unlocked new ability, add to list.
			player.all_Powerups[_abilityIndex].gameObject.SetActive(true);

			if (player.all_Powerups[_abilityIndex].isPassivePowerUp)
			{
				// is passive power up.
				passivePowerUpSelectedCount += 1;
				list_PassivePowerUpSelectedIndexes.Add(_abilityIndex);
			}
			else
			{
				activePowerUpSelectedCount += 1;
				list_ActivePowerUpSelectedIndexes.Add(_abilityIndex);
			}
		}

		Time.timeScale = 1;

		//StartCoroutine(WaitAndSelectAgain());
	}
	private void ResetAbilitySelectionPanel()
	{
		UIManager.Instance.ui_Gameplay.panel_AbilitySelection.SetActive(false);
		for (int i = 0; i < 3; i++)
		{
			UIManager.Instance.ui_Gameplay.all_AbilityInfo[i].gameObject.SetActive(false);
		}
	}

	public void CoinCollected()
	{
		ServiceManager.Instance.dataManager.CollectedCoinInGame();
		coinsCollectedInThisRound += 1;
		ServiceManager.Instance.soundManager.PlayCoinPickupSound();
		UIManager.Instance.ui_Gameplay.IncreasegameplayCoinCount(coinsCollectedInThisRound);
	}

	public void PlayerDied()
	{
		isGameRunning = false;

        if (!isPlayerTakeRevive)
        {
			//Show Revive Screen
			UIManager.Instance.ui_Revive.gameObject.SetActive(true);
			isPlayerTakeRevive = true;
        }
        else
        {
			//Destory all things
			GameObject[] all_PlayerPowers = GameObject.FindGameObjectsWithTag(tag_PlayerPowers);

			foreach (GameObject pp in all_PlayerPowers)
			{
				Destroy(pp);
			}

			UIManager.Instance.ui_Gameplay.gameObject.SetActive(false);
            
			UIManager.Instance.ui_GameOver.gameObject.SetActive(true);
		}
	}

	//private IEnumerator WaitAndSelectAgain()
	//{
	//	yield return new WaitForSeconds(1f);
	//	ShowRandomPowerUps();
	//}

	

	public Vector3 GetPlayerCurrentPosition()
	{
		return player.transform.position;
	}
}
