using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemyHandler : EnemyAbstractData
{
	[SerializeField] ChasingEnemyController enemy;
	[SerializeField] EnemyHealth health;

	private void Start()
	{
		SetSortingLayer();
		SetDamage();
		SetHealth();
	}

	private void SetHealth()
	{
		int currentHealthValue = baseHealth;

		for (int i = 0; i < GameManager.Instance.currentPlayerLevel; i++)
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
		enemy.SetDamage(damageValue);
	}

	public override void EnemyNotAffectedByInferno()
	{
		health.NoLongerStandingOnInferno();
	}

	public override void EnemyStandingInInferno(int _damageOverTime)
	{
		health.StandingOnInferno(_damageOverTime);
	}

	private void SetSortingLayer()
	{
		int layerIndex = GameManager.Instance.enemySpawnCount;
		string layerName = GameManager.Instance.all_SortingLayerName[0];

		if(layerIndex > 3500)
		{
			layerName = GameManager.Instance.all_SortingLayerName[1];
		}
		else if(layerIndex > 7000)
		{
			layerName = GameManager.Instance.all_SortingLayerName[2];
		}

		bodySprite.sortingLayerName = layerName;
		bodySprite.sortingOrder = layerIndex; 
	}

	//public override void PlayerInRange()
	//{
	//	enemy.isPlayerInRange = true;
	//}

	//public override void PlayerOutOfRange()
	//{
	//	enemy.isPlayerInRange = false;
	//}

	public void PlayerInRange()
	{
		enemy.isPlayerInRange = true;
	}

	public void PlayerOutOfRange()
	{
		enemy.isPlayerInRange = false;
	}
}
