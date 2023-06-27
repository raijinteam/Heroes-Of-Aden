using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinotaurController : MonoBehaviour
{
	private int damage;


	[SerializeField] private Animator anim;
	[SerializeField] private Transform spriteTransform;
	private Vector3 leftSideRotationValues = new Vector3(0, 0, 0);
	private Vector3 rightSideRotationValues = new Vector3(0, 180, 0);

	private bool isAttacking = false;
	[SerializeField] private float attackTimer;
	private float currentTimePassed = 0f;

	[Header("Jump Attack")]
	[SerializeField] private GameObject jumpMarker;
	[SerializeField] private float radius;
	[SerializeField] private LayerMask playerLayer;
	private int jumpDamage;
	[SerializeField] private ParticleSystem ps_GroundShock;

	[Header("Meteor Attack")]
	[SerializeField] private MinotaurMeteor meteor;
	private int meteorCount = 10;
	private int attackChance = 50;


	private string tag_JumpAttack = "JumpAttack";
	private string tag_Attack = "Attack";

	public void SetData(int _damage)
	{
		damage = _damage;
		jumpDamage = (int)(damage * 1.5);
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

		currentTimePassed += Time.deltaTime;
		if(currentTimePassed >= attackTimer)
		{
			currentTimePassed = 0f;
			isAttacking = true;

			int attackIndex = Random.Range(0, 100);

			if(attackIndex < attackChance)
			{
				JumpAttack();
			}
			else
			{
				MeteorAttack();
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

	private void JumpAttack()
	{
		StartCoroutine(JumpTowardsPlayer());
		StartCoroutine(JumpEffect());
	}

	private void MeteorAttack()
	{
		StartCoroutine(ThrowMeteors());
	}

	private void AreaDamage()
	{
		Collider2D colliders = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

		if(colliders != null)
		{
			GameManager.Instance.player.TakeDamageFromEnemy(jumpDamage);
		}
	}


	private IEnumerator JumpTowardsPlayer()
	{
		Vector3 currentPlayerPosition = GameManager.Instance.GetPlayerCurrentPosition();
		GameObject currentMarker = Instantiate(jumpMarker, currentPlayerPosition, jumpMarker.transform.rotation);

		yield return new WaitForSeconds(1.5f);
		anim.SetTrigger(tag_JumpAttack);

		float currentTime = 0f;
		float maxTime = 0.8f;

		Vector3 currentPosition = transform.position;

		while(currentTime < 1f)
		{
			currentTime += Time.deltaTime / maxTime;

			transform.position = Vector3.Lerp(currentPosition, currentPlayerPosition, currentTime);
			yield return null;
		}

		ps_GroundShock.Play();
		AreaDamage();
		isAttacking = false;
		Destroy(currentMarker);
	}

	

	private IEnumerator JumpEffect()
	{
		yield return new WaitForSeconds(1.5f);

		float currentTime = 0f;
		float maxTime = 0.5f;

		Vector3 currentScale = spriteTransform.localScale;
		Vector3 targetScale = new Vector3(1.3f, 1.3f, 1.3f);

		while(currentTime < 1f)
		{
			currentTime += Time.deltaTime/ maxTime;

			spriteTransform.localScale = Vector3.Lerp(currentScale, targetScale, currentTime);
			yield return null;
		}

		currentTime = 0f;
		while(currentTime < 1f)
		{
			currentTime += Time.deltaTime / maxTime;

			spriteTransform.localScale = Vector3.Lerp(targetScale, currentScale, currentTime);
			yield return null;
		}
	}

	private IEnumerator ThrowMeteors()
	{
		for(int i = 0; i < meteorCount; i++)
		{
			if (!GameManager.Instance.isGameRunning)
			{
				continue;
			}

			anim.SetTrigger(tag_Attack);
			Vector3 playerPos = GameManager.Instance.GetPlayerCurrentPosition();

			float randomXValue = Random.Range(-2f, 2f);
			float randomYValue = Random.Range(-2f, 2f);
			playerPos.x = playerPos.x + randomXValue;
			playerPos.y = playerPos.y + randomYValue;

			MinotaurMeteor mt = Instantiate(meteor, playerPos, meteor.transform.rotation);
			mt.SetData(damage);

			yield return new WaitForSeconds(0.32f);
		}

		isAttacking = false;
	}
}
