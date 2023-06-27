using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingEnemyController : MonoBehaviour
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

	[Header("Attack Data")]
	[SerializeField] private int damage;
	[SerializeField] private float flt_AttackRate;
	private float flt_currentTimePassed = 0f;

	private string str_AttackAnimation = "Attack";

	private void Start()
	{
		rb_Enemy = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		rb_Enemy.velocity = Vector2.zero;

		if (!GameManager.Instance.isGameRunning)
		{
			return;
		}

		ChasePlayer();
		FacePlayer();
		DamagePlayerOverTime();
	}

	private void ChasePlayer()
	{
		if (isPlayerInRange)
		{
			return;
		}
		// Calculating direction from the enemy to the player
		moveDirection = (GameManager.Instance.player.transform.position - transform.position).normalized;
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

	private void DamagePlayerOverTime()
	{
		if (!isPlayerInRange)
		{
			return;
		}

		flt_currentTimePassed += Time.deltaTime;

		if(flt_currentTimePassed >= flt_AttackRate)
		{
			anim.SetTrigger(str_AttackAnimation);
			GameManager.Instance.player.TakeDamageFromEnemy(damage);
			flt_currentTimePassed = 0f;
		}
	}

	public void SetDamage(int _damage)
	{
		damage = _damage;
	}
}
