using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JadeController : MonoBehaviour
{
	[SerializeField] private int damage;
	public GameObject body;
	private string tag_Enemy = "Enemy";

	public void SetDamage(int _damage)
	{
		damage = _damage;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{	
		// check for damage boost;
		int DamageToGive = (int)(damage + (damage * (GameManager.Instance.player.damageBoostPercent / 100)));
		// apply critical Chance
		int randomCritIndex = Random.Range(0, 100);

		if (randomCritIndex < GameManager.Instance.player.criticalChancePercent)
		{
			// crit
			DamageToGive = (int)(DamageToGive + (DamageToGive * (GameManager.Instance.player.criticalDamagePercent / 100)));
		}

		if (collision.gameObject.tag.Equals(tag_Enemy))
		{
			collision.GetComponent<CollisionControllerEnemy>().TakeDamage(DamageToGive);
		}

		//collision.GetComponent<CollisionControllerEnemy>().TakeDamage(DamageToGive);		
	}
}
