using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
	[SerializeField] private float flt_MoveSpeed;
	[SerializeField] private int damage;
	[SerializeField] private GameObject body;
	[SerializeField] private GameObject hitEffect;

	private bool hasCollided = false;

	public void SetPowerData(int _damage)
	{
		damage = _damage;
	}

	private void Start()
	{
		Destroy(gameObject, 8f);
	}

	private void Update()
	{
		if (hasCollided)
		{
			return;
		}
		transform.Translate(transform.right * flt_MoveSpeed * Time.deltaTime, Space.World);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (hasCollided)
		{
			return;
		}

		hasCollided = true;
		body.SetActive(false);
		hitEffect.SetActive(true);

		GameManager.Instance.player.TakeDamageFromEnemy(damage);
	}

}
