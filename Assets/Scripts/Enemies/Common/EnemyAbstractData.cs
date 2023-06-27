using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbstractData : MonoBehaviour
{
	public bool isBoss;
	public int baseHealth;
	public int baseDamage;
	public float PercentOfHealthIncreaseAccordingToLevel;
	public float PercentOfDamageIncreaseAccordingToLevel;
	public int points;

	public SpriteRenderer bodySprite;

	//public abstract void SetHealth();

	//public abstract void SetDamage();
	
	//public abstract void PlayerInRange();

	//public abstract void PlayerOutOfRange();

	public abstract void EnemyStandingInInferno(int _damageOverTime);

	public abstract void EnemyNotAffectedByInferno();
}
