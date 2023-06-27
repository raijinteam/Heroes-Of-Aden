using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickingTerrorData : PowerUpHandler
{
    [SerializeField] private TickingTerrorController tickingTerror;
    private int damage;
    [SerializeField] private float fireRate;
	private float currentTimePassed = 0f;

	private void Start()
	{
		SetData();
		currentTimePassed = fireRate;
	}

	private void Update()
	{
		currentTimePassed += Time.deltaTime;

		if(currentTimePassed >= fireRate)
		{
			currentTimePassed = 0f;
			SpawnTickingTerror();
		}
	}

	public override void UpdateCooldownTime()
	{
		fireRate = fireRate - (fireRate * (GameManager.Instance.player.cooldownReductionPercentage / 100));
	}

	public override void LevelUp()
	{
		currentLevel += 1;
		SetData();
	}

	private void SetData()
	{
		damage = AbilityManager.Instance.tickingTerrorData.all_Damage[currentLevel];
		fireRate = AbilityManager.Instance.tickingTerrorData.all_FireRate[currentLevel];
		UpdateCooldownTime();
	}

	private void SpawnTickingTerror()
	{
		TickingTerrorController ttc = Instantiate(tickingTerror, transform.position, tickingTerror.transform.rotation , GameManager.Instance.playerBulletSpawnParent);
		
		int DamageToGive = (int)(damage + (damage * (GameManager.Instance.player.damageBoostPercent / 100)));
		// apply critical Chance
		int randomCritIndex = Random.Range(0, 100);

		if (randomCritIndex < GameManager.Instance.player.criticalChancePercent)
		{
			// crit
			DamageToGive = (int)(DamageToGive + (DamageToGive * (GameManager.Instance.player.criticalDamagePercent / 100)));
		}
		ttc.SetData(DamageToGive);
	}

	public override string GetMyPowerName()
	{
		return AbilityManager.Instance.tickingTerrorData.powerUpName;
	}

	public override int GetMyCurrentLevel()
	{
		return currentLevel;
	}

	public override Sprite GetMyIcon()
	{
		return AbilityManager.Instance.tickingTerrorData.powerUpIcon;
	}

	public override string GetMyPowerInfo()
	{
		return AbilityManager.Instance.tickingTerrorData.powerUpInfo;
	}

	public override void SetUpdateInfoPanel(int _panelIndex)
	{
		int count = 0;

		int damageNew = AbilityManager.Instance.tickingTerrorData.all_Damage[currentLevel + 1];
		float fireRateNew = AbilityManager.Instance.tickingTerrorData.all_FireRate[currentLevel + 1];
		float fireRateOld = AbilityManager.Instance.tickingTerrorData.all_FireRate[currentLevel];

		if (damage != damageNew)
		{
			AbilityManager.Instance.HandleDamageIncrease(_panelIndex, count, damage, damageNew);
			count += 1;
		}

		if (fireRateOld != fireRateNew)
		{
			AbilityManager.Instance.HandleFireRateDecrease(_panelIndex, count, fireRateOld, fireRateNew);
			// count += 1;
		}
	}
}
