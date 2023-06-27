using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyController : MonoBehaviour
{
	[Header("Components")]
	private Rigidbody2D rb_Enemy;
	[SerializeField] private Transform spriteTransform;
	[SerializeField] private Animator anim;

	public bool isPlayerInRange = false;

	[Header("Movement Data")]
	[SerializeField] private float flt_MoveSpeed;
	private Vector3 moveDirection;
	private Vector3 leftSideRotationValues = new Vector3(0, 0, 0);
	private Vector3 rightSideRotationValues = new Vector3(0, 180, 0);
	private float distanceWithPlayerRequired = 5f;
	[SerializeField] private Vector3 randomTargetPosition;

	[Header("Attack Data")]
	[SerializeField] private EnemyBulletController bullet;
	[SerializeField] private Transform bulletSpawnPoint;
	[SerializeField] private int damage;
	[SerializeField] private float flt_AttackRate;
	private float flt_currentTimePassed = 0f;
	private bool isAttacking = false;
	private string str_Attack = "Attack";
	private string str_Walk = "Walk";

	public void SetData(int _damage)
	{
		damage = _damage;
	}

	private void Start()
	{
		rb_Enemy = GetComponent<Rigidbody2D>();
		FindARandomTargetPosition();
	}

	private void Update()
	{
		rb_Enemy.velocity = Vector2.zero;

		if (!GameManager.Instance.isGameRunning)
		{
			return;
		}

		CheckDistance();
		ChasePlayer();
		FacePlayer();
		ShootBullet();
	}

	private void FindARandomTargetPosition()
	{
		Vector3 playerPos = GameManager.Instance.GetPlayerCurrentPosition();

		float randomXPos = Random.Range(0f, playerPos.x + 6f);
		float randomYPos = Random.Range(0f, playerPos.y + 6f);

		int randomDirectionIndex = Random.Range(0, 2);
		if(randomDirectionIndex == 1)
		{
			randomXPos = -randomXPos;
		}

		randomDirectionIndex = Random.Range(0, 2);
		if (randomDirectionIndex == 1)
		{
			randomYPos = -randomYPos;
		}

		randomTargetPosition = new Vector3(playerPos.x + randomXPos, playerPos.y + randomYPos, transform.position.z);
	}

	private void CheckDistance()
	{
		float currentDistanceWithPlayer = Vector3.Distance(transform.position, GameManager.Instance.GetPlayerCurrentPosition());

		if(currentDistanceWithPlayer <= distanceWithPlayerRequired)
		{
			isPlayerInRange = true;
		}
		else
		{
			isPlayerInRange = false;
		}

		float distanceWithTargetPosition = Vector3.Distance(transform.position, randomTargetPosition);

		if(distanceWithTargetPosition <= 0.1f)
		{
			FindARandomTargetPosition();
		}
	}

	private void ChasePlayer()
	{
		if (isAttacking)
		{
			return;
		}

		moveDirection = (randomTargetPosition - transform.position).normalized;
		// Move the enemy towards the player using Transform.Translate
		transform.Translate(moveDirection * flt_MoveSpeed * Time.deltaTime);
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

	private void ShootBullet()
	{
		if (!isPlayerInRange)
		{
			return;
		}

		if (isAttacking)
		{
			return;
		}

		flt_currentTimePassed += Time.deltaTime;

		if (flt_currentTimePassed >= flt_AttackRate)
		{
			//anim.SetTrigger(str_AttackAnimation);
			flt_currentTimePassed = 0f;
			ShootBulletTowardsPlayer();
		
		}
	}

	private void ShootBulletTowardsPlayer()
	{
		isAttacking = true;

		StartCoroutine(WaitAndAttack());

		//Vector3 directionToTarget = GameManager.Instance.player.transform.position - transform.position;

		//// Calculate the angle in degrees for the rotation towards the target
		//float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
		//// Apply the rotation to the child object
		//bulletSpawnPoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

		//EnemyBulletController bul = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
		//bul.SetPowerData(damage);
		
	}

	private IEnumerator WaitAndAttack()
	{
		anim.SetTrigger(str_Attack);

		yield return new WaitForSeconds(0.5f);

		if (GameManager.Instance.isGameRunning)
		{
			Vector3 directionToTarget = GameManager.Instance.player.transform.position - transform.position;

			// Calculate the angle in degrees for the rotation towards the target
			float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
			// Apply the rotation to the child object
			bulletSpawnPoint.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

			EnemyBulletController bul = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
			bul.SetPowerData(damage);

			yield return new WaitForSeconds(0.5f);

			anim.SetTrigger(str_Walk);
			isAttacking = false;
		}
	}
}
