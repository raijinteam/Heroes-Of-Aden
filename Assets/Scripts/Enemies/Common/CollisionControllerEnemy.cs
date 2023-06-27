using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControllerEnemy : MonoBehaviour
{
	[SerializeField] private EnemyHealth health;
	[SerializeField] private EnemyAbstractData enemyHandler;

	public void TakeDamage(int _damage)
	{
		health.TakeDamage(_damage);
	}

	public void AffectedByInfernoFlames(int _damage)
	{
		enemyHandler.EnemyStandingInInferno(_damage);
	}
	
	public void NoLongerStandingOnInfernoFlames()
	{
		enemyHandler.EnemyNotAffectedByInferno();
	}
}
