using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterData : PowerUpHandler
{
    [SerializeField] private ThrusterController thruster;

	private int damage;
	private int thrusterCount;
	[SerializeField] private float fireRate;
	private float currentTimePassed;
	private bool isShootingThrusters = false;

	private float maxXOffset = 8f;
	private float maxYOffset = 8f;

	private void Start()
	{
		SetData();
		currentTimePassed = fireRate;
	}

	private void Update()
	{
		if (!GameManager.Instance.isGameRunning)
		{
			return;
		}

		if (isShootingThrusters)
		{
			return;
		}

		currentTimePassed += Time.deltaTime;
		if(currentTimePassed >= fireRate)
		{
			currentTimePassed = 0f;
			isShootingThrusters = true;

			StartCoroutine(ShootDownThrusters());
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
		damage = AbilityManager.Instance.thrusterData.all_DamageValues[currentLevel];
		thrusterCount = AbilityManager.Instance.thrusterData.all_CountValues[currentLevel];
		fireRate = AbilityManager.Instance.thrusterData.all_FireRate[currentLevel];
		UpdateCooldownTime();
	}

	private IEnumerator ShootDownThrusters()
	{
		for(int i = 0; i < thrusterCount; i++)
		{
			if (Time.timeScale == 0)
			{
				continue;
			}

			if (!GameManager.Instance.isGameRunning)
			{
				continue;
			}

			Vector3 playerPos = GameManager.Instance.GetPlayerCurrentPosition();

			float randomXPos = Random.Range(-maxXOffset, maxXOffset);
			float randomYPos = Random.Range(-maxYOffset, maxYOffset);

			Vector3 finalPosition = new Vector3(playerPos.x + randomXPos, playerPos.y + randomYPos, 0);

			ThrusterController tc = Instantiate(thruster, finalPosition, thruster.transform.rotation , GameManager.Instance.playerBulletSpawnParent);

			// check for damage boost;
			int DamageToGive = (int)(damage + (damage * (GameManager.Instance.player.damageBoostPercent / 100)));
			// apply critical Chance
			int randomCritIndex = Random.Range(0, 100);

			if (randomCritIndex < GameManager.Instance.player.criticalChancePercent)
			{
				// crit
				DamageToGive = (int)(DamageToGive + (DamageToGive * (GameManager.Instance.player.criticalDamagePercent / 100)));
			}


			tc.SetData(DamageToGive);

			yield return new WaitForSeconds(0.2f);
		}

		isShootingThrusters = false;
	}

	public override string GetMyPowerName()
	{
		return AbilityManager.Instance.thrusterData.powerUpName;
	}

	public override int GetMyCurrentLevel()
	{
		return currentLevel;
	}

	public override Sprite GetMyIcon()
	{
		return AbilityManager.Instance.thrusterData.powerUpIcon;
	}

	public override string GetMyPowerInfo()
	{
		return AbilityManager.Instance.thrusterData.powerUpInfo;
	}

	public override void SetUpdateInfoPanel(int _panelIndex)
	{
		int count = 0;
		int damageNew = AbilityManager.Instance.thrusterData.all_DamageValues[currentLevel + 1];
		int thrusterCountNew = AbilityManager.Instance.thrusterData.all_CountValues[currentLevel + 1];
		float fireRateNew = AbilityManager.Instance.thrusterData.all_FireRate[currentLevel + 1];
		float fireRateOld = AbilityManager.Instance.thrusterData.all_FireRate[currentLevel];


		if (damage != damageNew)
		{
			AbilityManager.Instance.HandleDamageIncrease(_panelIndex, count, damage, damageNew);
			count += 1;
		}

		if (thrusterCount != thrusterCountNew)
		{
			AbilityManager.Instance.HandleProjectileIncrease(_panelIndex, count, thrusterCount, thrusterCountNew);
			count += 1;
		}

		if (fireRateOld != fireRateNew)
		{
			AbilityManager.Instance.HandleFireRateDecrease(_panelIndex, count, fireRateOld, fireRateNew);
			// count += 1;
		}
	}
}
