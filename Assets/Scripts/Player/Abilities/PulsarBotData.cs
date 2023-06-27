using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsarBotData : PowerUpHandler
{
	[SerializeField] private PulsarBot pulsarBot;

	private int damage;
	private int activeTime;
	[SerializeField] private float spawnRate;
	private float currentTimePassedForSpawn;

	private void Start()
	{
		SetData();
		currentTimePassedForSpawn = spawnRate;
	}

	private void Update()
	{
		currentTimePassedForSpawn += Time.deltaTime;

		if(currentTimePassedForSpawn >= spawnRate)
		{
			currentTimePassedForSpawn = 0f;
			SpawnPulsarBot();
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
		damage = AbilityManager.Instance.pulsarBotData.all_Damage[currentLevel];
		activeTime = AbilityManager.Instance.pulsarBotData.all_ActiveTime[currentLevel];
		spawnRate = AbilityManager.Instance.pulsarBotData.all_SpawnRate[currentLevel];
		UpdateCooldownTime();
	}

	private void SpawnPulsarBot()
	{
		PulsarBot bot = Instantiate(pulsarBot, transform.position, pulsarBot.transform.rotation);
		bot.SetData(damage, activeTime);
	}

	public override string GetMyPowerName()
	{
		return AbilityManager.Instance.pulsarBotData.powerUpName;
	}

	public override int GetMyCurrentLevel()
	{
		return currentLevel;
	}

	public override Sprite GetMyIcon()
	{
		return AbilityManager.Instance.pulsarBotData.powerUpIcon;
	}

	public override string GetMyPowerInfo()
	{
		return AbilityManager.Instance.pulsarBotData.powerUpInfo;
	}

	public override void SetUpdateInfoPanel(int _panelIndex)
	{
		int count = 0;

		int damageNew = AbilityManager.Instance.pulsarBotData.all_Damage[currentLevel + 1];
		int activeTimeNew = AbilityManager.Instance.pulsarBotData.all_ActiveTime[currentLevel + 1];
		float spawnRateNew = AbilityManager.Instance.pulsarBotData.all_SpawnRate[currentLevel + 1];
		float spawnRateOld = AbilityManager.Instance.pulsarBotData.all_SpawnRate[currentLevel];

		if (damage != damageNew)
		{
			AbilityManager.Instance.HandleDamageIncrease(_panelIndex, count, damage, damageNew);
			count += 1;
		}

		if (activeTime != activeTimeNew)
		{
			AbilityManager.Instance.HandleActiveTimeIncrease(_panelIndex, count, activeTime, activeTimeNew);
			count += 1;
		}

		if (spawnRateOld != spawnRateNew)
		{
			AbilityManager.Instance.HandleFireRateDecrease(_panelIndex, count, spawnRateOld, spawnRateNew);
			// count += 1;
		}
	}
}
