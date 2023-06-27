using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelDanceData : PowerUpHandler
{
	[SerializeField] private SteelDanceController steelDance;
    private int damage;
    private int swordCount;
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
			StartCoroutine(ShootSwords());
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
		damage = AbilityManager.Instance.steelDanceData.all_Damage[currentLevel];
		swordCount = AbilityManager.Instance.steelDanceData.all_Count[currentLevel];
		fireRate = AbilityManager.Instance.steelDanceData.all_FireRate[currentLevel];
		UpdateCooldownTime();
	}

	private IEnumerator ShootSwords()
	{
		for(int i = 0; i < swordCount; i++)
		{
			if (Time.timeScale == 0)
			{
				continue;
			}

			float randomRotationZ = Random.Range(0f, 360f);

			// Apply the rotation to the GameObject
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, randomRotationZ);

			SteelDanceController steel = Instantiate(steelDance, transform.position, transform.rotation,GameManager.Instance.playerBulletSpawnParent);		

			int DamageToGive = (int)(damage + (damage * (GameManager.Instance.player.damageBoostPercent / 100)));
			// apply critical Chance
			int randomCritIndex = Random.Range(0, 100);

			if (randomCritIndex < GameManager.Instance.player.criticalChancePercent)
			{
				// crit
				DamageToGive = (int)(DamageToGive + (DamageToGive * (GameManager.Instance.player.criticalDamagePercent / 100)));
			}

			steel.SetData(DamageToGive, transform);

			yield return new WaitForSeconds(0.2f);
		}
	}

	public override string GetMyPowerName()
	{
		return AbilityManager.Instance.steelDanceData.powerUpName;
	}

	public override int GetMyCurrentLevel()
	{
		return currentLevel;
	}

	public override Sprite GetMyIcon()
	{
		return AbilityManager.Instance.steelDanceData.powerUpIcon;
	}

	public override string GetMyPowerInfo()
	{
		return AbilityManager.Instance.steelDanceData.powerUpInfo;
	}

	public override void SetUpdateInfoPanel(int _panelIndex)
	{
		int count = 0;

		int damageNew = AbilityManager.Instance.steelDanceData.all_Damage[currentLevel + 1];
		int swordCountNew = AbilityManager.Instance.steelDanceData.all_Count[currentLevel + 1];
		float fireRateNew = AbilityManager.Instance.steelDanceData.all_FireRate[currentLevel + 1];
		float fireRateOld = AbilityManager.Instance.steelDanceData.all_FireRate[currentLevel];

		if (damage != damageNew)
		{
			AbilityManager.Instance.HandleDamageIncrease(_panelIndex, count, damage, damageNew);
			count += 1;
		}

		if (swordCount != swordCountNew)
		{
			AbilityManager.Instance.HandleProjectileIncrease(_panelIndex, count, swordCount, swordCountNew);
			count += 1;
		}

		if (fireRateOld != fireRateNew)
		{
			AbilityManager.Instance.HandleFireRateDecrease(_panelIndex, count, fireRateOld, fireRateNew);
			// count += 1;
		}
	}
}
