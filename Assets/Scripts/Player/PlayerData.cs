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
		currentActivePlayerIndex = ServiceManager.Instance.dataManager.activePlayerIndex;

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

		float baseMaxHealth = PlayerDataManager.Instance.all_CharchterData[ServiceManager.Instance.dataManager.activePlayerIndex].GetPlayerHealth();


		health.flt_MaxHealth = baseMaxHealth + ((baseMaxHealth) * (maxHealthIncreasePercent / 100));
		//CalculatePlayerMaxHealth();
	}

	private void CalculatePlayerMaxHealth()
	{
		float baseMaxHealth = PlayerDataManager.Instance.all_CharchterData[ServiceManager.Instance.dataManager.activePlayerIndex].GetPlayerHealth();

		maxHealthIncreasePercent = PassiveUpgradeManager.Instance.all_PassiveData[1].GetMyPercentage();


		Debug.Log("Base Max Heealth : " + baseMaxHealth + " increase Per : " + maxHealthIncreasePercent + " Final Healht : " + (baseMaxHealth) * (maxHealthIncreasePercent / 100));

		health.flt_MaxHealth = baseMaxHealth + ((baseMaxHealth) * (maxHealthIncreasePercent / 100));
	}
	#endregion


	#region Damage For Player

	public void UpdateDamageForPlayer(float _percentage)
	{
		damageBoostPercent += _percentage;

		float baseDamage = PlayerDataManager.Instance.all_CharchterData[ServiceManager.Instance.dataManager.activePlayerIndex].GetPlayerDamage();

		shooting.normalPowerDamage = (int)baseDamage + (int)(baseDamage * (damageBoostPercent / 100));
		//CalculatePlayerDamage();

	}
	private void CalculatePlayerDamage()
	{
		float baseDamage = PlayerDataManager.Instance.all_CharchterData[ServiceManager.Instance.dataManager.activePlayerIndex].GetPlayerDamage();
		damageBoostPercent = PassiveUpgradeManager.Instance.all_PassiveData[3].GetMyPercentage();

		Debug.Log("Base Max Damage : " + baseDamage + " increase Per : " + damageBoostPercent + " Final Damage : " + (int)(baseDamage * (damageBoostPercent / 100)));

		shooting.normalPowerDamage = (int)baseDamage + (int)(baseDamage * (damageBoostPercent / 100));
	}

	#endregion

	#region MoveSpeed FOr Player

	public void UpdateMovementSpeedForPlayer(float _percentage)
	{
		movementSpeedPercent += _percentage;

		float baseMoveSpeed = PlayerDataManager.Instance.all_CharchterData[currentActivePlayerIndex].GetPlayerMovementSpeed();

		movement.flt_MoveSpeed = baseMoveSpeed + (baseMoveSpeed * (movementSpeedPercent / 100));

		//CalculatePlayerMovementSpeed();
	}

	private void CalculatePlayerMovementSpeed()
	{
		float baseMoveSpeed = PlayerDataManager.Instance.all_CharchterData[currentActivePlayerIndex].GetPlayerMovementSpeed();

		movementSpeedPercent = PassiveUpgradeManager.Instance.all_PassiveData[2].GetMyPercentage();

		Debug.Log("Base Max MoveSpeed : " + baseMoveSpeed + " increase Per : " + movementSpeedPercent + " Final MoveSpeed : " + (baseMoveSpeed * (movementSpeedPercent / 100)));

		movement.flt_MoveSpeed = baseMoveSpeed + (baseMoveSpeed * (movementSpeedPercent / 100));



	}
	#endregion


	#region Firerate For Player

	public void UpdateFireRateForPlayer(float _percentage)
	{
		fireRateReducePercentage += _percentage;

		float baseFirerate = PlayerDataManager.Instance.all_CharchterData[ServiceManager.Instance.dataManager.activePlayerIndex].GetPlayerFirerate();

		shooting.currentFireRate = baseFirerate - (baseFirerate * (fireRateReducePercentage / 100));

		// reduce player shooting firerate
		//CalculatePlayerFirerate();
	}

	private void CalculatePlayerFirerate()
	{
		Debug.Log("INDEX: " + ServiceManager.Instance.dataManager.activePlayerIndex);

		float baseFirerate = PlayerDataManager.Instance.all_CharchterData[ServiceManager.Instance.dataManager.activePlayerIndex].GetPlayerFirerate();
		fireRateReducePercentage = PassiveUpgradeManager.Instance.all_PassiveData[0].GetMyPercentage();

		Debug.Log("Base Max Firerate : " + baseFirerate + " increase Per : " + fireRateReducePercentage + " Final Firerate : " + (baseFirerate * (fireRateReducePercentage / 100)));

		shooting.maxFireRate = baseFirerate - (baseFirerate * (fireRateReducePercentage / 100));


	}

	#endregion

	#region Critical Damage

	public void UpdateCriticalDamageForPlayer(int _percentage)
	{
		criticalDamagePercent += _percentage;
		Debug.Log("Critical Damage Method Called");
		///CalculateCriticalDamageForPlayer();
	}

	private void CalculateCriticalDamageForPlayer()
	{
		float baseCriticalDamage = PlayerDataManager.Instance.all_CharchterData[currentActivePlayerIndex].GetCriticalDamage();

		float criticalDamage = PassiveUpgradeManager.Instance.all_PassiveData[5].GetMyPercentage();

		criticalDamagePercent = baseCriticalDamage + (baseCriticalDamage * (criticalDamage / 100));



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

		criticalChancePercent = (int)baseCriticalChance + (int)(baseCriticalChance * (criticalChance / 100));

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

		for (int i = 0; i < all_Powerups.Length; i++)
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
		Debug.Log("Power up Index" + _powerUpIndex);
		return all_Powerups[_powerUpIndex].currentLevel;
	}

	public void TakeDamageFromEnemy(int _amount)
	{
		health.TakeDamage(_amount);
	}

}
