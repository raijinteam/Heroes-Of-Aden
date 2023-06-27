using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShootingEnemySpawning : MonoBehaviour
{
	[Header("NORMAL ENEMY SINGLE")]
	[SerializeField] private int currentEnemyIndex;
	[SerializeField] private float spawnRate;
	private float currentTimePassedForSpawning = 0f;
	private float rateDecreaseMultiplier = 0.8f;
	private float minimumSpawnRate = 1f;

	public void ReduceSpawnRate()
	{
		spawnRate = spawnRate * rateDecreaseMultiplier;

		if (spawnRate < minimumSpawnRate)
		{
			spawnRate = minimumSpawnRate;
		}
	}

	public void ShootingEnemySpawnHandling()
	{
		currentTimePassedForSpawning += Time.deltaTime;

		if (currentTimePassedForSpawning >= spawnRate)
		{
			currentTimePassedForSpawning = 0f;
			SpawnNormalEnemyAroundPlayer();
		}

		
	}

	private void SpawnNormalEnemyAroundPlayer()
	{
		Vector3 randomPosition = GameManager.Instance.GetPlayerCurrentPosition();

		float randomXPos = Random.Range(5f, 10f);
		float randomYPos = Random.Range(5f, 10f);

		int randomSideIndex = Random.Range(0, 2);
		if (randomSideIndex == 1)
		{
			// spawn x negative
			randomXPos = -randomXPos;
		}

		randomSideIndex = Random.Range(0, 2);
		if (randomSideIndex == 1)
		{
			// spawn y negative
			randomYPos = -randomYPos;
		}

		randomPosition.x += randomXPos;
		randomPosition.y += randomYPos;

		GameObject enemy = Instantiate(EnemyManager.Instance.enemyShooting[currentEnemyIndex], randomPosition, Quaternion.identity, EnemyManager.Instance.enemySpawnParent);
		GameManager.Instance.AddEnemyToActiveList(enemy.transform);
	}

	public void IncreaseEnemyLevel()
	{
		ReduceSpawnRate();

		if (currentEnemyIndex == EnemyManager.Instance.enemyOne.Length - 1)
		{
			return; // already at max level
		}

		currentEnemyIndex += 1;
	}
}
