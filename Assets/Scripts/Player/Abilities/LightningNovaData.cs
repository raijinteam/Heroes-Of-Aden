using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningNovaData : PowerUpHandler
{
	[SerializeField] private LightningNovaController nova;

	private int damage;
	[SerializeField] private float spawnRate;
	private float currentTimePassed = 0f;

	private void Start()
	{
		nova.gameObject.SetActive(true);
		SetData();
		currentTimePassed = spawnRate;
	}

	private void Update()
	{
		currentTimePassed += Time.deltaTime;

		if(currentTimePassed >= spawnRate)
		{
			currentTimePassed = 0f;
			LightningNovaAttack();
		}
	}

	public override void UpdateCooldownTime()
	{
		spawnRate = spawnRate - (spawnRate * (GameManager.Instance.player.cooldownReductionPercentage / 100));
	}
	public override void LevelUp()
	{
		currentLevel += 1;
		SetData();
	}

	private void SetData()
	{
		damage = AbilityManager.Instance.lightningNovaData.all_Damage[currentLevel];
		spawnRate = AbilityManager.Instance.lightningNovaData.all_SpawnRate[currentLevel];
		UpdateCooldownTime();
	}

	private void LightningNovaAttack()
	{
		int DamageToGive = (int)(damage + (damage * (GameManager.Instance.player.damageBoostPercent / 100)));
		// apply critical Chance
		int randomCritIndex = Random.Range(0, 100);

		if (randomCritIndex < GameManager.Instance.player.criticalChancePercent)
		{
			// crit
			DamageToGive = (int)(DamageToGive + (DamageToGive * (GameManager.Instance.player.criticalDamagePercent / 100)));
		}
		nova.SetData(DamageToGive);
	}

	public override string GetMyPowerName()
	{
		return AbilityManager.Instance.lightningNovaData.powerUpName;
	}

	public override int GetMyCurrentLevel()
	{
		return currentLevel;
	}

	public override Sprite GetMyIcon()
	{
		return AbilityManager.Instance.lightningNovaData.powerUpIcon;
	}

	public override string GetMyPowerInfo()
	{
		return AbilityManager.Instance.lightningNovaData.powerUpInfo;
	}

	public override void SetUpdateInfoPanel(int _panelIndex)
	{
		int count = 0;

		int damageNew = AbilityManager.Instance.lightningNovaData.all_Damage[currentLevel + 1];
		float spawnRateNew = AbilityManager.Instance.lightningNovaData.all_SpawnRate[currentLevel + 1];
		float spawnRateOld = AbilityManager.Instance.lightningNovaData.all_SpawnRate[currentLevel];

		if (damage != damageNew)
		{
			AbilityManager.Instance.HandleDamageIncrease(_panelIndex, count, damage, damageNew);
			count += 1;
		}

		if (spawnRateOld != spawnRateNew)
		{
			AbilityManager.Instance.HandleSpawneRateDecrease(_panelIndex, count, spawnRateOld, spawnRateNew);
			// count += 1;
		}
	}
}
