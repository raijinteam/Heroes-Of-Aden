using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

	private void Awake()
	{
		Instance = this;
	}

	public Transform enemySpawnParent;

	[Header("Bosses")]
	public GameObject[] all_Bosses;

	[Header("Enemy - 1 Properties")]
	public GameObject[] enemyOne;

	[Header("Enemy - 2 Shooting")]
	public GameObject[] enemyShooting;

	[Header("Enemy = 3 Goblin")]
	public GameObject[] all_Goblins;

}
