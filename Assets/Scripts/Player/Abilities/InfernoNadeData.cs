using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfernoNadeData : PowerUpHandler
{
	[SerializeField] private InfernoNadeController nade;


	[SerializeField] private int damageOverTime;
	[SerializeField] private int projectileCount;
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

		if (currentTimePassed >= fireRate)
		{
			currentTimePassed = 0f;
			StartCoroutine(ThrowNadeAtRandomLocation());
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
		damageOverTime = AbilityManager.Instance.infernoNadeData.all_DamageOverTime[currentLevel];
		projectileCount = AbilityManager.Instance.infernoNadeData.all_NadeCount[currentLevel];
		fireRate = AbilityManager.Instance.infernoNadeData.all_SpawnRate[currentLevel];
		UpdateCooldownTime();
	}


	private IEnumerator ThrowNadeAtRandomLocation()
	{
		for (int i = 0; i < projectileCount; i++)
		{
			if(Time.timeScale == 0)
			{
				continue;
			}

			InfernoNadeController nde = Instantiate(nade, transform.position, nade.transform.rotation);
		
			// check for damage boost;
			int DamageToGive = (int)(damageOverTime + (damageOverTime * (GameManager.Instance.player.damageBoostPercent / 100)));
			// apply critical Chance
			int randomCritIndex = Random.Range(0, 100);

			if (randomCritIndex < GameManager.Instance.player.criticalChancePercent)
			{
				// crit
				DamageToGive = (int)(DamageToGive + (DamageToGive * (GameManager.Instance.player.criticalDamagePercent / 100)));
			}

			nde.SetDamageOverTime(DamageToGive);
			yield return new WaitForSeconds(0.2f);
		}
	}

	public override string GetMyPowerName()
	{
		return AbilityManager.Instance.infernoNadeData.powerUpName;
	}

	public override int GetMyCurrentLevel()
	{
		return currentLevel;
	}

	public override Sprite GetMyIcon()
	{
		return AbilityManager.Instance.infernoNadeData.powerUpIcon;
	}

	public override string GetMyPowerInfo()
	{
		return AbilityManager.Instance.infernoNadeData.powerUpInfo;
	}

	public override void SetUpdateInfoPanel(int _panelIndex)
	{
		int count = 0;

		int damageOverTimeNew = AbilityManager.Instance.infernoNadeData.all_DamageOverTime[currentLevel + 1];
		int projectileCountNew = AbilityManager.Instance.infernoNadeData.all_NadeCount[currentLevel + 1];
		float fireRateNew = AbilityManager.Instance.infernoNadeData.all_SpawnRate[currentLevel + 1];
		float fireRateOld= AbilityManager.Instance.infernoNadeData.all_SpawnRate[currentLevel];

		if (damageOverTime != damageOverTimeNew)
		{
			AbilityManager.Instance.HandleDamageIncrease(_panelIndex, count, damageOverTime, damageOverTimeNew);
			count += 1;
		}

		if (projectileCount != projectileCountNew)
		{
			AbilityManager.Instance.HandleProjectileIncrease(_panelIndex, count, projectileCount, projectileCountNew);
			count += 1;
		}

		if (fireRateOld != fireRateNew)
		{
			AbilityManager.Instance.HandleFireRateDecrease(_panelIndex, count, fireRateOld, fireRateNew);
			// count += 1;
		}
	}
}
