using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{
	private int damage;

	[SerializeField] private Animator anim;
	[SerializeField] private Transform spriteTransform;
	private Vector3 leftSideRotationValues = new Vector3(0, 0, 0);
	private Vector3 rightSideRotationValues = new Vector3(0, 180, 0);


	private float flt_AttackRate = 2f;
	private float currentTimePassed = 0f;
	private bool isAttacking = false;

	[Header("Missile Attack")]
	[SerializeField] private DemonMissileController missile;
	private int numberOfWaves = 4;
	private int missileCount = 20;
	public float radius = 2f;


	[Header("Fly Attack")]
	public bool isFlyAttack = false;
	private Vector3 targetPosition;
	private float flt_FlySpeed = 7f;
	public int flt_FlyDamage;
	private int flyAttackChance = 30;
	private float flyAttackStartDelay = 1f;
	private float currentFlyAttackStartTime = 0f;

	private string anim_FlyTag = "Fly";
	private string anim_IdleTag = "Idle";
	private string anim_AttackTag = "Attack";

    public void SetData(int _damage)
	{
		damage = _damage;
		flt_FlyDamage = (int)(damage * 1.5f);
	}

	private void Update()
	{
		if (!GameManager.Instance.isGameRunning)
		{
			return;
		}

		FacePlayer();

		if (isFlyAttack)
		{
			DoFlyAttack();
		}

		if (isAttacking)
		{
			return;
		}

		currentTimePassed += Time.deltaTime;

		if (currentTimePassed >= flt_AttackRate)
		{
			// attack now			
			currentTimePassed = 0f;
			isAttacking = true;
			RandomizeAttack();
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

	private void RandomizeAttack()
	{
		int attackIndex = Random.Range(0, 100);

		if(attackIndex < flyAttackChance)
		{
			anim.SetTrigger(anim_FlyTag);
			isFlyAttack = true;	
		}
		else
		{
			StartCoroutine(SpawnBullets());
		}
	}

	private void DoFlyAttack()
	{
		currentFlyAttackStartTime += Time.deltaTime;
		if(currentFlyAttackStartTime < flyAttackStartDelay)
		{
			targetPosition = GameManager.Instance.GetPlayerCurrentPosition();
			return;
		}

		transform.position = Vector3.MoveTowards(transform.position, targetPosition, flt_FlySpeed * Time.deltaTime);

		if(Vector3.Distance(transform.position, targetPosition) < 0.1f)
		{
			// fly attack over
			currentFlyAttackStartTime = 0f;
			anim.SetTrigger(anim_IdleTag);
			isFlyAttack = false;
			isAttacking = false;
		}
	}

	private IEnumerator SpawnBullets()
	{
		isAttacking = true;

		for(int j = 0; j < numberOfWaves; j++)
		{
			anim.SetTrigger(anim_AttackTag);

			yield return new WaitForSeconds(0.15f);

			float angleStep = 360f / missileCount;
			float angle = 0f;

			for (int i = 0; i <= missileCount - 1; i++)
			{
				float projectileDirXPosition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
				float projectileDirYPosition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

				Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition);
				Vector3 projectileMoveDirection = (projectileVector - transform.position).normalized;

				DemonMissileController bullet = Instantiate(missile, projectileVector, Quaternion.Euler(0, 0, angle - 90));
				bullet.transform.right = projectileMoveDirection;
				bullet.SetData(damage);
				//bullet.GetComponent<Rigidbody2D>().velocity =
				//	new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

				angle += angleStep;
			}

			yield return new WaitForSeconds(1f);
		}

		isAttacking = false;
	}

}
