using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemySpawning : MonoBehaviour
{
	[SerializeField] private int currentEnemyIndex;
	[SerializeField] private float normalEnemySpawnRate;
	private float currentTimePassedForNormalSpawnRate = 0f;
	private float rateDecreaseMultiplier = 0.8f;
	private float minimumSpawnRate = 0.1f;

	public void ReduceSpawnRate()
	{
		normalEnemySpawnRate = normalEnemySpawnRate * rateDecreaseMultiplier;
	
		if(normalEnemySpawnRate < minimumSpawnRate)
		{
			normalEnemySpawnRate = minimumSpawnRate;
		}
	}

	public void NormalEnemySpawnHandling()
	{
		currentTimePassedForNormalSpawnRate += Time.deltaTime;

		if (currentTimePassedForNormalSpawnRate >= normalEnemySpawnRate)
		{
			SpawnNormalEnemyAroundPlayer();
		}

		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//	SpawnNormalEnemyAroundPlayer();
		//}

	}

	private void SpawnNormalEnemyAroundPlayer()
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

		GameObject enemy = Instantiate(EnemyManager.Instance.enemyOne[currentEnemyIndex], randomPosition, Quaternion.identity, EnemyManager.Instance.enemySpawnParent);
		GameManager.Instance.AddEnemyToActiveList(enemy.transform);

		currentTimePassedForNormalSpawnRate = 0f;
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
