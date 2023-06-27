using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSurgeSpawn : MonoBehaviour
{
	[SerializeField] private int currentEnemyIndex;
	[SerializeField] private float SpawnRate;
	private float currentTimePassed = 0f;

	private bool isGoblinSurgeOn = false;
	private float goblinSurgeSpawnRate = 0.1f;
	private float currentTimePassedSurge = 0f;
	private int maxGoblinSpawnCount;
	private int currentSpawnCount = 0;
	private float rateDecreaseMultiplier = 0.8f;
	private float minimumSpawnRate = 10f;

	public void ReduceSpawnRate()
	{
		SpawnRate = SpawnRate * rateDecreaseMultiplier;

		if (SpawnRate < minimumSpawnRate)
		{
			SpawnRate = minimumSpawnRate;
		}
	}

	public void GoblinSurgeHandling()
	{
		if(GameManager.Instance.currentPlayerLevel < 7)
		{
			return; // start spawn after level 5
		}

		if (isGoblinSurgeOn)
		{
			GoblinSpawnInSurge();
		}
		else
		{
			currentTimePassed += Time.deltaTime;

			if (currentTimePassed >= SpawnRate)
			{
				isGoblinSurgeOn = true;
				currentSpawnCount = 0;
				maxGoblinSpawnCount = Random.Range(10, 20);
			}
		}
		
	}

	private void GoblinSpawnInSurge()
	{
		currentTimePassedSurge += Time.deltaTime;

		if(currentTimePassedSurge >= goblinSurgeSpawnRate)
		{
			
			currentTimePassedSurge = 0f;
			SpawnGoblin();
		}
	}

	private void SpawnGoblin()
	{
		Vector3 randomPosition = GameManager.Instance.GetPlayerCurrentPosition();

		float randomXPos = Random.Range(3f, 10f);
		float randomYPos = Random.Range(3f, 10f);

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

		GameObject enemy = Instantiate(EnemyManager.Instance.all_Goblins[currentEnemyIndex], randomPosition, Quaternion.identity, EnemyManager.Instance.enemySpawnParent);
		GameManager.Instance.AddEnemyToActiveList(enemy.transform);

		currentSpawnCount += 1;

		if(currentSpawnCount == maxGoblinSpawnCount)
		{
			isGoblinSurgeOn = false;
		}
	}

	public void StopGoblinSurgeIfRunning()
	{
		isGoblinSurgeOn = false;
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
