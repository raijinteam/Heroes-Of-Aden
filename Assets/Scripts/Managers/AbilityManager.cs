using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
	public static AbilityManager Instance;

	private void Awake()
	{
		Instance = this;
	}

	public int abilityMaxLevel;

	[Header("Upgrade Names")]
	private string str_Damage = "Damage";
	private string str_ProjectileCount = "Proj. Count";
	private string str_FireRate = "FireRate";
	private string str_SpawnRate = "Cooldown";
	private string str_AliveTime = "Alive Time";
	private string str_MaxHealth = "Max Health";
	private string str_Regen = "Regen";
	private string str_CritialChance = "Crit. Chance";
	private string str_CritialDamage = "Crit. Damage";
	private string str_MovementSpeed = "Move Speed";

	[Header("POWER UP SCRIPTS")]
	public ShatterstormManager shatterStormData;
	public JadeDefenseManager jadeDefenseData;
	public InfernoNadeManager infernoNadeData;
	public ThrusterManager thrusterData;
	public PhantomStrikeManager phantomStrikeData;
	public SteelDanceManager steelDanceData;
	public PulsarBotManager pulsarBotData;
	public TickingTerrorManager tickingTerrorData;
	public LightningNovaManager lightningNovaData;
	public CrimsonMissileManager crimsonMissileData;

	[Header("Passive Power Up Scripts")]
	public FireRateManager fireRateData;
	public MaxHealthManager maxHealthData;
	public HPRegenManager hpRegenData;
	public DamageManager damageData;
	public CriticalStrikeChanceManager criticalStrikeChanceData;
	public CriticalDamageManager criticalDamageData;
	public PowerupCooldownManager powerupCooldownData;
	public MovementSpeedManager movementSpeed;

	//For Deubbing Only


	public void HandleDamageIncrease(int _panelIndex, int _count, int _damage, int _newDamage)
	{
		string oldDamage = _damage.ToString("F1");

		int damageDifference = _newDamage - _damage;
		string newDamageUpgrade = "+ " + damageDifference.ToString("F1");

		UIManager.Instance.ui_Gameplay.all_AbilityInfo[_panelIndex].SetMyUpdatePanel(_count, str_Damage, oldDamage, newDamageUpgrade);
	}

	public void HandleProjectileIncrease(int _panelIndex, int _count, int _oldCount, int _newCount)
	{
		string oldCount = _oldCount.ToString("F1");

		int difference = _newCount - _oldCount;
		string str_Difference = "+ " + difference.ToString("F1");

		UIManager.Instance.ui_Gameplay.all_AbilityInfo[_panelIndex].SetMyUpdatePanel(_count, str_ProjectileCount, oldCount, str_Difference);
	}

	public void HandleFireRateDecrease(int _panelIndex, int _count, float _oldFireRate, float _newFireRate)
	{
		string oldFireRate = _oldFireRate.ToString("F1") + "s";

		float difference = _oldFireRate - _newFireRate;
		string str_Difference = "- " + difference.ToString("F1") + "s";

		UIManager.Instance.ui_Gameplay.all_AbilityInfo[_panelIndex].SetMyUpdatePanel(_count, str_FireRate, oldFireRate, str_Difference);
	}

	public void HandleSpawneRateDecrease(int _panelIndex, int _count, float _oldSpawnRate, float _newSpawnRate)
	{
		string oldSpawnRate = _oldSpawnRate.ToString("F1") + "s";

		float difference = _oldSpawnRate - _newSpawnRate;
		string str_Difference = "- " + difference.ToString("F1") + "s";

		UIManager.Instance.ui_Gameplay.all_AbilityInfo[_panelIndex].SetMyUpdatePanel(_count, str_SpawnRate, oldSpawnRate, str_Difference);
	}

	public void HandleActiveTimeIncrease(int _panelIndex, int _count, float _oldActiveTime, float _newActiveTime)
	{
		string oldActiveTime = _oldActiveTime.ToString("F1") + "s";

		float difference = _newActiveTime - _oldActiveTime;
		string str_Difference = "+ " + difference.ToString("F1") + "s";

		UIManager.Instance.ui_Gameplay.all_AbilityInfo[_panelIndex].SetMyUpdatePanel(_count, str_AliveTime, oldActiveTime, str_Difference);
	}

	public void HandleFireRatePercentIncrease(int _panelIndex, int _count, float _oldFireRate, float _newFireRate)
	{
		string oldFireRate = _oldFireRate.ToString("F1") + "%";

		float difference = _newFireRate - _oldFireRate;
		string str_Difference = "+ " + difference.ToString("F1") + "%";

		UIManager.Instance.ui_Gameplay.all_AbilityInfo[_panelIndex].SetMyUpdatePanel(_count, str_FireRate, oldFireRate, str_Difference);
	}

	public void HandleMaxHPPercentIncrease(int _panelIndex, int _count, float _oldMaxHealth, float _newMaxHealth)
	{
		string oldFireRate = _oldMaxHealth.ToString("F1") + "%";

		float difference = _newMaxHealth - _oldMaxHealth;
		string str_Difference = "+ " + difference.ToString("F1") + "%";

		UIManager.Instance.ui_Gameplay.all_AbilityInfo[_panelIndex].SetMyUpdatePanel(_count, str_MaxHealth, oldFireRate, str_Difference);
	}

	public void HandleRegenPercentIncrease(int _panelIndex, int _count, float _oldRegen, float _newRegen)
	{
		string oldFireRate = _oldRegen.ToString("F1") + "%";

		float difference = _newRegen - _oldRegen;
		string str_Difference = "+ " + difference.ToString("F1") + "%";

		UIManager.Instance.ui_Gameplay.all_AbilityInfo[_panelIndex].SetMyUpdatePanel(_count, str_Regen, oldFireRate, str_Difference);
	}

	public void HandleDamagePercentIncrease(int _panelIndex, int _count, float _oldDamage, float _newDamage)
	{
		string oldFireRate = _oldDamage.ToString("F1") + "%";

		float difference = _newDamage - _oldDamage;
		string str_Difference = "+ " + difference.ToString("F1") + "%";

		UIManager.Instance.ui_Gameplay.all_AbilityInfo[_panelIndex].SetMyUpdatePanel(_count, str_Damage, oldFireRate, str_Difference);
	}

	public void HandleCriticalChanceIncrease(int _panelIndex, int _count, float _oldChance, float _newChance)
	{
		string oldFireRate = _oldChance.ToString("F1") + "%";

		float difference = _newChance - _oldChance;
		string str_Difference = "+ " + difference.ToString("F1") + "%";

		UIManager.Instance.ui_Gameplay.all_AbilityInfo[_panelIndex].SetMyUpdatePanel(_count, str_CritialChance, oldFireRate, str_Difference);
	}
	public void HandleCriticalDamageIncrease(int _panelIndex, int _count, float _oldDamage, float _newDamage)
	{
		string oldFireRate = _oldDamage.ToString("F1") + "%";

		float difference = _newDamage - _oldDamage;
		string str_Difference = "+ " + difference.ToString("F1") + "%";

		UIManager.Instance.ui_Gameplay.all_AbilityInfo[_panelIndex].SetMyUpdatePanel(_count, str_CritialDamage, oldFireRate, str_Difference);
	}

	public void HandlePowerupCooldownIncrease(int _panelIndex, int _count, float _oldValue, float _newValue)
	{
		string oldFireRate = _oldValue.ToString("F1") + "%";

		float difference = _newValue - _oldValue;
		string str_Difference = "+ " + difference.ToString("F1") + "%";

		UIManager.Instance.ui_Gameplay.all_AbilityInfo[_panelIndex].SetMyUpdatePanel(_count, str_SpawnRate, oldFireRate, str_Difference);
	}

	public void HandleMovementSpeedIncrease(int _panelIndex, int _count, float _oldValue, float _newValue)
    {
		string oldMS = _oldValue.ToString("F1") + "%";

		float difference = _newValue - _oldValue;
		string str_Difference = "+ " + difference.ToString("F1") + "ms";

		UIManager.Instance.ui_Gameplay.all_AbilityInfo[_panelIndex].SetMyUpdatePanel(_count, str_MovementSpeed, oldMS, str_Difference);
	}
}
