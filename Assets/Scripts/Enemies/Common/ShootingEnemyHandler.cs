using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyHandler : EnemyAbstractData
{
	[SerializeField] private EnemyHealth health;
	[SerializeField] private ShootingEnemyController enemy;

	private void Start()
	{
		SetSortingLayer();
		SetDamage();
		SetHealth();
	}

	private void SetSortingLayer()
	{
		int layerIndex = GameManager.Instance.enemySpawnCount;
		string layerName = GameManager.Instance.all_SortingLayerName[0];

		if (layerIndex > 3500)
		{
			layerName = GameManager.Instance.all_SortingLayerName[1];
		}
		else if (layerIndex > 7000)
		{
			layerName = GameManager.Instance.all_SortingLayerName[2];
		}

		bodySprite.sortingLayerName = layerName;
		bodySprite.sortingOrder = layerIndex;
	}

	private void SetHealth()
	{
		int currentHealthValue = baseHealth;

		for(int i =0; i < GameManager.Instance.currentPlayerLevel; i++)
		{
			currentHealthValue += (int)(baseHealth * (PercentOfHealthIncreaseAccordingToLevel / 100));
		}
		health.SetInitialHealth(currentHealthValue);
	}

	private void SetDamage()
	{
		int damageValue = baseDamage;

		for (int i = 0; i < GameManager.Instance.currentPlayerLevel; i++)
		{
			damageValue += (int)(damageValue * (PercentOfDamageIncreaseAccordingToLevel / 100));

		}
		enemy.SetData(damageValue);
	}

	public override void EnemyNotAffectedByInferno()
	{
		health.NoLongerStandingOnInferno();
	}

	public override void EnemyStandingInInferno(int _damageOverTime)
	{
		health.StandingOnInferno(_damageOverTime);
	}
}
