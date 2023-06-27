using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[SerializeField] private EnemyAbstractData data;
	[SerializeField] private Transform damageOffset;

	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip clip_Death;

	[SerializeField] private GameObject enemyDeathPartical;
	[SerializeField] private float flt_Health;

	[Header("Inferno")]
	private bool isStandingOnInferno = false;
	private int infernoDamageOverTime;
	private float infernoDamageWaitTime = 0.5f;
	private float currentInfernoDamageTime = 0f;

	// demo 
	public int deathCOunt = 0;
	public void SetInitialHealth(int _healthValue)
	{
		flt_Health = _healthValue;
	}

	private void Update()
	{
		if (isStandingOnInferno)
		{
			InfernoNadeDamgeOverTime();
		}
	}

	private void InfernoNadeDamgeOverTime()
	{
		currentInfernoDamageTime += Time.deltaTime;

		if(currentInfernoDamageTime >= infernoDamageWaitTime)
		{
			currentInfernoDamageTime = 0f;
			TakeDamage(infernoDamageOverTime);
		}
	}

	public void TakeDamage(int _damage)
	{
		if(flt_Health <= 0)
		{
			return;
		}
	
		flt_Health -= _damage;

		MMTextSpawnerManager.Instance.SpawnAtTarget(damageOffset, _damage);

		if(flt_Health <= 0)
		{
			deathCOunt += 1;

			//Debug.Log("Died : " + deathCOunt);
			GameManager.Instance.enemyKilled += 1;

			if (data.isBoss)
			{
				GameManager.Instance.BossDied(transform.position);
			}

            //play death sound
            if (DataManager.Instance.IsSFXON() == true)
            {
				//Debug.Log("Player Enemy Death Sound");
				SoundManager.Instance.PlayEnemyDeathSound(clip_Death);
				//audioSource.PlayOneShot(clip_Death);
			}

			// enemy died
			Instantiate(enemyDeathPartical, transform.position, Quaternion.identity);

			GameManager.Instance.EnemyDied(transform, data.points);
			Destroy(this.gameObject);
		}
	}

	public void ForceDie()
    {
		GameObject deathPartical = Instantiate(enemyDeathPartical, transform.position, Quaternion.identity);
		GameManager.Instance.EnemyDied(transform, data.points);
		Destroy(this.gameObject);
	}

	public void StandingOnInferno(int _damageOverTimeToBeTaken)
	{
		isStandingOnInferno = true;
		infernoDamageOverTime = _damageOverTimeToBeTaken;
	}

	public void NoLongerStandingOnInferno()
	{
		isStandingOnInferno = false;
		currentInfernoDamageTime = 0f;
	}

}
