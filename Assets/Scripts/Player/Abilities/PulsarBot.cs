using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsarBot : MonoBehaviour
{
	[SerializeField] private PulsarMissile missile;
	[SerializeField] private Transform spawnPosition;
	[SerializeField] private Transform gunBody;

	private Transform currentTarget;

	private int activeTimer;
	private int damage;
	private float fireRate = 0.4f;
	private float currentTimePassedForFiring;

	private float maxOffset = 7f;

	public void SetData(int _damage, int _activeTimer)
	{
		damage = _damage;
		activeTimer = _activeTimer;

		SetRandomSpawnPosition();

		Destroy(gameObject, activeTimer);
	}

	private void SetRandomSpawnPosition()
	{
		
		//float randomXPosition = Random.Range(playerPos.x + minOffset, playerPos.x + maxOffset);
		//float randomYPosition = Random.Range(playerPos.y + minOffset, playerPos.y + maxOffset);

		//int randomDirectionIndex = Random.Range(0, 2);
		//if(randomDirectionIndex == 1)
		//{
		//	randomXPosition = -randomXPosition;
		//}

		//randomDirectionIndex = Random.Range(0, 2);
		//if (randomDirectionIndex == 1)
		//{
		//	randomYPosition = -randomYPosition;
		//}

		Vector3 playerPos = GameManager.Instance.GetPlayerCurrentPosition();


		float randomXPos = Random.Range(-maxOffset, maxOffset);
		float randomYPos = Random.Range(-maxOffset, maxOffset);

		Vector3 finalPosition = new Vector3(playerPos.x + randomXPos, playerPos.y + randomYPos, 0);

		transform.position = finalPosition;
	}

	private void Update()
	{
		if (!GameManager.Instance.isGameRunning)
		{
			return;
		}

		currentTimePassedForFiring += Time.deltaTime;

		if(currentTimePassedForFiring >= fireRate)
		{
			currentTimePassedForFiring = 0f;
			FindAndAssignTarget();

			if (currentTarget != null)
			{
				// only shoot if there is an active target
				RotateShootingPointTowardsTarget();
				SpawnMissile();
			}
		}
	}

	private void FindAndAssignTarget()
	{
		
		float currentClosestDistance = 0f;

		for (int i = 0; i < GameManager.Instance.list_ActiveEnemies.Count; i++)
		{
			float distanceWithBot = Vector3.Distance(transform.position, GameManager.Instance.list_ActiveEnemies[i].position);

			if (currentClosestDistance == 0)
			{
				currentClosestDistance = distanceWithBot;
				currentTarget = GameManager.Instance.list_ActiveEnemies[i];
			}
			else
			{
				if (currentClosestDistance > distanceWithBot)
				{
					currentClosestDistance = distanceWithBot;
					currentTarget = GameManager.Instance.list_ActiveEnemies[i];
				}
			}

		}
	}

	private void RotateShootingPointTowardsTarget()
	{

		Vector3 directionToTarget = currentTarget.position - transform.position;

		// Calculate the angle in degrees for the rotation towards the target
		float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

		// Apply the rotation to the child object
		gunBody.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
	}

	private void SpawnMissile()
	{
		PulsarMissile mis = Instantiate(missile, spawnPosition.position, gunBody.rotation);
		
		int DamageToGive = (int)(damage + (damage * (GameManager.Instance.player.damageBoostPercent / 100)));
		// apply critical Chance
		int randomCritIndex = Random.Range(0, 100);

		if (randomCritIndex < GameManager.Instance.player.criticalChancePercent)
		{
			// crit
			DamageToGive = (int)(DamageToGive + (DamageToGive * (GameManager.Instance.player.criticalDamagePercent / 100)));
		}
		mis.SetData(DamageToGive);
	}
}
