using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDragonController : MonoBehaviour
{
	private int damage;

	[SerializeField] private Animator anim;
	[SerializeField] private Transform spriteTransform;
	[SerializeField] private float flt_Speed;
	[SerializeField] private DragonMissileController missile;
	[SerializeField] private Transform spawnPointParent;
	[SerializeField] private Transform[] all_SpawnPoints;
	[SerializeField] private float attackTime = 1.5f;
	private float currentTimePassed = 0f;
	private bool isAttacking;

	private Vector3 leftSideRotationValues = new Vector3(0, 0, 0);
	private Vector3 rightSideRotationValues = new Vector3(0, 180, 0);

	private int fireballCount = 8;
	private int loopCount = 3;

	private int normalAttackChance = 70;

	private string tag_AttackAnim = "Attack";
	private string tag_RunAnim = "Run";
	private string tag_IdleAnim = "Idle";

	public void SetData(int _damage)
	{
		damage = _damage;
	}



	private void Update()
	{
		if (!GameManager.Instance.isGameRunning)
		{
			return;
		}

		FacePlayer();

		if (isAttacking)
		{
			return;
		}

		ChasePlayer();

		currentTimePassed += Time.deltaTime;

		if(currentTimePassed >= attackTime)
		{
			isAttacking = true;
			currentTimePassed = 0f;

			int randomAttackIndex = Random.Range(0, 100);

			if(randomAttackIndex < normalAttackChance)
			{
				ThrowMultipleFireballsFromSameLocation();
			}
			else
			{
				ThrowThreeFireBallAtOnce();
			}


			
		}

	}

	private void FacePlayer()
	{
		if (transform.position.x < GameManager.Instance.player.transform.position.x)
		{
			spriteTransform.localEulerAngles = leftSideRotationValues;
		}
		else
		{
			spriteTransform.localEulerAngles = rightSideRotationValues;
		}
	}

	private void ChasePlayer()
	{
		// Calculating direction from the enemy to the player
		Vector3 moveDirection = (GameManager.Instance.player.transform.position - transform.position).normalized;
		// Move the enemy towards the player using Transform.Translate
		transform.Translate(moveDirection * flt_Speed * Time.deltaTime);
	}

	private void ThrowMultipleFireballsFromSameLocation()
	{
		StartCoroutine(WaitAndShootFireballsFromSameLocation());
	}

	private void ThrowThreeFireBallAtOnce()
	{
		StartCoroutine(WaitAndShootThreeFireballs());
	}

	private IEnumerator WaitAndShootFireballsFromSameLocation()
	{
		anim.SetTrigger(tag_IdleAnim);
		yield return new WaitForSeconds(0.2f);

		for(int i = 0; i < fireballCount; i++)
		{
			if (!GameManager.Instance.isGameRunning)
			{
				continue;
			}

			anim.SetTrigger(tag_AttackAnim);

			Vector3 directionToTarget = GameManager.Instance.GetPlayerCurrentPosition() - transform.position;
			// Calculate the angle in degrees for the rotation towards the target
			float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
			// Apply the rotation to the child object
			spawnPointParent.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

			DragonMissileController dm = Instantiate(missile, all_SpawnPoints[0].position, spawnPointParent.rotation);
			dm.SetData(damage);
			yield return new WaitForSeconds(0.3f);
		}

		isAttacking = false;
		anim.SetTrigger(tag_RunAnim);
	}

	private IEnumerator WaitAndShootThreeFireballs()
	{
		anim.SetTrigger(tag_IdleAnim);
		yield return new WaitForSeconds(0.2f);

		for (int i = 0; i < loopCount; i++)
		{
			if (!GameManager.Instance.isGameRunning)
			{
				continue;
			}

			anim.SetTrigger(tag_AttackAnim);
			Vector3 directionToTarget = GameManager.Instance.GetPlayerCurrentPosition() - transform.position;
			// Calculate the angle in degrees for the rotation towards the target
			float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
			// Apply the rotation to the child object
			spawnPointParent.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

			for(int j = 0; j < all_SpawnPoints.Length; j++)
			{
				Debug.Log("Here");	
				DragonMissileController dm = Instantiate(missile, all_SpawnPoints[i].position, spawnPointParent.rotation);
				dm.SetData(damage);
			}

			yield return new WaitForSeconds(0.5f);
		}

		isAttacking = false;
		anim.SetTrigger(tag_RunAnim);
	}
}
