using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrimsonMissileData : PowerUpHandler
{
    [SerializeField] private CrimsonMissileController crimsonMissile;
    [SerializeField] private Transform[] all_SpawnPoints;

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
			ShootCrimsonMissiles();
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
        damage = AbilityManager.Instance.crimsonMissileData.all_Damage[currentLevel];
        fireRate = AbilityManager.Instance.crimsonMissileData.all_FireRate[currentLevel];
		UpdateCooldownTime();
	}

	private void ShootCrimsonMissiles()
	{
		for(int i = 0; i < all_SpawnPoints.Length; i++)
		{
			CrimsonMissileController cm = Instantiate(crimsonMissile, all_SpawnPoints[i].transform.position, all_SpawnPoints[i].transform.rotation , GameManager.Instance.playerBulletSpawnParent);

			int DamageToGive = (int)(damage + (damage * (GameManager.Instance.player.damageBoostPercent / 100)));
			// apply critical Chance
			int randomCritIndex = Random.Range(0, 100);

			if (randomCritIndex < GameManager.Instance.player.criticalChancePercent)
			{
				// crit
				DamageToGive = (int)(DamageToGive + (DamageToGive * (GameManager.Instance.player.criticalDamagePercent / 100)));
			}

			cm.SetData(DamageToGive);
		}
	}

	public override string GetMyPowerName()
	{
		return AbilityManager.Instance.crimsonMissileData.powerUpName;
	}

	public override int GetMyCurrentLevel()
	{
		return currentLevel;
	}

	public override Sprite GetMyIcon()
	{
		return AbilityManager.Instance.crimsonMissileData.powerUpIcon;
	}

	public override string GetMyPowerInfo()
	{
		return AbilityManager.Instance.crimsonMissileData.powerUpInfo;
	}

	public override void SetUpdateInfoPanel(int _panelIndex)
	{
		int count = 0;

		int damageNew = AbilityManager.Instance.crimsonMissileData.all_Damage[currentLevel + 1];
		float fireRateNew = AbilityManager.Instance.crimsonMissileData.all_FireRate[currentLevel + 1];
		float fireRateOld = AbilityManager.Instance.crimsonMissileData.all_FireRate[currentLevel];

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
