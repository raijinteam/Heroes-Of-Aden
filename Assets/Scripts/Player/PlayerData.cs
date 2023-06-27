using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
	[Header("PowerUp Handler")]
	public PowerUpHandler[] all_Powerups;

	public int currentActivePlayerIndex;

	[Header("Common Scripts")]
	public PlayerMovement movement;
	public PlayerHealth health;
	public PlayerShooting shooting;

	//public ShatterstormData shatterStorm;
	//public JadeDefenseData jade;
	//public InfernoNadeData infernoNade;
	//public ThrusterData thruster;
	//public PhantomStrikeData phantom;
	//public SteelDanceData steelDance;
	//public PulsarBotData pulsarBot;
	//public TickingTerrorData tickingTerror;
	//public LightningNovaData lightningNova;
	//public CrimsonMissileData crimsonMissile;



	[Header("Universal Powerup Upgrade Data")]
	public float damageBoostPercent;
	public int criticalChancePercent;
	public float criticalDamagePercent;
	public float cooldownReductionPercentage = 0;
	public float fireRateReducePercentage;
	public float maxHealthIncreasePercent;
	public float movementSpeedPercent;

	private void Start()
	{
		currentActivePlayerIndex = DataManager.Instance.activePlayerIndex;

		CalculatePlayerMaxHealth();

		CalculatePlayerFirerate();

		CalculatePlayerDamage();

		CalculatePlayerMovementSpeed();

		CalculateCriticalChance();

		CalculateCriticalDamageForPlayer();

		CalculateCooldownTime();

	}


	#region Max Health For Player

	public void UpdateMaxHealthForPlayer(float _percentage)
    {
		maxHealthIncreasePercent += _percentage;

		CalculatePlayerMaxHealth();
	}

	private void CalculatePlayerMaxHealth()
    {
		float baseMaxHealth = PlayerDataManager.Instance.all_CharchterData[DataManager.Instance.activePlayerIndex].GetPlayerHealth();

		maxHealthIncreasePercent = PassiveUpgradeManager.Instance.all_PassiveData[1].GetMyPercentage();

		health.flt_MaxHealth = baseMaxHealth + maxHealthIncreasePercent;

    }
    #endregion


    #region Damage For Player

    public void UpdateDamageForPlayer(float _percentage)
    {
		damageBoostPercent += _percentage;
		//CalculatePlayerDamage();

	}
	private void CalculatePlayerDamage()
    {
		float baseDamage = PlayerDataManager.Instance.all_CharchterData[DataManager.Instance.activePlayerIndex].GetPlayerDamage();
		damageBoostPercent = PassiveUpgradeManager.Instance.all_PassiveData[3].GetMyPercentage();

		shooting.normalPowerDamage = (int)baseDamage + (int)damageBoostPercent;


	}

    #endregion

    #region MoveSpeed FOr Player

    public void UpdateMovementSpeedForPlayer(float _percentage)
    {
		movementSpeedPercent += _percentage;
		//CalculatePlayerMovementSpeed();
    }

	private void CalculatePlayerMovementSpeed()
    {
		float baseMoveSpeed = PlayerDataManager.Instance.all_CharchterData[currentActivePlayerIndex].GetPlayerMovementSpeed();

		movementSpeedPercent = PassiveUpgradeManager.Instance.all_PassiveData[2].GetMyPercentage();

		movement.flt_MoveSpeed = baseMoveSpeed + movementSpeedPercent;

		

	}
    #endregion


    #region Firerate For Player

    public void UpdateFireRateForPlayer(float _percentage)
    {
		fireRateReducePercentage += _percentage;
		// reduce player shooting firerate
		//CalculatePlayerFirerate();
	}

	private void CalculatePlayerFirerate()
    {
		float baseFirerate = PlayerDataManager.Instance.all_CharchterData[DataManager.Instance.activePlayerIndex].GetPlayerFirerate();
		fireRateReducePercentage = PassiveUpgradeManager.Instance.all_PassiveData[0].GetMyPercentage();

		shooting.currentFireRate = baseFirerate - fireRateReducePercentage;


	}

    #endregion

    #region Critical Damage

	public void UpdateCriticalDamageForPlayer(int _percentage)
    {
		criticalDamagePercent += _percentage;
		///CalculateCriticalDamageForPlayer();
    }

	private void CalculateCriticalDamageForPlayer()
    {
		float baseCriticalDamage = PlayerDataManager.Instance.all_CharchterData[currentActivePlayerIndex].GetCriticalDamage();

		float criticalDamage = PassiveUpgradeManager.Instance.all_PassiveData[5].GetMyPercentage();

		criticalDamagePercent = baseCriticalDamage + criticalDamage;


		
	}

    #endregion

    #region Critical Chance For Player

	public void UpdateCriticalChance(int _percentage)
    {
		criticalChancePercent += _percentage;
		//CalculateCriticalChance();
    }

	private void CalculateCriticalChance()
    {
		float baseCriticalChance = PlayerDataManager.Instance.all_CharchterData[currentActivePlayerIndex].GetCriticalChance();

		int criticalChance = (int)PassiveUpgradeManager.Instance.all_PassiveData[4].GetMyPercentage();

		criticalChancePercent = (int)baseCriticalChance + criticalChance;

	}

    #endregion



	private void CalculateCooldownTime()
    {
		//float basetime = PlayerDataManager.Instance.all_CharchterData[currentActivePlayerIndex].GetPowerCooldownTime();
		float cooldowntTime = PassiveUpgradeManager.Instance.all_PassiveData[6].GetMyPercentage();

		cooldownReductionPercentage = cooldowntTime;

		//UpdateCooldownReductionPercentageValue(cooldowntTime);
	}


    public void UpdateCooldownReductionPercentageValue(float _reductionPercentage)
	{	
		cooldownReductionPercentage += _reductionPercentage;

		for(int i = 0; i < all_Powerups.Length; i++)
        {
			all_Powerups[i].UpdateCooldownTime();
        }
		//shatterStorm.UpdateCooldownTime();
		//infernoNade.UpdateCooldownTime();
		//thruster.UpdateCooldownTime();
		//phantom.UpdateCooldownTime();
		//steelDance.UpdateCooldownTime();
		//pulsarBot.UpdateCooldownTime();
		//tickingTerror.UpdateCooldownTime();
		//lightningNova.UpdateCooldownTime();
		//crimsonMissile.UpdateCooldownTime();
	}

	public int GetMyPowerupLevel(int _powerUpIndex)
    {
		return all_Powerups[_powerUpIndex].currentLevel;
    }

	public void TakeDamageFromEnemy(int _amount)
	{
		health.TakeDamage(_amount);
	}
	
}
